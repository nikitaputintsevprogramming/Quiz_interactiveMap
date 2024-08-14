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
                //FindObjectOfType<PagesManager>().OnCorrectAnswer -= PlayVideoForCorrectAnswer;
            }
        }

        private void PlayVideoForCorrectAnswer(string questionText)
        {
            // Определите индекс видео, соответствующий тексту вопроса
            int videoIndex = GetVideoIndexFromQuestionText(questionText);

            if (videoIndex >= 0 && videoIndex < videoClips.Length)
            {
                // Устанавливаем видеоклип и начинаем воспроизведение
                videoPlayer.clip = videoClips[videoIndex];
                videoPlayer.Play();
                Debug.Log($"Playing video for question: {questionText}");
            }
            else
            {
                Debug.LogError("No video found for the given question text.");
            }
        }

        private int GetVideoIndexFromQuestionText(string questionText)
        {
            // Преобразуйте текст вопроса в индекс видео
            // Предполагается, что текст вопроса совпадает с названием видео
            for (int i = 0; i < videoClips.Length; i++)
            {
                if (videoClips[i] != null && videoClips[i].name == questionText)
                {
                    return i;
                }
            }
            return -1; // Возвращает -1, если видео не найдено
        }
    }
}
