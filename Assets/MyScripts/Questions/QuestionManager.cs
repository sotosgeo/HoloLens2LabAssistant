using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    //TODO Subscribe to events from MultipleChoice and PartSelection
    private MultipleChoiceQuestionManager mcQuestionManager;
    private PartSelectionQuestionManager psQuestionManager;

    [SerializeField] private GameObject MultipleChoiceQuestion;
    [SerializeField] private GameObject PartSelectionQuestion;

    private bool PassedMultipleChoice = false;
    private bool PassedPartSelection = false;


    private void Awake()
    {
        mcQuestionManager = MultipleChoiceQuestion.GetComponent<MultipleChoiceQuestionManager>();
        psQuestionManager = PartSelectionQuestion.GetComponent<PartSelectionQuestionManager>();
    }

    private void MultipleChoiceFinished(bool result)
    {
        PassedMultipleChoice = result;
        if (PassedMultipleChoice)
        {
            //Display Successful Screen and contine to connection
            Debug.Log("Passed Multiple Choice");
            mcQuestionManager.Finished -= MultipleChoiceFinished;
        }
        
        MultipleChoiceQuestion.SetActive(false);
        PartSelectionQuestion.SetActive(true);
    }

    private void PartSelectionFinished(bool result)
    {
        PassedPartSelection = result;
        if (PassedPartSelection)
        {
            //Display Successful Screen and contine to connection
            Debug.Log("Passed Part Selection");
            mcQuestionManager.Finished -= PartSelectionFinished;
        }


        PartSelectionQuestion.SetActive(false);
        CalculateFinalScore();
    }


    private void CalculateFinalScore()
    {
        if (PassedMultipleChoice & PassedPartSelection)
        {
            //Display Successful Screen and contine to connection
            Debug.Log("Passed Both Parts");
        }
        else
        {
            //Display Game over screen, and chance to start again
            Debug.Log("Try Again!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mcQuestionManager != null)
        {
            mcQuestionManager.Finished += MultipleChoiceFinished;
        }

        if (psQuestionManager != null)
        {
            psQuestionManager.Finished += PartSelectionFinished;
        }


        //Start by enabling the MultipleChoiceQuestions;
        //Replace with an event that starts when the user accesses this part of the app
        MultipleChoiceQuestion.SetActive(true);
        PartSelectionQuestion.SetActive(false);


    }

    private void OnDisable()
    {
        MultipleChoiceQuestion.SetActive(false);
        PartSelectionQuestion.SetActive(false);
    }


}
