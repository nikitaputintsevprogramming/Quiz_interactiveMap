using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz
{
    public class RandomQuestions : MonoBehaviour
    {
        public Dictionary<int, string> shuffledQuestions;

        public void ShuffleQuestion()
        {
            Debug.Log("Start");

            if (Questions.Instance != null)
            {
                shuffledQuestions = ShuffleQuestions(Questions.Instance.questions);

                // Пример вывода перемешанных вопросов в консоль
                foreach (var question in shuffledQuestions)
                {
                    Debug.Log($"{question.Key}: {question.Value}");
                }
            }
            else
            {
                Debug.LogError("Questions instance is not set!");
            }
        }

        public Dictionary<int, string> ShuffleQuestions(Dictionary<int, string> originalQuestions)
        {
            System.Random random = new System.Random();
            return originalQuestions
                .OrderBy(x => random.Next())
                .ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
