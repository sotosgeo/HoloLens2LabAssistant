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



    public Action<bool> PartSelectionFinished;

    private void Awake()
    {
        GetQuestionAssets();
    }

    void Start()
    {
        SelectNewQuestion();
    }

    private void OnEnable()
    {
        helpDialogHandler.SetHelpText("Επιλέξτε το εξάρτημα της μηχανής που ζητείται.\nΤο επιλεγμένο εξάρτημα φαίνεται με πορτοκαλί χρώμα. \nΥποβάλετε την απάντηση σας με το κουμπί Υποβολή");
        helpDialogHandler.OpenHelpDialogSmall();
    }



    private void GetQuestionAssets()
    {
        partSelectionQuestions = new List<PartSelectionData>(Resources.LoadAll<PartSelectionData>("Questions/PartSelection"));
        numberOfQuestions = partSelectionQuestions.Count;
        Debug.Log( numberOfQuestions.ToString() + " Questions Selected");
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
        PartSelectionFinished?.Invoke(result);
        Console.WriteLine("Done with Part Selection Questions");
        //gameObject.SetActive(false);
    }

    //When Button is Pressed:
    //Update questions and check if answer is correct
    public void OnAnswerSubmission()
    {

        submittedAnswerIndex = PartSelectionAnswerCollection.GetComponent<InteractableToggleCollection>().CurrentIndex;
        if (submittedAnswerIndex == currentPartQuestion.correctAnswerIndex) isCorrect = true;
        else isCorrect = false;
        Debug.Log("Remaining Questions are" + partSelectionQuestions.Count.ToString());

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
