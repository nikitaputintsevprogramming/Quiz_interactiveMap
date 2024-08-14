using Quiz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Pagination;

namespace Quiz
{
    public class PagesManager : MonoBehaviour
    {
        public delegate void CorrectAnswerHandler(string value);
        public delegate void IncorrectAnswerHandler();

        // События для правильного и неправильного ответа
        public event CorrectAnswerHandler OnCorrectAnswer;
        public event IncorrectAnswerHandler OnIncorrectAnswer;

        public void SubcribeOnWaitDownedKey()
        {
            InputManager.KeyCode3sec += OnKeyCodeHeldFor3Seconds;
        }

        public void UnsubcribeOnWaitDownedKey()
        {
            InputManager.KeyCode3sec -= OnKeyCodeHeldFor3Seconds;
        }

        // Этот метод будет вызван, когда клавиша удерживается 3 секунды
        private void OnKeyCodeHeldFor3Seconds(KeyCode keyCode)
        {
            Debug.Log($"Key {keyCode} was held for 3 seconds in PagesManager");
            FindObjectOfType<PagedRect>().NextPage();
        }

        public void SubcribeOnKey()
        {
            InputManager.KeyCodeDown += CheckCorrectKey;
        }

        public void UnsubcribeOnKey()
        {
            InputManager.KeyCodeDown -= CheckCorrectKey;
        }

        private void CheckCorrectKey(KeyCode keyCode)
        {
            // Получаем текущую страницу
            Page currentPage = FindObjectOfType<Page>();

            // Получаем Image компонент из текущей страницы
            Image imageComponent = currentPage.gameObject.GetComponentInChildren<Image>();

            if (imageComponent != null)
            {
                // Получаем текстуру из Image компонента
                Texture2D currentTexture = imageComponent.sprite.texture;

                // Проверяем, существует ли текстура в словаре вопросов
                if (Questions.Instance.questions.TryGetValue(currentTexture, out KeyCode correctAnswer))
                {
                    if (keyCode == correctAnswer)
                    {
                        Debug.Log("Correct answer!");
                        OnCorrectAnswer?.Invoke(currentTexture.name);
                    }
                    else
                    {
                        Debug.Log("Incorrect answer!");
                        OnIncorrectAnswer?.Invoke();
                    }
                }
                else
                {
                    Debug.LogError($"Current texture '{currentTexture.name}' does not exist in the questions dictionary.");
                    FindObjectOfType<PagedRect>().NextPage();

                }
            }
            else
            {
                Debug.LogError("No Image component found on the current page.");
            }
        }
    }
}
