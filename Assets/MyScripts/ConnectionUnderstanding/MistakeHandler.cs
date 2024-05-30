using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MistakeHandler : MonoBehaviour
{

    [SerializeField] ConnectionManager connectionManager;


    private int helpLevel = 0;
    private int numOfWrongConnections = 0;
    private int numOfMissingConnections = 0;
    private int timesAskedForHelp;


    #region Dialog UI and prefabs
    [SerializeField]
    [Tooltip("Assign DialogLarge_192x192.prefab")]
    private GameObject dialogPrefabLarge;

    /// <summary>
    /// Large Dialog example prefab to display
    /// </summary>
    public GameObject DialogPrefabLarge
    {
        get => dialogPrefabLarge;
        set => dialogPrefabLarge = value;
    }

    [SerializeField]
    [Tooltip("Assign DialogMediume_192x128.prefab")]
    private GameObject dialogPrefabMedium;

    /// <summary>
    /// Medium Dialog example prefab to display
    /// </summary>
    public GameObject DialogPrefabMedium
    {
        get => dialogPrefabMedium;
        set => dialogPrefabMedium = value;
    }

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





    public void OpenHelp0Dialog()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Βοήθεια", $"Υπάρχουν {numOfWrongConnections} λάθος συνδεδεμένα καλώδια. \nΛείπουν {numOfMissingConnections} συνδέσεις.\nΧρειάζεσαι περισσότερη βοήθεια;", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenHelp1Dialog()
    {
        string help1Text = "";
        Dialog myDialog = Dialog.Open(DialogPrefabMedium, DialogButtonType.Yes | DialogButtonType.No, "Βοήθεια", help1Text + "\nΧρειάζεσαι περισσότερη βοήθεια;", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenHelp2Dialog()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Help", $"Mistakes Outlined blah blah \nDo you need more help?", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenCorrectDialogSmall()
    {
        Dialog.Open(DialogPrefabSmall, DialogButtonType.OK, "Help", "There are no mistakes so far.\nPlease continue with the exercise", true);
    }


    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            helpLevel++;
            GetHelp(helpLevel);
        }
        if (obj.Result == DialogButtonType.No)
        {
            helpLevel = 0;

        }
    }
    #endregion

    private void HelpLevel0()
    {
        OpenHelp0Dialog();

    }


    private void HelpLevel1()
    {

        OpenHelp1Dialog();
    }

    private void HelpLevel2()
    {
        OpenHelp2Dialog();

    }



    private void GetHelp(int myHelpLevel)
    {
        switch (myHelpLevel)
        {
            case 0:
                HelpLevel0();
                break;
            case 1:
                HelpLevel1();
                break;
            case 2:
                HelpLevel2();
                break;
            default:
                helpLevel = 0;
                break;
        };
    }


    public void FindMistakes()
    {

        timesAskedForHelp++;
        //Level 1 Mistakes
        numOfWrongConnections = connectionManager.wrongConnections.Count;
        numOfMissingConnections = connectionManager.missingConnections.Count;

        List<GameObject> wrongComponents = new();
        //Level 2 Mistakes

        

        //Level 3 Mistakes


        if (numOfWrongConnections > 0 | numOfMissingConnections > 0)
        {
            GetHelp(helpLevel);
        }
        else
        {
            OpenCorrectDialogSmall();
        }



    }



    private GameObject GetComponentFromPin(GameObject pin)
    {
        return pin.transform.parent.parent.gameObject;
    }


    void Start()
    {
        timesAskedForHelp = 0;
        helpLevel = 0;
    }

    //private void OnEnable()
    //{
    //    connectionManager.OnConnectionCheck += FindMistakes;
    //}

    //private void OnDisable()
    //{
    //    connectionManager.OnConnectionCheck -= FindMistakes;
    //}

}
