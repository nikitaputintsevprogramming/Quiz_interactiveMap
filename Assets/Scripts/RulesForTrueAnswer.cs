using UI.Pagination;
using UnityEngine;
using UnityEngine.Video;

namespace Quiz
{
    public class RulesForTrueAnswer : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer videoPlayer; // Компонент VideoPlayer, который будет использоваться для воспроизведения видео

        [SerializeField]
        private VideoClip[] videoClips; // Массив видеоклипов, соответствующих вопросам

        private void Start()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component is not assigned.");
            }

            if (videoClips.Length == 0)
            {
                Debug.LogError("VideoClips array is empty.");
            }

            // Подписка на событие правильного ответа
            FindObjectOfType<PagesManager>().OnCorrectAnswer += PlayVideoForCorrectAnswer;
        }

        private void OnDestroy()
        {
            // Отписка от события при уничтожении объекта, чтобы избежать утечек памяти
            if (FindObjectOfType<PagesManager>() != null)
            {
                FindObjectOfType<PagesManager>().OnCorrectAnswer -= PlayVideoForCorrectAnswer;
            }
        }

        private void PlayVideoForCorrectAnswer(KeyCode keyCode)
        {
            // Определите индекс видео, соответствующий ключу ответа
            int videoIndex = GetVideoIndexFromKeyCode(keyCode);

            if (videoIndex >= 0 && videoIndex < videoClips.Length)
            {
                // Устанавливаем видеоклип и начинаем воспроизведение
                videoPlayer.clip = videoClips[videoIndex];
                videoPlayer.Play();
                Debug.Log($"Playing video for answer {keyCode}");
            }
            else
            {
                Debug.LogError("No video found for the given KeyCode.");
            }
        }

        private int GetVideoIndexFromKeyCode(KeyCode keyCode)
        {
            // Преобразуйте KeyCode в индекс видео
            // Предполагается, что KeyCode соответствует индексам в videoClips
            switch (keyCode)
            {
                case KeyCode.Keypad1:
                    return 0;
                case KeyCode.Keypad2:
                    return 1;
                case KeyCode.Keypad3:
                    return 2;
                case KeyCode.Keypad4:
                    return 3;
                case KeyCode.Keypad5:
                    return 4;
                case KeyCode.Keypad6:
                    return 5;
                case KeyCode.Keypad7:
                    return 6;
                case KeyCode.Keypad8:
                    return 7;
                default:
                    return -1;
            }
        }
    }
}
