using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorCapture : MonoBehaviour
{

    [SerializeField] private Image colorImagePicker; //cor salva
    [SerializeField] private Image colorImageActive; //cor realtime
    [SerializeField] private RenderTexture renderTexture; // RenderTexture que será analisada
    public Color defaultColor;
    [SerializeField] private Color targetColor; // cor a ser procurada
    [SerializeField] private Color pixelColorOnInspector; // cor que ele está vendo
    [Range(0,1)]
    [SerializeField] private float colorThreshold = 0.15f; // cor a ser procurada
    [SerializeField] private Rect captureArea; // área na RenderTexture a ser analisada

    
    public UnityEvent ColorFound;

    void Start()
    {
        LoadColor();
    }

    private void LoadColor()
    {
        if (PlayerPrefs.HasKey("colorPicker"))
        {
            string colorString = PlayerPrefs.GetString("colorPicker");
            string[] rgba = colorString.Split(',');
            float.TryParse(rgba[0], out float r);
            float.TryParse(rgba[1], out float g);
            float.TryParse(rgba[2], out float b);
            float.TryParse(rgba[3], out float a);
            targetColor = new Color(r, g, b, a);

        }
        else
        {
            targetColor = defaultColor;
        }
        colorImageActive.color = targetColor; 
    }




    private void Update()
    {
        colorImagePicker.color = pixelColorOnInspector; //mostra qual é a cor que está sendo capturada

        Texture2D tempTexture = new Texture2D((int)captureArea.width, (int)captureArea.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        tempTexture.ReadPixels(captureArea, 0, 0);
        RenderTexture.active = null;
        tempTexture.Apply();

        // Percorra os pixels da textura temporária em busca da cor alvo
        Color[] pixels = tempTexture.GetPixels();
        foreach (Color pixelColor in pixels)
        {
            bool isTargetColor = ColorThreshold(pixelColor);
            if (isTargetColor == true)
            {
                Debug.Log("Cor encontrada na área especificada!");
                ColorFound.Invoke();
                break;
            }
        }

        // Limpe a textura temporária
        Destroy(tempTexture);
    }

    private bool ColorThreshold(Color color)
    {
        float targetColorR = targetColor.r;
        float targetColorG = targetColor.g;
        float targetColorB = targetColor.b;

        pixelColorOnInspector = color;

        if ((color.r < (targetColorR - colorThreshold)) || (color.r > targetColorR + colorThreshold))
        {
            return false;
        }

        if ((color.g < (targetColorG - colorThreshold)) || (color.g > targetColorG + colorThreshold))
        {
            return false;
        }

        if ((color.b < (targetColorB - colorThreshold)) || (color.b > targetColorB + colorThreshold))
        {
            return false;
        }

        return true;
    }

    public static float Remap(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        float fromRange = fromHigh - fromLow;
        float toRange = toHigh - toLow;
        float scaledValue = (value - fromLow) / fromRange;
        return toLow + (scaledValue * toRange);
    }

    public void SaveColor() {
        targetColor = pixelColorOnInspector;
        colorImageActive.color = targetColor; 
        string colorString = $"{targetColor.r},{targetColor.g},{targetColor.b},{targetColor.a}";
        PlayerPrefs.SetString("colorPicker", colorString);
        PlayerPrefs.Save();
    }


}
