using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel.Design;
using TMPro;
using Mirror;
using System.Linq;
using System.Net.NetworkInformation;



public class ConnectionManager : MonoBehaviour
{

    [SerializeField] TextMeshPro connectionText;

    [SerializeField] PlacementManager placementManager;

    private List<Connection> currentConnections = new();

    private List<Connection> wrongConnections = new();
    private List<Connection> missingConnections = new();

    List<Connection> motorExcitementConnections = new();

    List<Connection> motorDrumConnections = new();

    public Action<List<Connection>, List<Connection>> OnConnectionCheck;


    public  List<string[]> motorExcitementStrPins = new()
    {

        new string[]{"Network.Pos","Switch1.PosIn" },
        new string[]{"Network.Neg","Switch1.NegIn"},

        new string[] {"Switch1.PosOut", "VariableResistance.Pos" },
        new string[] {"Switch1.NegOut", "VariableResistance.Neg" },

        new string[] { "VariableResistance.Pos", "Amperometer.In" },
        new string[] { "VariableResistance.Gnd", "Motor.K" },

        new string[] {"Amperometer.Out", "Motor.J" }

    };

    public  List<string[]> motorDrumStrPins = new()
    {

        new string[]{"networkPos","switchPosIn" },
        new string[]{"networkNeg","switchNegIn"},

        new string[] {"switchPosOut", "motorGa" },
        new string[] {"switchNegOut", "amperIn" },

        new string[] {"amperOut", "resPos" },
        new string[] {"switchPosIn", "resPos" },

        new string[] {"switchPosOut", "resNeg" },
        new string[] {"resNeg", "motorHb" },


    };




    private void Start()
    {
        currentConnections.Clear();
        wrongConnections.Clear();
        missingConnections.Clear();

        //motorExcitementConnections = CreateConnectionListFromListOfStrings(motorExcitementStrPins);
        //DebugPrintConnections(motorExcitementConnections);

        ConnectionSystem motorExcitement = new("Διέγερση Κινητήρα", motorExcitementConnections );
    }

    //private List<Connection> CreateConnectionListFromListOfStrings(List<string[]> stringList)
    //{
    //    List<Connection> connectionList = new();
    //    foreach (var conStr in stringList)
    //        foreach(var placedComponent in placementManager.placedComponentObjects)
    //        {
    //            Pin[] pins = placedComponent.GetComponentsInChildren<Pin>();
    //            foreach(var pin in pins)
    //            {
    //                if(pin.FullTag = conStr[0])
    //            }
    //        }
    //    {
    //        connectionList.Add(new Connection(conStr[0], conStr[1], 0));
    //    }

    //    return connectionList;
    //}



    public void CheckCurrentConnection()
    {
        CheckConnection(currentConnections);

        Debug.Log("Wrong connections are: ");
        DebugPrintConnections(wrongConnections);

        Debug.Log("Missing Connections are: ");
        DebugPrintConnections(missingConnections);
    }

    public void OnConnectionMade(Pin cableStart, Pin cableEnd, int cableId)
    {
        var newConnection = new Connection(cableStart, cableEnd, cableId);
        currentConnections.Add(newConnection);
        PrintConnections();

    }

    public void OnConnectionRemoved(Pin cableStart, Pin cableEnd, int cableId)
    {

        if (currentConnections.Contains(new Connection(cableStart, cableEnd, cableId)))
        {
            currentConnections.Remove(new Connection(cableStart, cableEnd, cableId));
        }

        PrintConnections();
    }

    void PrintConnections()
    {
        connectionText.text = "";
        foreach (var connection in currentConnections)
        {

            connectionText.text = connection.ToString() + '\n';
        }
    }


    void DebugPrintConnections(List<Connection> connections)
    {
        foreach (var connection in connections)
        {
            Debug.Log(connection.ToString());
        }
    }


    /// <summary>
    /// Checks current connection, in comparison to each connection system that the exercise has
    /// and returns
    /// </summary>
    /// <param name="connectionsToCheck"></param>

    public void CheckConnection(List<Connection> connectionsToCheck)
    {

        wrongConnections.Clear();
        missingConnections.Clear();

        //Find all the wrong Connections
        foreach (var connection in connectionsToCheck)
        {
            if (!motorExcitementConnections.Contains(connection))
            {
                wrongConnections.Add(connection);
            }


        }

        if (motorExcitementConnections.Any())

            //Find the missing Connections
            foreach (var connection in motorExcitementConnections)
            {
                if (!connectionsToCheck.Contains(connection))
                {
                    missingConnections.Add(connection);
                }
            }

        OnConnectionCheck?.Invoke(wrongConnections, missingConnections);
    }


}



