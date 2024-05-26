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
        startingMenu.SetActive(true);
    }

    // Display starting text, and choose between student and teacher mode
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Student()
    {

    }


}
