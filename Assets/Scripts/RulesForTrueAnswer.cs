using UI.Pagination;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System.Collections.Generic;

namespace Quiz
{
    public class RulesForTrueAnswer : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer videoPlayer; // Компонент VideoPlayer, который будет использоваться для воспроизведения видео

        private Dictionary<string, string> videoClipPaths; // Словарь для хранения путей к видеофайлам

        private void Start()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is not assigned.");
            }

            // Загружаем пути к видеоклипам из папки StreamingAssets/Videos
            LoadVideoPathsFromStreamingAssets();

            if (videoClipPaths.Count == 0)
            {
                Debug.LogError("No video clips found in the StreamingAssets/Videos folder.");
            }

            // Подписка на событие правильного ответа
            FindObjectOfType<PagesManager>().OnCorrectAnswer += PlayVideoForCorrectAnswer;
        }

        private void OnDestroy()
        {
            // Отписка от события при уничтожении объекта, чтобы избежать утечек памяти
            PagesManager pagesManager = FindObjectOfType<PagesManager>();
            if (pagesManager != null)
            {
                pagesManager.OnCorrectAnswer -= PlayVideoForCorrectAnswer;
            }
        }

        private void LoadVideoPathsFromStreamingAssets()
        {
            string videosPath = Path.Combine(Application.streamingAssetsPath, "Videos");

            if (Directory.Exists(videosPath))
            {
                videoClipPaths = new Dictionary<string, string>();

                string[] videoFiles = Directory.GetFiles(videosPath, "*.mp4");

                foreach (string filePath in videoFiles)
                {
                    string videoName = Path.GetFileNameWithoutExtension(filePath);
                    videoClipPaths.Add(videoName, filePath);
                }
            }
            else
            {
                Debug.LogError($"Videos folder not found at path: {videosPath}");
            }
        }

        private void PlayVideoForCorrectAnswer(string textureName)
        {
            if (string.IsNullOrEmpty(textureName))
            {
                Debug.LogError("Texture name is null or empty.");
                return;
            }

            Debug.Log($"Looking for video for texture name: {textureName}");

            if (videoClipPaths.TryGetValue(textureName, out string videoPath))
            {
                // Устанавливаем путь к видеоклипу и начинаем воспроизведение
                videoPlayer.url = videoPath;
                videoPlayer.Play();
                Debug.Log($"Playing video for texture: {textureName} from path: {videoPath}");
            }
            else
            {
                Debug.LogError($"No video found for the given texture name: {textureName}");
            }
        }

    }
}
