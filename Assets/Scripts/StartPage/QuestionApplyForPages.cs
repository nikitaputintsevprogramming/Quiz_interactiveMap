using Quiz;
using UnityEngine;
using UnityEngine.UI;

public class QuestionApplyForPages : MonoBehaviour
{
    // Массив страниц, которые будут заполнены вопросами
    [SerializeField]
    private GameObject[] pages;

    private void Awake()
    {
        // Подписка на событие KeyCode3sec
        InputManager.KeyCode3sec += ChooseQuestion;
    }

    //private void OnDestroy()
    //{
    //    // Отписка от события при уничтожении объекта
    //    InputManager.KeyCode3sec -= ChooseQuestion;
    //}

    public void ChooseQuestion(KeyCode key)
    {
        if (ShuffleQuestions.Instance != null)
        {
            // Шафлим вопросы перед распределением
            ShuffleQuestions.Instance.ShuffleQuestion();

            // Убедимся, что количество вопросов совпадает с количеством страниц
            if (ShuffleQuestions.Instance.shuffledQuestions.Count == pages.Length)
            {
                // Перебираем страницы и распределяем вопросы
                for (int i = 0; i < pages.Length; i++)
                {
                    Text textComponent = pages[i].GetComponentInChildren<Text>();
                    if (textComponent != null)
                    {
                        // Вставляем вопрос в текстовый компонент страницы
                        textComponent.text = ShuffleQuestions.Instance.shuffledQuestions[i].question;
                    }
                    else
                    {
                        Debug.LogError($"Page {i} does not have a Text component.");
                    }
                }
                InputManager.KeyCode3sec -= ChooseQuestion;
            }
            else
            {
                Debug.LogError("The number of shuffled questions does not match the number of pages.");
            }
        }
        else
        {
            Debug.LogError("ShuffleQuestions instance is not set!");
        }
    }

    public void RemoveQuestion(string questionText)  // Используем string вместо KeyCode
    {
        ShuffleQuestions.Instance.RemoveQuestion(questionText);
    }
}
