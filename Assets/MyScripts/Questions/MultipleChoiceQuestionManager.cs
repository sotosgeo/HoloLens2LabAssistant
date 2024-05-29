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
    private List<MultipleChoiceData> currentQuestions;
    private MultipleChoiceData currentQuestion;


    [SerializeField] private GameObject multipleChoiceAnswerCollection;
    [SerializeField] private List<TextMesh> multipleChoiceAnswerFields;
    private int submittedAnswerIndex;
    private bool isCorrect = false;
    private int correctAnswersScore = 0;
    public float percentageToBeat = 0.75f;
    private int numberOfQuestions;

    private int currentQuestionNumberForTitle = 0;
    private bool result = false;


    public delegate void FinishHandler(bool result);
    public event FinishHandler Finished;


    public HelpDialogHandler helpDialogHandler;


    private void Awake()
    {
        multipleChoiceQuestions = new List<MultipleChoiceData>(Resources.LoadAll<MultipleChoiceData>("Questions/MultipleChoice"));
        Debug.Log(multipleChoiceQuestions.Count.ToString() + " questions loaded");
        
        
    }

  
    private void OnEnable()
    {
        GetQuestionAssets();
        correctAnswersScore = 0;
        helpDialogHandler.SetHelpText("Επιλέξτε την μοναδική σωστή απάντηση.\nΥποβάλετε την απάντηση σας με το κουμπί Υποβολή");
        helpDialogHandler.OpenHelpDialogSmall();
        SelectNewQuestion();
        SetAnswerValues();
       

    }

    private void OnDisable()
    {
       correctAnswersScore = 0;
    }


    private void GetQuestionAssets()
    {
        currentQuestions = new List<MultipleChoiceData>(multipleChoiceQuestions);
        numberOfQuestions = currentQuestions.Count;
        Debug.Log(currentQuestions.Count.ToString() + " questions copied");
        currentQuestionNumberForTitle = 0;

    }

    private void SelectNewQuestion()
    {
        currentQuestionNumberForTitle++;
        //Pick a question at random from the List
        int randomQuestionIndex = UnityEngine.Random.Range(0, currentQuestions.Count);
        currentQuestion = currentQuestions[randomQuestionIndex];
        currentQuestions.RemoveAt(randomQuestionIndex);
        questionText.text = currentQuestion.question;
        questionTitle.text = $"Ερώτηση  {currentQuestionNumberForTitle}";
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

        if (currentQuestions.Count == 0)
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
    }

}
