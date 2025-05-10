using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTracker : MonoBehaviour
{
    public RawImage webcamDisplay;
    public GameObject targetObject;
    public Color targetColor = Color.red;
    public float colorTolerance = 0.2f;
    public int webcamIndex = 0;
    public int width = 640;
    public int height = 480;
    public int fps = 30;

    private WebCamTexture webcamTexture;

    void Start()
    {
        if (WebCamTexture.devices.Length <= webcamIndex)
        {
            Debug.LogError("Webcam index out of range");
            return;
        }
        // Cria uma nova instância do WebCamTexture
        webcamTexture = new WebCamTexture(WebCamTexture.devices[webcamIndex].name, width, height, fps);
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();
    }

    void Update()
    {
        TrackColor();
        if (Input.GetMouseButtonDown(0))
        {
            PickColor();
        }
    }

    void TrackColor()
    {
        Color32[] pixels = webcamTexture.GetPixels32();
        int width = webcamTexture.width;
        int height = webcamTexture.height;

        Vector2 averagePos = Vector2.zero;
        int foundPixels = 0;

        for (int y = 0; y < height; y += 5)
        {
            for (int x = 0; x < width; x += 5)
            {
                Color32 pixel = pixels[y * width + x];
                if (IsColorMatch(pixel, targetColor))
                {
                    averagePos += new Vector2(x, y);
                    foundPixels++;
                }
            }
        }

        if (foundPixels > 0)
        {
            averagePos /= foundPixels;
            Vector2 normalizedPos = new Vector2(averagePos.x / width, averagePos.y / height);

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(normalizedPos.x, normalizedPos.y, Camera.main.nearClipPlane + 10f));

            targetObject.transform.position = new Vector3(worldPos.x, worldPos.y, targetObject.transform.position.z);
        }
    }

    bool IsColorMatch(Color32 pixel, Color target)
    {
        float diff = Mathf.Abs(pixel.r / 255f - target.r) + Mathf.Abs(pixel.g / 255f - target.g) + Mathf.Abs(pixel.b / 255f - target.b);
        return diff < colorTolerance;
    }
    void PickColor()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransform rectTransform = webcamDisplay.rectTransform;

        Vector2 localCursor;
        var canvas = webcamDisplay.canvas;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, canvas.worldCamera, out localCursor))
        {
            float x = (localCursor.x + rectTransform.rect.width * 0.5f) / rectTransform.rect.width;
            float y = (localCursor.y + rectTransform.rect.height * 0.5f) / rectTransform.rect.height;

            int texX = Mathf.Clamp((int)(x * webcamTexture.width), 0, webcamTexture.width - 1);
            int texY = Mathf.Clamp((int)(y * webcamTexture.height), 0, webcamTexture.height - 1);

            targetColor = webcamTexture.GetPixel(texX, texY);
        }
    }
}

