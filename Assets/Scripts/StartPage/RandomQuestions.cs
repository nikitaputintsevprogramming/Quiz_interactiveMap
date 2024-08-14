using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz
{
    [System.Serializable]
    public struct QuestionEntry
    {
        public int id;
        public string question;
    }

    public class RandomQuestions : MonoBehaviour
    {
        public static RandomQuestions Instance;

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
                shuffledQuestions = ShuffleQuestions(Questions.Instance.questions);

                // Optionally, you can log the shuffled questions for debugging purposes
                foreach (var question in shuffledQuestions)
                {
                    //Debug.Log($"{question.id}: {question.question}");
                }
            }
            else
            {
                Debug.LogError("Questions instance is not set!");
            }
        }

        private List<QuestionEntry> ShuffleQuestions(Dictionary<int, string> originalQuestions)
        {
            System.Random random = new System.Random();

            return originalQuestions
                .OrderBy(x => random.Next())
                .Select(q => new QuestionEntry { id = q.Key, question = q.Value })
                .ToList();
        }

        public void RemoveQuestion(int questionId)
        {
            shuffledQuestions.RemoveAll(q => q.id == questionId);
        }
    }
}
