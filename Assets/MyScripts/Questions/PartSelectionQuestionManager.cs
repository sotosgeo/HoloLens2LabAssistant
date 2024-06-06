using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PartSelectionQuestionManager : MonoBehaviour
{

    [SerializeField] HelpDialogHandler helpDialogHandler;
    public List<PartSelectionData> partSelectionQuestions;
    private List<PartSelectionData> currentQuestions;
    [SerializeField] private GameObject PartSelectionAnswerCollection;
    [SerializeField] private TextMeshPro questionTitle;
    
    private PartSelectionData currentPartQuestion;
    private int submittedAnswerIndex;
    private bool isCorrect = false;

  
    [SerializeField] private float percentageToBeat = 0.75f;
    private int correctAnswersScore = 0;
    private int numberOfQuestions;
    private bool result =false;

    

    public Action<bool> PartSelectionFinished;

    public bool isStudent = false;

    private void Awake()
    {
        partSelectionQuestions = new List<PartSelectionData>(Resources.LoadAll<PartSelectionData>("Questions/PartSelection"));
        Debug.Log(partSelectionQuestions.Count.ToString() + " Questions Loaded");
    }


    private void OnEnable()
    {
        helpDialogHandler.SetHelpText("Επιλέξτε το εξάρτημα της μηχανής που ζητείται.\nΤο επιλεγμένο εξάρτημα φαίνεται με πορτοκαλί χρώμα. \nΥποβάλετε την απάντηση σας με το κουμπί Υποβολή");
        helpDialogHandler.OpenHelpDialogSmall();
        GetQuestionAssets();
        SelectNewQuestion();
    }


    private void OnDisable()
    {
        correctAnswersScore = 0;   
    }


    private void GetQuestionAssets()
    {
        currentQuestions = new List<PartSelectionData>(partSelectionQuestions);
        numberOfQuestions = currentQuestions.Count;
        Debug.Log( numberOfQuestions.ToString() + " Questions copied");
    }

    private void CalculateScore()
    {
        if (correctAnswersScore / numberOfQuestions >= percentageToBeat) result = true;
        else result = false;
    }

    private void SelectNewQuestion()
    {
        
        //Pick a question at random from the List
        int randomQuestionIndex = UnityEngine.Random.Range(0, currentQuestions.Count);
        currentPartQuestion = currentQuestions[randomQuestionIndex];
        currentQuestions.RemoveAt(randomQuestionIndex);
        questionTitle.text = currentPartQuestion.question;


    }
    private void OnQuestionsFinished()
    {
        CalculateScore();
        PartSelectionFinished?.Invoke(result);
        Console.WriteLine("Done with Part Selection Questions");
    }

    //When Button is Pressed:
    //Update questions and check if answer is correct
    public void OnAnswerSubmission()
    {

        submittedAnswerIndex = PartSelectionAnswerCollection.GetComponent<InteractableToggleCollection>().CurrentIndex;
        if (submittedAnswerIndex == currentPartQuestion.correctAnswerIndex) isCorrect = true;
        else isCorrect = false;
        Debug.Log("Remaining Questions are" + currentQuestions.Count.ToString());

        if (isCorrect)
        {
            correctAnswersScore++;
            Debug.Log("Correct Answer");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        if (currentQuestions.Count == 0)
        {
            OnQuestionsFinished();
        }
        else
        {
            SelectNewQuestion();
        }


    }



}
