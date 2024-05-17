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


    public class Connection : IEquatable<Connection>
    {

        public string PinA { get; private set; }
        public string PinB { get; private set; }
        public int ConnectingCable { get; private set; }


        public Connection(string pinA, string pinB, int connectingCable)
        {
            PinA = pinA;
            PinB = pinB;
            ConnectingCable = connectingCable;
        }

        public override string ToString()
        {
            return PinA.ToString() + " - > " + PinB.ToString() + " via cable " + ConnectingCable.ToString();
        }


        public bool Equals(Connection other)
        {
            return other != null && GetType() == other.GetType() && ((PinA == other.PinA && PinB == other.PinB) || (PinA == other.PinB && PinB == other.PinA));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Connection);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PinA, PinB);
        }
    }


   


    List<Connection> motorExcitementConnections = new List<Connection>()
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

       

        CheckConnection(testConnections);

        Debug.Log("Wrong Connections are \n");

        TestPrintConnections(wrongConnections);

        Debug.Log("Missing Connections are \n");

        TestPrintConnections(missingConnections);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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
        //Find all the wrong Connections
        foreach (var connection in connectionsToCheck)
        {
            if (!motorExcitementConnections.Contains(connection))
            {
                wrongConnections.Add(connection);
            }



        }

        if(motorExcitementConnections.Any())

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



