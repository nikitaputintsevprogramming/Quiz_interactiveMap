using System.Collections;
using UnityEngine;

namespace Quiz
{
    public class QuestionsManager
    {
        public void ChooseQuestion()
        {
            int randomIndex = Random.Range(0, RandomQuestions.Instance.shuffledQuestions.Count);
            string choosenQuestion = RandomQuestions.Instance.shuffledQuestions[randomIndex].ToString();
            Debug.Log("ChoosenQuestion: " + choosenQuestion);
            RemoveAnswer(randomIndex);
        }

        public void RemoveAnswer(int randomIndex)
        {
            RandomQuestions.Instance.shuffledQuestions.Remove(randomIndex);
        }
    }
}