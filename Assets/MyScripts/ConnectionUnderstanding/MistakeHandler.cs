using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MistakeHandler : MonoBehaviour
{

    [SerializeField] ConnectionManager connectionManager;
    [SerializeField] PlacementManager placementManager;

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


    [SerializeField] Material[] missingPinMaterials;
    [SerializeField] Material wrongPinMaterial;
    #endregion

    public void OpenHelp0Dialog()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Βοήθεια", $"{SingleOrMultipleMistakesText()}\n{SingleOrMultipleMissingText()}\nΧρειάζεσαι περισσότερη βοήθεια;", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenHelp1Dialog()
    {

        Dialog myDialog = Dialog.Open(DialogPrefabMedium, DialogButtonType.Yes | DialogButtonType.No, "Βοήθεια", $"{SingleOrMultipleMistakesText()}\n{SingleOrMultipleMissingText()}\nΤα σημέια της σύνδεσης που είναι λάθος ή όπου λείπουν συνδέσεις επισημαίνονται με ταμπέλες.\nΧρειάζεσαι περισσότερη βοήθεια;", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenHelp2Dialog()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabMedium, DialogButtonType.OK, "Βοήθεια", $"{SingleOrMultipleMistakesText()}\n{SingleOrMultipleMissingText()}\nΟι λάθος συνδέσεις φαίνονται με κόκκινο χρώμα." +
           "\nΟι συνδέσεις που λείπουν φαίνονται με το ίδιο χρώμα", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedDialogEvent;
        }
    }

    public void OpenCorrectDialogSmall()
    {
        Dialog.Open(DialogPrefabSmall, DialogButtonType.OK, "Βοήθεια", "Δεν υπάρχει κανένα λάθος μέχρι στιγμής.\nΣυνεχίστε με την άσκηση.", true);
    }

    public void OpenFinishedDialog()
    {
        Dialog.Open(DialogPrefabSmall, DialogButtonType.OK, "Συγχαρητήρια!", "Ολοκληρώσατε επιτυχώς την άσκηση!", true);
    }

    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            helpLevel++;
            GetHelp(helpLevel);
        }

        if (obj.Result == DialogButtonType.OK || obj.Result == DialogButtonType.No)
        {
            helpLevel = 0;

        }
    }


    private void HelpLevel0()
    {
        //Level 0 Help -> Just show number of wrong and missing connections
        OpenHelp0Dialog();
    }


    private void HelpLevel1()
    {

        OpenHelp1Dialog();
        //Level 1 Help -> Enable the tooltips of the wrong components
        foreach (var pinConnection in connectionManager.wrongConnections)
        {

            //Turn on the tooltip of that component
            pinConnection.PinA.parent.transform.GetChild(2).gameObject.SetActive(true);
            pinConnection.PinB.parent.transform.GetChild(2).gameObject.SetActive(true);


            //Turn on the Directional Indicator of that component
            pinConnection.PinA.parent.transform.GetChild(3).gameObject.SetActive(true);
            pinConnection.PinA.parent.transform.GetChild(3).gameObject.SetActive(true);
        }

        foreach (var pinConnection in connectionManager.missingConnections)
        {
            //Turn on the Tooltip of that component
            pinConnection.PinA.parent.transform.GetChild(2).gameObject.SetActive(true);
            pinConnection.PinB.parent.transform.GetChild(2).gameObject.SetActive(true);

            //Turn on the Directional Indicator of that component
            pinConnection.PinA.parent.transform.GetChild(3).gameObject.SetActive(true);
            pinConnection.PinA.parent.transform.GetChild(3).gameObject.SetActive(true);

        }


    }

    private void HelpLevel2()
    {
        OpenHelp2Dialog();
        int missingMatIndex = 0;

        //Level 3 Help - Highlight the wrong connections with the same red color, and missing connections with the same random color
        foreach (var pinConnection in connectionManager.wrongConnections)
        {
            pinConnection.PinA.GetComponentInChildren<MeshRenderer>().enabled = true;
            pinConnection.PinA.GetComponentInChildren<MeshRenderer>().material = wrongPinMaterial;

            pinConnection.PinB.GetComponentInChildren<MeshRenderer>().enabled = true;
            pinConnection.PinB.GetComponentInChildren<MeshRenderer>().material = wrongPinMaterial;
        }

        foreach (var pinConnection in connectionManager.missingConnections)
        {
            pinConnection.PinA.GetComponentInChildren<MeshRenderer>().enabled = true;
            pinConnection.PinA.GetComponentInChildren<MeshRenderer>().material = missingPinMaterials[missingMatIndex];

            pinConnection.PinB.GetComponentInChildren<MeshRenderer>().enabled = true;
            pinConnection.PinB.GetComponentInChildren<MeshRenderer>().material = missingPinMaterials[missingMatIndex];

            missingMatIndex++;
            if(missingMatIndex > numOfMissingConnections) missingMatIndex = 0;
        }


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


    private string SingleOrMultipleMistakesText()
    {
        if (numOfWrongConnections == 0)
        {
            return "Δεν υπάρχει κανένα λάθος συνδεδεμένο καλώδιο";
        }
        else if (numOfWrongConnections == 1)
        {
            return $"Υπάρχει {numOfWrongConnections} λάθος συνδεδεμένο καλώδιο.";
        }
        else return $"Υπάρχουν {numOfWrongConnections} λάθος συνδεδεμένα καλώδια.";
    }

    private string SingleOrMultipleMissingText()
    {
        if (numOfMissingConnections == 0)
        {
            return "Δεν λείπει καμία σύνδεση";
        }

        else if (numOfMissingConnections == 1)
        {
            return $"Λείπει {numOfMissingConnections} σύνδεση.";
        }
        else return $"Λείπουν {numOfMissingConnections} συνδέσεις.";
    }

    public void FindMistakes()
    {
        placementManager.ChangeTooltip(false);
        placementManager.ChangeDirectionalIndicator(false);
        timesAskedForHelp++;


        //Level 0 Mistakes
        numOfWrongConnections = connectionManager.wrongConnections.Count;
        numOfMissingConnections = connectionManager.missingConnections.Count;

        placementManager.ChangePinVisualization(false);
        //Level 1 Mistakes -> Enable their tooltip components
        //Its done in HelpLevel 2

        //Level 3 Mistakes


        if (numOfWrongConnections > 0 | numOfMissingConnections > 0)
        {
            GetHelp(helpLevel);
        }
        else if ((numOfMissingConnections == 0) & (numOfWrongConnections == 0))
        {
            OpenFinishedDialog();
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



}
