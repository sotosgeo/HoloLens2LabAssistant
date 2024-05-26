using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MultipleChoice", menuName = "ScriptableObjects/Question/MultipleChoice", order = 1)]

public class MultipleChoiceData : ScriptableObject
{
    public string question;
    [Tooltip("Correct answer first, and randomized next")]
    public string[] answers;
    public int correctAnswerIndex;



    public override string ToString()
    {
        return $"Question: {question} \n Answers: {answers}";
    }
}

 

