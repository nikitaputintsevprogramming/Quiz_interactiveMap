using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UI.Pagination;

namespace Quiz
{
    [System.Serializable]
    public struct QuestionEntry
    {
        public Texture2D question;  // Используем Texture2D вместо string
        public KeyCode id;          // Используем KeyCode как идентификатор
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
            if(FindObjectOfType<PagedRect>().CurrentPage != 1)
            {
                return;
            }

            Debug.Log("Shuffling Questions...");

            if (Questions.Instance != null)
            {
                shuffledQuestions = ShuffleQuestionsDict(Questions.Instance.questions);

                // Optionally, you can log the shuffled questions for debugging purposes
                foreach (var question in shuffledQuestions)
                {
                    //Debug.Log($"{question.question.name}: {question.id}");
                }
            }
            else
            {
                Debug.LogError("Questions instance is not set!");
            }
        }

        private List<QuestionEntry> ShuffleQuestionsDict(Dictionary<Texture2D, KeyCode> originalQuestions)
        {
            System.Random random = new System.Random();

            return originalQuestions
                .OrderBy(x => random.Next())
                .Select(q => new QuestionEntry { question = q.Key, id = q.Value })
                .ToList();
        }

        public void RemoveQuestion(Texture2D questionTexture)
        {
            shuffledQuestions.RemoveAll(q => q.question == questionTexture);
        }
    }
}
