using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class MultipleChoiceQuestionManager : MonoBehaviour
{
    /* Responsible For:
    - Updating Question and answet Text in UI
    - Getting QuestionData from Resources/Questions folder
    - Checking if answer is correct and keeping a score
    */

    [SerializeField] private TextMeshPro questionText;
    [SerializeField] private TextMeshPro questionTitle;
    private List<MultipleChoiceData> multipleChoiceQuestions;

    private MultipleChoiceData currentQuestion;


    [SerializeField] private GameObject multipleChoiceAnswerCollection;
    [SerializeField] private List<TextMesh> multipleChoiceAnswerFields;
    private int submittedAnswerIndex;
    private bool isCorrect = false;
    private int correctAnswersScore = 0;
    public float percentageToBeat = 0.75f;
    private int numberOfQuestions;


    private bool result = false;


    public delegate void FinishHandler(bool result);
    public event FinishHandler Finished;



    private void Awake()
    {
        
        GetQuestionAssets();
    }  
    

    void Start()
    {
        
        SelectNewQuestion();
        SetAnswerValues();

    }


    private void GetQuestionAssets()
    {
        multipleChoiceQuestions = new List<MultipleChoiceData>(Resources.LoadAll<MultipleChoiceData>("Questions/MultipleChoice"));
        numberOfQuestions = multipleChoiceQuestions.Count;
    }

    private void SelectNewQuestion()
    {
        //Pick a question at random from the List
        int randomQuestionIndex = UnityEngine.Random.Range(0, multipleChoiceQuestions.Count);
        currentQuestion = multipleChoiceQuestions[randomQuestionIndex];
        multipleChoiceQuestions.RemoveAt(randomQuestionIndex);
        questionText.text = currentQuestion.question;
        
           
    }

    //Update UI of Question
    private void SetAnswerValues()
    {
        //List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        for (int i = 0; i < multipleChoiceAnswerFields.Count; i++)
        {
            multipleChoiceAnswerFields[i].text = currentQuestion.answers[i];

        }


    }

    private void CalculateScore()
    {

        if (correctAnswersScore / numberOfQuestions >= percentageToBeat) result = true;
        else result = false;

    }

    //When Button is Pressed:
    //Update questions and check if answer is correct
    public void OnAnswerSubmission()
    {

        submittedAnswerIndex = multipleChoiceAnswerCollection.GetComponent<InteractableToggleCollection>().CurrentIndex;
        if (submittedAnswerIndex == currentQuestion.correctAnswerIndex) isCorrect = true;
        else isCorrect = false;


        if (isCorrect)
        {
            correctAnswersScore++;
            Debug.Log("Correct Answer");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        if (multipleChoiceQuestions.Count == 0)
        {

            OnQuestionsFinished();
        }
        else
        {
            SelectNewQuestion();
            SetAnswerValues();
        }

    }

    private void OnQuestionsFinished()
    {
        CalculateScore();
        Finished?.Invoke(result);
        Console.WriteLine("Done with Multiple Choice Questions");
        //gameObject.SetActive(false);
    }

}
