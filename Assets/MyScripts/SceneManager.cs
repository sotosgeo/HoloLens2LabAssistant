using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SceneManager : MonoBehaviour
{

    //UI Stuff

    private GameObject _studenHandMenu;
    private GameObject _teacherHandMenu;



    //Questions Objects

    [SerializeField] GameObject questionManager;
    [SerializeField] GameObject startingMenu;
    [SerializeField] HelpDialogHandler helpDialogHandler;


    //Tracking Objects
    [SerializeField] GameObject connectionManagerObject;
    [SerializeField] GameObject cableTrackingObject;
    [SerializeField] PlacementManager placementManager;

    // Display starting text, and choose between student and teacher mode
    void Start()
    {
        startingMenu.SetActive(true);
        questionManager.GetComponent<QuestionManager>().OnQuestionsFinished += StartStudentConnection;


        _studenHandMenu = GameObject.Find("StudentMenu");
        _teacherHandMenu = GameObject.Find("TeacherMenu");

        _studenHandMenu.SetActive(false);
        _teacherHandMenu.SetActive(false);

        cableTrackingObject.SetActive(false);
        placementManager.ChangeManipulationAndVisualization(false);
    }

    public void SceneReset()
    {
        startingMenu.SetActive(true);
        _studenHandMenu.SetActive(false);
        _teacherHandMenu.SetActive(false);
        questionManager.GetComponent<QuestionManager>().Reset();
        cableTrackingObject.SetActive(false);
        placementManager.ChangeManipulationAndVisualization(false);
    }

   

    public void Student()
    {
        _studenHandMenu.SetActive(true);

        //Setting question manager to active, resets the question part
        questionManager.SetActive(true);
        startingMenu.SetActive(false);
        placementManager.ChangeManipulationAndVisualization(false);
    }

    public void Teacher()
    {
        _teacherHandMenu.SetActive(true);
        startingMenu.SetActive(false);
        StartTeacherConnection();
        
    }
    public void StartStudentConnection()
    {
        helpDialogHandler.OpenCustomOKDialogue("Διέγερση Κινητήρα", "Ξεκινήστε τη συνδεσμολογία της διέγερσης κινητήρα συνεχούς ρεύματος");
        cableTrackingObject.SetActive(true);
        
    }

    public void StartTeacherConnection()
    {
        cableTrackingObject.SetActive(true);
        placementManager.ChangeManipulationAndVisualization(true);
    }

}
