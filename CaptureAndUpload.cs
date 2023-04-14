using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class CaptureAndUpload : MonoBehaviour
{
    public QRCodeGenerator qrcodeGenerator;
    private RenderTexture renderTexture;
    public RenderTexture renderToCapture;
    public string uploadURL = "https://yourserver.com/";
    string fileName;

    private void Start()
    {
        //cameraToCapture = Camera.main;
        renderTexture = new RenderTexture(
            renderToCapture.width,
            renderToCapture.height,
            24,
            RenderTextureFormat.ARGB32
        );
    }

    public void CaptureAndSendImage()
    {
        StartCoroutine(CaptureAndUploadImage());
    }

    IEnumerator CaptureAndUploadImage()
    {
        //yield return new WaitForEndOfFrame();

        if (renderToCapture == null)
        {
            Debug.LogError("A câmera não foi atribuída!");
            yield break;
        }

        Graphics.Blit(renderToCapture, renderTexture);

        Texture2D texture2D = new Texture2D(
            renderTexture.width,
            renderTexture.height,
            TextureFormat.RGBA32,
            false
        );
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(rect, 0, 0);
        RenderTexture.active = null;
        texture2D.Apply();
        byte[] bytes = texture2D.EncodeToJPG();
        Destroy(texture2D);
        RenderTexture.active = null;

        WWWForm form = new WWWForm();
        fileName = GenerateUnixTimeFileName();
        form.AddBinaryData("image", bytes, fileName+".jpg", "image/jpeg");

        UnityWebRequest www = UnityWebRequest.Post(uploadURL+"upload.php", form);
        yield return www.SendWebRequest();

        if (
            www.result == UnityWebRequest.Result.ConnectionError
            || www.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(www.error);
        }
        else
        {
            qrcodeGenerator.QRCodeCreate(uploadURL+"uploads/"+fileName+".jpg");
            Debug.Log("Upload concluído!");
        }
    }

    public string GenerateUnixTimeFileName()
    {
        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        // Converter o Unix Time para uma string e retorná-la
        return unixTime.ToString();
    }
}
