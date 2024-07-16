using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MistakeHandler : MonoBehaviour
{

    [SerializeField] ConnectionManager connectionManager;
    [SerializeField] PlacementManager placementManager;
    [SerializeField] CableReset cablesObject;

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
           "\nΠάνω από τους ακροδέκτες που πρέπει να συνδεθούν για να ολοκληρωθεί η συνδεσμολογία, υπάρχει ο ίδιος αριθμός.", true);
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
        Dialog.Open(DialogPrefabSmall, DialogButtonType.OK, "Συγχαρητήρια!", $"Ολοκληρώσατε επιτυχώς την άσκηση! Ζητήθηκε βοήθεια {timesAskedForHelp} φορές από τη συσκευή", true);
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

    private void OnClosedConnectionClearDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            connectionManager.ClearConnections();
            cablesObject.ResetCables();
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
        //Level 1 Help -> Enable the visualization of the wrong components
        foreach (var pinConnection in connectionManager.wrongConnections)
        {
            if (pinConnection != null)
            {
                //Turn on the Visualization of that component parent
                pinConnection.PinA.parentComponent.transform.GetChild(0).gameObject.SetActive(true);
                pinConnection.PinB.parentComponent.transform.GetChild(0).gameObject.SetActive(true);
            }

            //Turn on the Directional Indicator of that component
            pinConnection.PinA.parentComponent.transform.GetChild(3).gameObject.SetActive(true);
            pinConnection.PinA.parentComponent.transform.GetChild(3).gameObject.SetActive(true);
        }

        foreach (var pinConnection in connectionManager.missingConnections)
        {
            //Enable the visualization of the wrong components
            if (pinConnection != null)
            {

                pinConnection.PinA.parentComponent.transform.GetChild(0).gameObject.SetActive(true);
                pinConnection.PinB.parentComponent.transform.GetChild(0).gameObject.SetActive(true);
            }


            //Turn on the Directional Indicator of that component
            pinConnection.PinA.parentComponent.transform.GetChild(3).gameObject.SetActive(true);
            pinConnection.PinA.parentComponent.transform.GetChild(3).gameObject.SetActive(true);

        }


    }

    private void HelpLevel2()
    {
        OpenHelp2Dialog();
        int missingConnCount = 0;
        //Level 3 Help - Highlight the wrong connections with the same red color
        //For the missing connections, add a number atop each pin, the same for each wrong connection


        foreach (var pinConnection in connectionManager.missingConnections)
        {
            missingConnCount++;
            pinConnection.PinA.pinVisual.SetActive(true);
            pinConnection.PinA.pinText.text += missingConnCount.ToString();

            pinConnection.PinB.pinVisual.SetActive(true);
            pinConnection.PinB.pinText.text += missingConnCount.ToString();

            // missingMatIndex++;

            // if (missingMatIndex > numOfMissingConnections) missingMatIndex = 0;
        }

        foreach (var pinConnection in connectionManager.wrongConnections)
        {
            pinConnection.PinA.pinVisual.SetActive(true);
            pinConnection.PinA.pinVisual.GetComponent<MeshRenderer>().material = wrongPinMaterial;

            pinConnection.PinB.pinVisual.SetActive(true);
            pinConnection.PinB.pinVisual.GetComponent<MeshRenderer>().material = wrongPinMaterial;
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

    public void ShowMistakes()
    {
        timesAskedForHelp++;

        
        ClearHelpVisualisation();
        //Level 0 Mistakes
        numOfWrongConnections = connectionManager.wrongConnections.Count;
        numOfMissingConnections = connectionManager.missingConnections.Count;

     
        

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



    private void ClearHelpVisualisation()
    {
        placementManager.ChangeTooltip(false);
        placementManager.ChangeDirectionalIndicator(false);
        placementManager.ChangeComponentVisualization(false);
        placementManager.ClearPinText();
    }

    public void ClearConnectionPrompt()
    {
        Dialog myDialog = Dialog.Open(DialogPrefabSmall, DialogButtonType.Yes | DialogButtonType.No, "Καθαρισμός Σύνδεσης", "Είστε σίγουροι ότι θέλετε να καθαρίσετε τη σύνδεση;", true);
        if (myDialog != null)
        {
            myDialog.OnClosed += OnClosedConnectionClearDialogEvent;
        }
    }


    void Start()
    {
        timesAskedForHelp = 0;
        helpLevel = 0;
    }



}
