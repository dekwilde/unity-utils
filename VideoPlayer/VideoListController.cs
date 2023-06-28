using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoListController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<VideoClip> videoClips;
    
    public void ChangeVideo(int index)
    {
        // Troca o vídeo atual pelo selecionado na lista
        if (index >= 0 && index < videoClips.Count)
        {
            videoPlayer.Stop();
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    public void RandomVideo()
    {
        if (videoClips.Count > 0)
        {
            int randomIndex = Random.Range(0, videoClips.Count);
            ChangeVideo(randomIndex);
        }
        else
        {
            Debug.LogWarning("A lista de vídeo está vazia. Não é possível selecionar um vídeo aleatório.");
        }
    }
}
