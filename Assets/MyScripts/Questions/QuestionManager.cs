using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    #region Dialog UI and prefabs
    [SerializeField]
    [Tooltip("Assign DialogSmall_192x96.prefab")]
    private GameObject dialogPrefabSmall;

    /// <summary>
    /// Small Dialog example prefab to display
    /// </summary>
    public GameObject DialogPrefabSmall
    {
        get => dialogPrefabSmall;
        set => dialogPrefabSmall = value;
    }
    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.OK)
        {
            OnQuestionsFinished?.Invoke();
        }

    }
    public void OpenQuestionsFinishedDialogSmall(string title, string text)
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.OK,title, text, true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    #endregion

    //TODO Subscribe to events from MultipleChoice and PartSelection
    private MultipleChoiceQuestionManager mcQuestionManager;
    private PartSelectionQuestionManager psQuestionManager;

    [SerializeField] private GameObject MultipleChoiceQuestion;
    [SerializeField] private GameObject PartSelectionQuestion;

    private bool PassedMultipleChoice = false;
    private bool PassedPartSelection = false;

    private int multipleChoiceScore;
    private int partSelectionScore;

    private int multipleChoiceQuestionNumber;
    private int partSelectionQuestionNumber;

    [SerializeField] HelpDialogHandler dialogHandler;

    public bool isStudent = false;

    public Action OnQuestionsFinished;
    private void Awake()
    {
        mcQuestionManager = MultipleChoiceQuestion.GetComponent<MultipleChoiceQuestionManager>();
        psQuestionManager = PartSelectionQuestion.GetComponent<PartSelectionQuestionManager>();
    }

    private void OnEnable()
    {

        MultipleChoiceQuestion.SetActive(true);
        PartSelectionQuestion.SetActive(false);

        if (mcQuestionManager != null)
        {
            mcQuestionManager.Finished += MultipleChoiceFinished;
        }

        if (psQuestionManager != null)
        {
            psQuestionManager.PartSelectionFinished += PartSelectionFinished;
        }
    }

    //When QuestionManager is disabled,lets reset the whole thing
    private void OnDisable()
    {
        mcQuestionManager.Finished -= MultipleChoiceFinished;
        psQuestionManager.PartSelectionFinished -= PartSelectionFinished;
        MultipleChoiceQuestion.SetActive(false);
        PartSelectionQuestion.SetActive(false);
    }


    public void SceneReset()
    {
        gameObject.SetActive(false);
        
    }



    private void MultipleChoiceFinished(bool result, int score)
    {
        multipleChoiceScore = score;
        multipleChoiceQuestionNumber = mcQuestionManager.numberOfQuestions;
        PassedMultipleChoice = result;
        if (PassedMultipleChoice)
        {
            //Display Successful Screen and contine to connection
            Debug.Log("Passed Multiple Choice");
            mcQuestionManager.Finished -= MultipleChoiceFinished;
        }

        MultipleChoiceQuestion.SetActive(false);
        PartSelectionQuestion.SetActive(true);
        psQuestionManager.enabled = true;
    }

    private void PartSelectionFinished(bool result, int score)
    {
        partSelectionScore = score;
        partSelectionQuestionNumber = psQuestionManager.numberOfQuestions;
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
            //Debug.Log("Passed Both Parts");
            OpenQuestionsFinishedDialogSmall("Επιτυχία", $"Πολλαπλής Επιλογής {multipleChoiceScore}/{multipleChoiceQuestionNumber}\n" +
                $"Αναγνώριση Μηχανής {partSelectionScore}/{partSelectionQuestionNumber}\nΠεράσατε επιτυχώς το κομμάτι των ερωτήσεων.\nΠατήστε ΟΚ για να συνεχίσετε με τη συνδεσμολογία.");
        }
        else
        {
            //Display Game over screen, and chance to start again
            OpenQuestionsFinishedDialogSmall("Αποτυχία", $"Πολλαπλής Επιλογής {multipleChoiceScore}/{multipleChoiceQuestionNumber}\n" +
                $"Αναγνώριση Μηχανής {partSelectionScore}/{partSelectionQuestionNumber}\nΔυστυχώς δεν περάσατε επιτυχώς το κομμάτι των ερωτήσεων.\nΠατήστε ΟΚ για να ξαναπροσπαθήσετε.");
            // Debug.Log("Try Again!");
        }
       
    }





}
