﻿using UI.Pagination;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI; // Не забудьте подключить этот namespace для использования UI элементов.

namespace Quiz
{
    public class RulesForTrueAnswer : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer videoPlayer; // Компонент VideoPlayer, который будет использоваться для воспроизведения видео

        [SerializeField]
        private RawImage correctAnswerImage; // UI элемент для отображения изображения правильного ответа

        [SerializeField]
        private RawImage videoImage; // UI элемент для отображения видео

        private Dictionary<string, string> videoClipPaths; // Словарь для хранения путей к видеофайлам

        private void Start()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is not assigned.");
            }

            if (correctAnswerImage == null)
            {
                Debug.LogError("RawImage for correct answer is not assigned.");
            }

            if (videoImage == null)
            {
                Debug.LogError("RawImage for video is not assigned.");
            }

            // Скрыть оба RawImage при старте
            correctAnswerImage.gameObject.SetActive(false);
            videoImage.gameObject.SetActive(false);

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

            // Отписка от события, чтобы избежать повторного воспроизведения
            FindObjectOfType<PagesManager>().OnCorrectAnswer -= PlayVideoForCorrectAnswer;

            // Показать изображение правильного ответа и затем видео
            StartCoroutine(ShowCorrectAnswerAndPlayVideo(textureName));
        }

        private IEnumerator<WaitForSeconds> ShowCorrectAnswerAndPlayVideo(string textureName)
        {
            // Показать изображение правильного ответа
            string correctAnswerImagePath = Path.Combine(Application.streamingAssetsPath, "correct_answer.jpg");

            if (File.Exists(correctAnswerImagePath))
            {
                // Загрузить текстуру для изображения
                byte[] imageData = File.ReadAllBytes(correctAnswerImagePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);

                // Установить текстуру в RawImage
                correctAnswerImage.texture = texture;
                correctAnswerImage.gameObject.SetActive(true);

                // Подождать 3 секунды
                yield return new WaitForSeconds(3f);
            }
            else
            {
                Debug.LogError($"Correct answer image not found at path: {correctAnswerImagePath}");
            }

            // Скрыть изображение правильного ответа
            correctAnswerImage.gameObject.SetActive(false);

            // Установить RawImage для видео и начать воспроизведение
            if (videoClipPaths.TryGetValue(textureName, out string videoPath))
            {
                videoImage.gameObject.SetActive(true);
                videoPlayer.gameObject.SetActive(true);
                videoPlayer.url = videoPath;
                videoPlayer.Play();
                videoPlayer.loopPointReached += OnVideoEnd; // Подписка на событие окончания видео
                Debug.Log($"Playing video for texture: {textureName} from path: {videoPath}");
            }
            else
            {
                Debug.LogError($"No video found for the given texture name: {textureName}");
            }
        }

        private void OnVideoEnd(VideoPlayer vp)
        {
            Debug.Log("Video has finished playing.");
            vp.loopPointReached -= OnVideoEnd; // Отписка от события окончания видео
            videoImage.gameObject.SetActive(false); // Скрыть RawImage для видео
            videoPlayer.gameObject.SetActive(false); // Скрыть VideoPlayer
        }
    }
}
