using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WebcamLightenBlend : MonoBehaviour
{
    public Text countdown;
    public GameObject flash;
    public GameObject loading;
    public RenderTexture renderTexture1;
    public RenderTexture renderTexture2;
    public RenderTexture renderMerged;

    public GameObject Webcam1;
    public GameObject Webcam2;
    public GameObject WebcamMerged;
    public int captureWidth = 640;
    public int captureHeight = 480;
    public int targetFPS = 30;

    //public Color targetColor = Color.white;
    //public float colorVariance = 0.1f;

    private Texture2D resultTexture;
    public List<Texture2D> capturedFrames;
    private bool isCapturing;

    public UnityEvent OnStopCount;
    public UnityEvent OnProgress;
    public UnityEvent OnStartCapture;
    public UnityEvent OnSaveCapture;
    public UnityEvent OnEndCapture;

    private void Start()
    {
        Caching.ClearCache();
        capturedFrames = new List<Texture2D>();
        ResetCapture();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }


    public void ResetCapture()
    {
        countdown.text = "";
        flash.SetActive(false);
        loading.SetActive(false);
        //Webcam1.SetActive(true);
        //Webcam2.SetActive(false);
        WebcamMerged.SetActive(false);
        capturedFrames.Clear();
    }

    public void StartCapture()
    {
        if (!isCapturing)
        {
            StartCoroutine(GenerateImage());
        }
    }

    public void StopCapture()
    {
        isCapturing = false;
    }

    void CaptureFrame(RenderTexture renderTexture)
    {
        Rect captureArea = new Rect(0, 0, captureWidth, captureHeight);
        Texture2D tempTexture = new Texture2D(
            captureWidth,
            captureHeight,
            TextureFormat.RGBA32,
            false
        );
        RenderTexture.active = renderTexture;
        tempTexture.ReadPixels(captureArea, 0, 0);
        RenderTexture.active = null;
        tempTexture.Apply();

        Texture2D frame = new Texture2D(captureWidth, captureHeight, TextureFormat.RGBA32, false);
        frame.SetPixels(tempTexture.GetPixels());
        frame.Apply();
        capturedFrames.Add(frame);
    }

    private IEnumerator GenerateImage()
    {
        isCapturing = true;
        float captureStartTime = Time.time;
        float elapsedTime = 0;

        countdown.text = "3";
        yield return new WaitForSeconds(1f);
        countdown.text = "2";
        yield return new WaitForSeconds(1f);
        countdown.text = "1";
        yield return new WaitForSeconds(1f);
        flash.SetActive(true);
        countdown.text = "";
        OnStopCount.Invoke();
        yield return new WaitForSeconds(1f);
        CaptureFrame(renderTexture1);
        yield return new WaitForSeconds(0.5f);
        //Webcam1.SetActive(false);
        //Webcam2.SetActive(true);
        flash.SetActive(false);
        yield return new WaitForSeconds(1f);

        //GRAVANDO
        OnStartCapture.Invoke();

        while (isCapturing)
        {
            yield return new WaitForEndOfFrame();
            CaptureFrame(renderTexture2);
        }
        loading.SetActive(true);
        //PROCESSANDO
        OnProgress.Invoke();
        yield return new WaitForSeconds(1f);
        BlendLighten();
        loading.SetActive(false);
        //SALVANDO
        OnSaveCapture.Invoke();
        yield return new WaitForSeconds(10f);
        ResetCapture();
        OnEndCapture.Invoke();
    }



    private void BlendLighten()
    {
        resultTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGBA32, false);
        Color32[] finalColors = new Color32[captureWidth * captureHeight];

        // Inicializa a primeira cor final com o valor da primeira textura capturada
        Texture2D firstTexture = capturedFrames[0];
        Color32[] firstColors = firstTexture.GetPixels32();
        for (int i = 0; i < finalColors.Length; i++)
        {
            finalColors[i] = firstColors[i];
        }

        for (int i = 1; i < capturedFrames.Count; i++)
        {
            Color32[] currentColors = capturedFrames[i].GetPixels32();
            for (int j = 0; j < finalColors.Length; j++)
            {
                // Realiza a operação Blend Lighten para cada pixel
                finalColors[j] = new Color32(
                    (byte)Mathf.Max(finalColors[j].r, currentColors[j].r),
                    (byte)Mathf.Max(finalColors[j].g, currentColors[j].g),
                    (byte)Mathf.Max(finalColors[j].b, currentColors[j].b),
                    255
                );
            }
        }

        resultTexture.SetPixels32(finalColors);
        resultTexture.Apply();

        Texture2D mergeTexture = CombineTextures(firstTexture, resultTexture, "screen");

        WebcamMerged.SetActive(true);
        Graphics.Blit(mergeTexture, renderMerged);

    }



    private Color LightenBlend(Color source, Color destination)
    {
        return new Color(
            Mathf.Max(source.r, destination.r),
            Mathf.Max(source.g, destination.g),
            Mathf.Max(source.b, destination.b),
            1f
        );
    }

    private Texture2D CombineTextures(Texture2D texture1, Texture2D texture2, string blendMode)
    {
        int width = texture1.width;
        int height = texture1.height;

        if (texture2.width != width || texture2.height != height)
        {
            Debug.LogError("The dimensions of the two textures don't match");
            return null;
        }

        Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color1 = texture1.GetPixel(x, y);
                Color color2 = texture2.GetPixel(x, y);

                Color finalColor = BlendColors(color1, color2, blendMode);
                result.SetPixel(x, y, finalColor);
            }
        }

        result.Apply();
        return result;
    }

    private Color BlendColors(Color color1, Color color2, string blendMode)
    {
        switch (blendMode)
        {
            case "screen":
                return ScreenBlend(color1, color2);
            case "additive":
                return AdditiveBlend(color1, color2);
            default:
                return color1;
        }
    }

    private Color ScreenBlend(Color color1, Color color2)
    {
        return Color.white - ((Color.white - color1) * (Color.white - color2));
    }

    private Color AdditiveBlend(Color color1, Color color2)
    {
        return color1 + color2;
    }
}
