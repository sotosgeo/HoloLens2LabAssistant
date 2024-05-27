using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SceneManager : MonoBehaviour
{

    [SerializeField] GameObject questionManager;
    [SerializeField] GameObject startingMenu;
    [SerializeField] HelpDialogHandler helpDialogHandler;

    private void Awake()
    {
       
    }

    // Display starting text, and choose between student and teacher mode
    void Start()
    {
        startingMenu.SetActive(true);
    }

   


    public void Student()
    {
        questionManager.SetActive(true);
        startingMenu.SetActive(false);
    }

    public void Teacher()
    {
        startingMenu.SetActive(false);
    }


}
