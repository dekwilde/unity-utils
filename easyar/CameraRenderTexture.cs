using easyar;
using UnityEngine;
using UnityEngine.UI;

public class CameraRenderTexture : MonoBehaviour
{
    public ARSession Session;
    private RenderTexture renderTexture;
    public  RenderTexture colorRenderTexture;
    private void Awake()
    {
        Session.StateChanged += (state) =>
        {
            if (state == ARSession.SessionState.Ready)
            {
                var renderer = Session.Assembly.FrameSource.GetComponent<CameraImageRenderer>();
                renderer.RequestTargetTexture((camera, texture) => { renderTexture = texture; });
            }
        };

    }
    void Update() {
        Graphics.Blit(renderTexture, colorRenderTexture);
    }

}
