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
     public Slider slider;

     [Header("Options")]
     public float fadeInDuration = 0.4f;
     public bool playOnEnable = true;

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
          rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0);
          videoPlayer.Prepare();

          yield return new WaitUntil(() => videoPlayer.isPrepared);

          rawImage.texture = videoPlayer.texture;
          Application.targetFrameRate = (int)videoPlayer.frameRate;
          videoPlayer.Play();
          audioSource.Play();

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

          yield return new WaitUntil(() => videoPlayer.time >= videoPlayer.clip.length - .2f);
          yield return new WaitForSeconds(fadeInDuration);

          if (onComplete != null)
               onComplete.Invoke();
     }

}