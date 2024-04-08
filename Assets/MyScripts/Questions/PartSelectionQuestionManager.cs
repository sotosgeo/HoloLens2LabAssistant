using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PartSelectionQuestionManager : MonoBehaviour
{
    //TODO Implement score calculation and part selection

    public List<PartSelectionData> partSelectionQuestions;
    [SerializeField] private GameObject PartSelectionAnswerCollection;
    [SerializeField] private TextMeshPro questionTitle;
    
    private PartSelectionData currentPartQuestion;
    private int submittedAnswerIndex;
    private int correctAnswerChoice;
    private bool isCorrect = false;

  
    [SerializeField] private float percentageToBeat = 0.75f;
    private int correctAnswersScore = 0;
    private int numberOfQuestions;


    private bool result =false;


    public delegate void FinishHandler(bool result);
    public event FinishHandler Finished;

    private void Awake()
    {
        GetQuestionAssets();
    }

    void Start()
    {
        SelectNewQuestion();
    }

    private void GetQuestionAssets()
    {
        partSelectionQuestions = new List<PartSelectionData>(Resources.LoadAll<PartSelectionData>("Questions/PartSelection"));
        numberOfQuestions = partSelectionQuestions.Count;
        Debug.Log("Questions Selected");
    }

    private void CalculateScore()
    {
        if (correctAnswersScore / numberOfQuestions >= percentageToBeat) result = true;
        else result = false;
    }

    private void SelectNewQuestion()
    {

        //Pick a question at random from the List
        int randomQuestionIndex = UnityEngine.Random.Range(0, partSelectionQuestions.Count);
        currentPartQuestion = partSelectionQuestions[randomQuestionIndex];
        partSelectionQuestions.RemoveAt(randomQuestionIndex);
        questionTitle.text = currentPartQuestion.question;


    }
    private void OnQuestionsFinished()
    {
        CalculateScore();
        Finished?.Invoke(result);
        Console.WriteLine("Done with Part Selection Questions");
        gameObject.SetActive(false);
    }

    //When Button is Pressed:
    //Update questions and check if answer is correct
    public void OnAnswerSubmission()
    {

        submittedAnswerIndex = PartSelectionAnswerCollection.GetComponent<InteractableToggleCollection>().CurrentIndex;
        if (submittedAnswerIndex == currentPartQuestion.correctAnswerIndex) isCorrect = true;
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

        if (partSelectionQuestions.Count == 0)
        {
            OnQuestionsFinished();
        }
        else
        {
            SelectNewQuestion();
        }


    }



}
