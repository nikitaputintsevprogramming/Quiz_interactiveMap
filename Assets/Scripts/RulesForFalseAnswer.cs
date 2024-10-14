using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Для работы с UI элементами
using System.IO;
using Quiz;

namespace Assets.Scripts
{
    public class RulesForFalseAnswer : MonoBehaviour
    {
        [SerializeField] private Canvas CanvasVideoQuestion;
        [SerializeField] private Canvas CanvasAnswers;
        [SerializeField] private Canvas CanvasPages;

        [SerializeField]
        private RawImage incorrectAnswerImage; // UI элемент для отображения изображения неправильного ответа

        public bool isImageFalseShowing = false; // Флаг для отслеживания отображения изображения

        private void Start()
        {
            if (incorrectAnswerImage == null)
            {
                Debug.LogError("RawImage for incorrect answer is not assigned.");
            }

            // Скрыть изображение при старте
            incorrectAnswerImage.gameObject.SetActive(false);

            // Подписка на событие неверного ответа
            FindObjectOfType<PagesManager>().OnIncorrectAnswer += HandleIncorrectAnswer;
        }

        private void OnDestroy()
        {
            // Отписка от события при уничтожении объекта
            PagesManager pagesManager = FindObjectOfType<PagesManager>();
            if (pagesManager != null)
            {
                pagesManager.OnIncorrectAnswer -= HandleIncorrectAnswer;
            }
        }

        private void HandleIncorrectAnswer()
        {
            if (FindObjectOfType<RulesForTrueAnswer>().isImageTrueShowing)
                return;

            if (FindObjectOfType<RulesForTrueAnswer>().isVideoPlaying)
                return;

            if (isImageFalseShowing)
            {
                Debug.Log("Image is currently showing. Ignoring new incorrect answer.");
                return;
            }

            Debug.Log("Handling incorrect answer.");

            // Показать изображение неправильного ответа
            StartCoroutine(ShowIncorrectAnswer());
        }

        private IEnumerator ShowIncorrectAnswer()
        {
            CanvasPages.sortingOrder = 0;
            CanvasAnswers.sortingOrder = 1;
            CanvasVideoQuestion.sortingOrder = 0;

            // Показать изображение неправильного ответа
            string incorrectAnswerImagePath = Path.Combine(Application.streamingAssetsPath, "incorrect_answer.jpg");

            if (File.Exists(incorrectAnswerImagePath))
            {
                // Загрузить текстуру для изображения
                byte[] imageData = File.ReadAllBytes(incorrectAnswerImagePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);

                // Установить текстуру в RawImage
                incorrectAnswerImage.texture = texture;
                incorrectAnswerImage.gameObject.SetActive(true);

                // Установить флаг, что изображение показывается
                isImageFalseShowing = true;

                // Подождать 3 секунды
                yield return new WaitForSeconds(3f);

                // Скрыть изображение неправильного ответа
                incorrectAnswerImage.gameObject.SetActive(false);

                // Сбросить флаг после скрытия изображения
                isImageFalseShowing = false;

                CanvasPages.sortingOrder = 1;
                CanvasAnswers.sortingOrder = 0;
                CanvasVideoQuestion.sortingOrder = 0;
            }
            else
            {
                Debug.LogError($"Incorrect answer image not found at path: {incorrectAnswerImagePath}");
            }
        }
    }
}
