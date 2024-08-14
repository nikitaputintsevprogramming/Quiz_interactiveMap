using Quiz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace UI.Pagination
{
    public class PagesManager : MonoBehaviour
    {
        // События для правильного и неправильного ответа
        public event System.Action<KeyCode> OnCorrectAnswer;
        public event System.Action<KeyCode> OnIncorrectAnswer;

        public void SubcribeOnWaitDownedKey()
        {
            // Подписываемся на событие удержания клавиши в течение 3 секунд
            InputManager.KeyCode3sec += OnKeyCodeHeldFor3Seconds;
        }

        public void UnsubcribeOnWaitDownedKey()
        {
            // Отписываемся от события, чтобы избежать утечек памяти
            InputManager.KeyCode3sec -= OnKeyCodeHeldFor3Seconds;
        }

        // Этот метод будет вызван, когда клавиша удерживается 3 секунды
        public void OnKeyCodeHeldFor3Seconds(KeyCode keyCode)
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

        public void CheckCorrectKey(KeyCode keyCode)
        {
            //Debug.Log($"Key {keyCode} was down in PagesManager");
            //string currentQuestion = gameObject.GetComponentInChildren<Text>().text = keyCode.ToString();
            //KeyCode correctAnswer = Questions.Instance.questions.TryGetValue(currentQuestionText);

            string currentQuestion = FindObjectOfType<Page>().gameObject.GetComponentInChildren<Text>().text;
            Debug.Log("EEEEEEEEEEEEE currentQuestion" + FindObjectOfType<Page>().gameObject.name);

            if (Questions.Instance.questions.TryGetValue(currentQuestion, out KeyCode correctAnswer))
            {
                if (keyCode == correctAnswer)
                {
                    Debug.Log("Correct answer!");
                    OnCorrectAnswer?.Invoke(keyCode);
                }
                else
                {
                    Debug.Log("Incorrect answer!");
                    OnIncorrectAnswer?.Invoke(keyCode);
                }
            }
            else
            {
                Debug.LogError("Current question text does not exist in the questions dictionary." + currentQuestion);
            }
        }
    }
}
