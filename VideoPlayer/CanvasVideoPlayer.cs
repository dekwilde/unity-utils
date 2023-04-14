using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class CanvasVideoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    [Header("Options")]
    public float fadeInDuration = 0.4f;
    public bool playOnEnable = true;

    private bool videoEnd = false;

    public UnityEvent onStart;

    public UnityEvent onComplete;

    private void OnEnable()
    {

        if (playOnEnable) PlayVideo();
    }

    public void PlayVideo()
    {
        StartCoroutine(StartVideo());
    }

    IEnumerator StartVideo()
    {
        videoEnd = false;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0);
        videoPlayer.Prepare();

        yield return new WaitUntil(() => videoPlayer.isPrepared);

        videoPlayer.loopPointReached += OnMovieFinished;

        rawImage.texture = videoPlayer.texture;
        Application.targetFrameRate = (int)videoPlayer.frameRate;
        //videoPlayer.Play();
        //audioSource.Play();

        yield return new WaitUntil(() => videoPlayer.time > 0.05f);

        PotaTween tween = PotaTween.Create(gameObject, 0);
        tween.SetFloat(0, 1);
        tween.SetDuration(fadeInDuration);
        tween.UpdateCallback(() =>
        {
            //rawImage.color = Color.Lerp(Color.black, Color.white, tween.Duration);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, tween.Float.Value);

        });
        tween.Play(() =>
        {
            if (onStart != null)
                onStart.Invoke();
        });

    }

    void OnMovieFinished(VideoPlayer player)
    {
        if(!videoEnd) {
          videoEnd = true;
          Debug.Log("Event for movie end called");
          player.Stop();
          if (onComplete != null) {
               onComplete.Invoke();
          } 
        }
    }

}