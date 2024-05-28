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
    [SerializeField] GameObject connectionTracking;

    private void Awake()
    {
       
    }

    // Display starting text, and choose between student and teacher mode
    void Start()
    {
        startingMenu.SetActive(true);
        questionManager.GetComponent<QuestionManager>().OnQuestionsFinished += StartConnection;


        _studenHandMenu = GameObject.Find("StudentMenu");
        _teacherHandMenu = GameObject.Find("TeacherMenu");

        _studenHandMenu.SetActive(false);
        _teacherHandMenu.SetActive(false);

        connectionTracking.SetActive(false);
    }


    public void StartConnection()
    {
        helpDialogHandler.OpenCustomOKDialogue("Διέγερση Κινητήρα", "Ξεκινήστε τη συνδεσμολογία της διέγερσης κινητήρα συνεχούς ρεύματος");
        connectionTracking.SetActive(true);
    }

    public void Student()
    {
        _studenHandMenu.SetActive(true);
        questionManager.SetActive(true);
        startingMenu.SetActive(false);
    }

    public void Teacher()
    {
        _teacherHandMenu.SetActive(true);
        startingMenu.SetActive(false);
        connectionTracking.SetActive(true);
    }


}
