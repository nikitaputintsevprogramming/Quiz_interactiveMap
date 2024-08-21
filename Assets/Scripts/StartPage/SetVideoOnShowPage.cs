using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // For VideoPlayer
using System.IO;
using UI.Pagination;

public class SetVideoOnShowPage : MonoBehaviour
{
    public void SetVideoToFirstPage(string video)
    {
        // Path to the video file in the StreamingAssets folder
        string videoPath = Path.Combine(Application.streamingAssetsPath, video);

        if (File.Exists(videoPath))
        {
            // Find the VideoPlayer component in the first Page
            VideoPlayer videoPlayer = FindObjectOfType<Page>().GetComponentInChildren<VideoPlayer>();

            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is not assigned.");
                return;
            }

            // Set the video path
            videoPlayer.url = videoPath;

            // Start playing the video
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError($"Video file not found at path: {videoPath}");
        }
    }
}
