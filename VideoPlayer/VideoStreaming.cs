using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStreaming : MonoBehaviour
{
    public VideoPlayer video;
    private bool isPlaying;

    private void Start() {
        isPlaying = true;
        video.source = VideoSource.Url;
        video.url = "file://" + Application.streamingAssetsPath + "/video.mp4";
    }

    public void TogglePlay() {
        if(isPlaying) {
            Debug.Log("Pause");
            video.Pause();
            isPlaying = false;
        } else {
            Debug.Log("Play");
            video.Play();
            isPlaying = true;
        }
    }

}
