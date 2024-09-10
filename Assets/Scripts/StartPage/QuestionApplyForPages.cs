using Quiz;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TextureAtlas;

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

    private void OnDestroy()
    {
        // Отписка от события при уничтожении объекта
        InputManager.KeyCode3sec -= ChooseQuestion;
    }

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
                    Image imageComponent = pages[i].GetComponentInChildren<Image>();
                    if (imageComponent != null)
                    {
                        string textureName = ShuffleQuestions.Instance.shuffledQuestions[i].question.name;
                        // Вставляем спрайт в Image компонент страницы
                        imageComponent.sprite = Sprite.Create(
                            ShuffleQuestions.Instance.shuffledQuestions[i].question,
                            new Rect(0, 0, ShuffleQuestions.Instance.shuffledQuestions[i].question.width, ShuffleQuestions.Instance.shuffledQuestions[i].question.height),
                            new Vector2(0.5f, 0.5f)
                        );

                        imageComponent.sprite.name = textureName;
                    }
                    //Text textObj = pages[i].GetComponentInChildren<Text>();
                    //if (textObj != null)
                    //{
                    //    textObj.text = ShuffleQuestions.Instance.shuffledQuestions[i].question.GetInstanceID().ToString();
                    //    //Questions.Instance.questions.TryGetValue(currentTexture, out KeyCode correctAnswer)
                    //}
                    else
                    {
                        Debug.LogError($"Page {i} does not have an Image component.");
                    }
                }
                // Отписка от события после применения вопросов
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

    public void RemoveQuestion(Texture2D questionTexture)
    {
        ShuffleQuestions.Instance.RemoveQuestion(questionTexture);
    }
}
