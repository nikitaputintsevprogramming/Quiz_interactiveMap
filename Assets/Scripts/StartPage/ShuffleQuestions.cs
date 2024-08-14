using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz
{
    [System.Serializable]
    public struct QuestionEntry
    {
        public string question;  // Используем string вместо KeyCode
        public KeyCode id;  // Используем KeyCode как идентификатор
    }

    public class ShuffleQuestions : MonoBehaviour
    {
        public static ShuffleQuestions Instance;

        [SerializeField]
        public List<QuestionEntry> shuffledQuestions;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShuffleQuestion()
        {
            Debug.Log("Shuffling Questions...");

            if (Questions.Instance != null)
            {
                shuffledQuestions = ShuffleQuestionsDict(Questions.Instance.questions);

                // Optionally, you can log the shuffled questions for debugging purposes
                foreach (var question in shuffledQuestions)
                {
                    //Debug.Log($"{question.question}: {question.id}");
                }
            }
            else
            {
                Debug.LogError("Questions instance is not set!");
            }
        }

        private List<QuestionEntry> ShuffleQuestionsDict(Dictionary<string, KeyCode> originalQuestions)
        {
            System.Random random = new System.Random();

            return originalQuestions
                .OrderBy(x => random.Next())
                .Select(q => new QuestionEntry { question = q.Key, id = q.Value })
                .ToList();
        }

        public void RemoveQuestion(string questionText)  // Используем string вместо KeyCode
        {
            shuffledQuestions.RemoveAll(q => q.question == questionText);
        }
    }
}
