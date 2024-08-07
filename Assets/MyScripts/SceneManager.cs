﻿using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    //UI Stuff

    private GameObject _studenHandMenu;
    private GameObject _teacherHandMenu;
    [SerializeField] GameObject startingMenu;
    [SerializeField] HelpDialogHandler helpDialogHandler;

    //Questions Objects

    [SerializeField] GameObject questionManagerObject;
    private QuestionManager questionManager;
    [SerializeField] GameObject motorQuestions;

    //Tracking Objects
    [SerializeField] ArucoTracker arucoTracker;
    [SerializeField] MediaCapturer mediaCapturer;
   




    [SerializeField] PlacementManager placementManager;


    private void Awake()
    {
        arucoTracker.enabled = true;
        mediaCapturer.enabled = true;

        questionManager = questionManagerObject.GetComponent<QuestionManager>();
        questionManager.OnQuestionsFinished += StartStudentConnection;

        _studenHandMenu = GameObject.Find("StudentMenu");
        _teacherHandMenu = GameObject.Find("TeacherMenu");
    }


   
    // Display starting text, and choose between student and teacher mode
    void Start()
    {


        startingMenu.SetActive(true);

        _studenHandMenu.SetActive(false);
        _teacherHandMenu.SetActive(false);

        questionManagerObject.SetActive(false);
        questionManager.enabled = false;

        placementManager.ChangeManipulationAndVisualization(false);

        
    }

    public void SceneReset()
    {
        startingMenu.SetActive(true);
        _studenHandMenu.SetActive(false);
        _teacherHandMenu.SetActive(false);

        questionManager.SceneReset();


        //arucoTracker.enabled = false;
        

        placementManager.ChangeManipulationAndVisualization(false);
        placementManager.ChangeDirectionalIndicator(false);
        placementManager.ChangeTooltip(false);


        motorQuestions.SetActive(false);
     
    }



    public void Student()
    {
        _studenHandMenu.SetActive(true);
        
        //Setting question manager to active, resets the question part
        questionManager.enabled = true;
        questionManagerObject.SetActive(true);

        startingMenu.SetActive(false);
        placementManager.ChangeManipulationAndVisualization(false);


        motorQuestions.GetComponent<Collider>().enabled = false;
        motorQuestions.GetComponent<ObjectManipulator>().enabled = false;
    }

    public void Teacher()
    {

        _teacherHandMenu.SetActive(true);
        startingMenu.SetActive(false);
        StartTeacherConnection();
        motorQuestions.GetComponent<PartSelectionQuestionManager>().enabled = false;
        motorQuestions.SetActive(true);
        motorQuestions.GetComponent<Collider>().enabled = true;
        motorQuestions.GetComponent<ObjectManipulator>().enabled = true;


    }
    public void StartStudentConnection()
    {
        helpDialogHandler.OpenCustomOKDialogue("Διέγερση Κινητήρα", "Ξεκινήστε τη συνδεσμολογία της διέγερσης κινητήρα συνεχούς ρεύματος");
        arucoTracker.enabled = true;
        

    }

    public void StartTeacherConnection()
    {
        arucoTracker.enabled = true;
        
        helpDialogHandler.OpenCustomOKDialogue("Διέγερση Κινητήρα", "Τοποθετήστε τα ψηφιακά εξαρτήματα ακριβώς πάνω στα πραγματικά.\n Τοποθετήστε το μοντέλο της μηχανης πιο πάνω από το πραγματικό");
        placementManager.ChangeManipulationAndVisualization(true);
    }

}
