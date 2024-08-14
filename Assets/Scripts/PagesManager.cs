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
            string currentQuestion = FindObjectOfType<Page>().gameObject.GetComponentInChildren<Text>().text;
            Debug.Log($"Current question: {currentQuestion}");

            if (Questions.Instance.questions.TryGetValue(currentQuestion, out KeyCode correctAnswer))
            {
                if (keyCode == correctAnswer)
                {
                    Debug.Log("Correct answer!");
                    OnCorrectAnswer?.Invoke(currentQuestion);
                }
                else
                {
                    Debug.Log("Incorrect answer!");
                    OnIncorrectAnswer?.Invoke();
                }
            }
            else
            {
                Debug.LogError($"Current question text '{currentQuestion}' does not exist in the questions dictionary.");
            }
        }
    }
}
