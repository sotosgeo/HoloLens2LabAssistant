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
    public static float connectionTime = 3;

    [SerializeField] TextMeshPro connectionText;

        

    List<Connection> motorExcitementConnections = new()
        {
            new Connection("networkPos","switchPosIn",0),
            new Connection("networkNeg","switchNegIn",0),

            new Connection("switchPosOut","resPos",0),
            new Connection("switchNegOut","resNeg",0),

            new Connection("resPos","amperIn",0),
            new Connection("resGnd","motorK",0),

            new Connection("amperOut","motorJ",0),


        };


    


    List<Connection> currentConnections = new();

    List<Connection> wrongConnections = new();
    List<Connection> missingConnections = new();

    List<Connection> testConnections = new()
    {
        new Connection("networkPos","switchPosIn",5), //Should be correct
        new Connection("resPos","switchPosOut",2), //Should be correct (just different order)
        new Connection("amperIn","motorJ",0), //Should be incorrect
        //all the others should be missing
    };

    private void Start()
    {
        currentConnections.Clear();
        wrongConnections.Clear();
        missingConnections.Clear();

        Debug.Log(motorExcitementConnections.Contains(new("networkPos", "switchPosIn", 3)));

        //CheckConnection(testConnections);
        
    }

    public void CheckCurrentConnection()
    {
        CheckConnection(currentConnections);


        Debug.Log("Wrong connections are: ");
        TestPrintConnections(wrongConnections);

       

        Debug.Log("Missing Connections are: ");
        TestPrintConnections(missingConnections);
    }

    public void OnConnectionMade(string cableStart, string cableEnd, int cableId)
    {
        var newConnection = new Connection(cableStart, cableEnd, cableId);
        currentConnections.Add(newConnection);
        PrintConnections();

    }

    public void OnConnectionRemoved(string cableStart, string cableEnd, int cableId)
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



