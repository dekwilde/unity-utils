using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamTrails : MonoBehaviour
{
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public int captureWidth = 640;
    public int captureHeight = 480;
    public int targetFPS = 30;
    public float trailFactor = 0.95f;
    public float threshold = 0.1f;


    private Texture2D trailTexture;
    private Color[] trailColors;

    void Start()
    {

        trailTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        trailColors = new Color[captureWidth * captureHeight];
    }

    void Update()
    {
        Rect captureArea = new Rect(0, 0, captureWidth, captureHeight);
        Texture2D tempTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        tempTexture.ReadPixels(captureArea, 0, 0);
        RenderTexture.active = null;
        tempTexture.Apply();


        Color[] webcamColors = tempTexture.GetPixels();
        for (int i = 0; i < webcamColors.Length; i++)
        {
            if (webcamColors[i].grayscale > threshold)
            {
                trailColors[i] = Color.Lerp(trailColors[i], webcamColors[i], Time.deltaTime);
            }
            else
            {
                trailColors[i] *= trailFactor;
            }
        }
        trailTexture.SetPixels(trailColors);
        trailTexture.Apply();
        rawImage.texture = trailTexture;
        rawImage.material.mainTexture = trailTexture;
    }
}
