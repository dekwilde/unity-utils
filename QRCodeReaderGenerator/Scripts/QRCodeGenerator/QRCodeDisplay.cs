using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QRCodeDisplay : MonoBehaviour
{
    public GameObject objectTarget;
    public Image targetImage; // Referência para o objeto Image no Canvas
    public UnityEvent EventInvoke;

    void Start() {
        objectTarget.SetActive(false);
    }
    
    public void SetQRCode(Texture2D qrCodeTexture)
    {
        // Converte a textura do QRCode em um Sprite
        Sprite qrCodeSprite = TextureToSprite(qrCodeTexture);
        // Atribui o Sprite do QRCode à imagem do Canvas
        targetImage.sprite = qrCodeSprite;
        objectTarget.SetActive(true);
        EventInvoke.Invoke();
    }

    private Sprite TextureToSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Sprite sprite = Sprite.Create(texture, rect, pivot);
        return sprite;
    }
}
