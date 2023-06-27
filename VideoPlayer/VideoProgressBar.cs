using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 using UnityEngine.EventSystems;
using UnityEngine.UI;
 using UnityEngine.Video;


public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private Image progress;

    public GameObject knob;
    
    float pct = 0.0f;

    bool isPointer = false;

    private void Awake()
    {
        progress = GetComponent<Image>();
    }

    float percAmount;
    private void Update()
    {
        if (videoPlayer.frameCount > 0)
            percAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
            progress.fillAmount = percAmount;
            if(!isPointer) {
                knob.transform.localPosition = new Vector2(0, -this.gameObject.GetComponent<RectTransform>().sizeDelta.y * percAmount + this.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isPointer = true;
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointer = true;
        TrySkip(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointer = false;
        SkipToPercent(pct);
    }


    public void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            knob.transform.localPosition = new Vector2(0, localPoint.y);
            pct = Mathf.InverseLerp(progress.rectTransform.rect.yMax, progress.rectTransform.rect.yMin, localPoint.y);
        }
    }

    private void SkipToPercent(float pct)
    {
        var frame = videoPlayer.frameCount * pct;
        videoPlayer.frame = (long)frame;

        
    }
}