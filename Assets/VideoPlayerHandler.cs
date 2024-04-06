using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Update()
    {
        if(videoPlayer.frame == (long)videoPlayer.frameCount - 1) 
        {
            VideoEnd();
        }
    }
    private void VideoEnd() 
    {
        Debug.Log("Video End");
    }
}
