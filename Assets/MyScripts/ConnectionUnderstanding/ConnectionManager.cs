using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel.Design;
using TMPro;
using Mirror;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.MixedReality.Toolkit.UI;


public class ConnectionManager : MonoBehaviour
{
    public static float connectionTime = 3;

    [SerializeField] TextMeshPro connectionText;

    [SerializeField] PlacementManager placementManager;



    List<Connection> currentConnections = new();

    List<Connection> wrongConnections = new();
    List<Connection> missingConnections = new();

    List<Connection> motorExcitementConnections = new();


    List<string[]> motorExcitement = new List<string[]>
    {

        new string[]{"networkPos","switchPosIn" },
        new string[]{"networkNeg","switchNegIn"},

        new string[] {"switchPosOut", "resPos" },
        new string[] {"switchNegOut", "resNeg" },
        
        new string[] {"resPos", "amperIn" },
        new string[] {"resGnd", "motorK" },

        new string[] {"amperOut", "motorJ" }

    };

    private void Start()
    {
        currentConnections.Clear();
        wrongConnections.Clear();
        missingConnections.Clear();

        CreateConnectionFromList();
        TestPrintConnections(motorExcitementConnections);

    }

    private void CreateConnectionFromList()
    {
        foreach (var conStr in motorExcitement)
        {
            motorExcitementConnections.Add(new Connection(GameObject.Find(conStr[0]), GameObject.Find(conStr[1]), 0));
            //Debug.Log(GameObject.Find(conStr[0]).ToString() + " "+ GameObject.Find(conStr[1]).ToString());
        }
    }



    public void CheckCurrentConnection()
    {
        CheckConnection(currentConnections);


        Debug.Log("Wrong connections are: ");
        TestPrintConnections(wrongConnections);



        Debug.Log("Missing Connections are: ");
        TestPrintConnections(missingConnections);
    }

    public void OnConnectionMade(GameObject cableStart, GameObject cableEnd, int cableId)
    {
        var newConnection = new Connection(cableStart, cableEnd, cableId);
        currentConnections.Add(newConnection);
        PrintConnections();

    }

    public void OnConnectionRemoved(GameObject cableStart, GameObject cableEnd, int cableId)
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

            connectionText.text += connection.ToString() + '\n';
        }



    }


    void TestPrintConnections(List<Connection> connections)
    {
        foreach (var connection in connections)
        {
            Debug.Log(connection.ToString());
        }
    }

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
    }


}



