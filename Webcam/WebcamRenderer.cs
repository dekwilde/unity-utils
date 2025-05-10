using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WebcamRenderer : MonoBehaviour
{
    public RenderTexture renderTexture;
    // numero alternativo caso o json não esteja presente
    public int webcamIndex = 0;
    private int webcamID;
    public int width = 640;
    public int height = 480;
    public int fps = 30;

    private WebCamTexture webcamTexture;

    void Awake() {
        webcamID = loadJSON();
    }
    
    void Start()
    {
        // Verifica se a webcam selecionada existe
        if (WebCamTexture.devices.Length <= webcamID)
        {
            Debug.LogError("Webcam index out of range");
            return;
        }
        // Cria uma nova instância do WebCamTexture
        webcamTexture = new WebCamTexture(
            WebCamTexture.devices[webcamID].name,
            width,
            height,
            fps
        );

        webcamTexture.Play();
    }

    private void Update()
    {
        // Atualiza a textura da RenderTexture com a imagem mais recente da webcam
        Graphics.Blit(webcamTexture, renderTexture);
    }

    int loadJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", "config.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            JsonData loadedData = JsonUtility.FromJson<JsonData>(dataAsJson);
            Debug.Log("loadJSON base_url " + loadedData.webcam);
            return loadedData.webcam;
        }
        else
        {
            return webcamIndex;
            Debug.LogError("Cannot find file! named");
        }
    }

    void OnDestroy()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
