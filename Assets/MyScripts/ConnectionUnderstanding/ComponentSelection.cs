using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSelection : MonoBehaviour
{

    public Dictionary<string, string> motorExcitementComponents = new()
    {
        { "network","" },
        { "switch" ,"" },
        { "variableRes","" },
        { "amperometer","" },
        { "motor" ,""}
    };



    public Dictionary<string, string> motorDrumComponents = new()
    {
        { "network","" },
        { "switch1" ,"" },
        { "variableRes2","" },
        { "switch2" ,""},
        { "amperometer","" },
        { "motor" ,""}
    };


    

}
