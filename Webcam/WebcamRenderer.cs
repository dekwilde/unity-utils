using UnityEngine;
using UnityEngine.UI;

public class WebcamRenderer : MonoBehaviour
{
    public RenderTexture renderTexture;
    public int webcamIndex = 0;
    public int width = 640;
    public int height = 480;
    public int fps = 30;

    private WebCamTexture webcamTexture;

    void Start()
    {
        // Verifica se a webcam selecionada existe
        if (WebCamTexture.devices.Length <= webcamIndex)
        {
            Debug.LogError("Webcam index out of range");
            return;
        }
        // Cria uma nova instância do WebCamTexture
        webcamTexture = new WebCamTexture(WebCamTexture.devices[webcamIndex].name, width, height, fps);

        webcamTexture.Play();
    }

    private void Update()
    {
        // Atualiza a textura da RenderTexture com a imagem mais recente da webcam
        Graphics.Blit(webcamTexture, renderTexture);
    }


    void OnDestroy()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
