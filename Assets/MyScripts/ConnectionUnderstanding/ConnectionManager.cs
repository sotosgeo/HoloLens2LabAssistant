using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel.Design;


public class ConnectionManager : MonoBehaviour
{
    public static float connectionTime = 3;

    //Knows the correct Connection, and 

    //AdjacencyGraph<string, TaggedEdge<string, string>> currentGraph = new AdjacencyGraph<string, TaggedEdge<string, string>>();


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
            return PinA.ToString() + " - > " + PinB.ToString() + "via cable" + ConnectingCable.ToString();
        }


        public bool Equals(Connection other)
        {
            return other != null && GetType() == other.GetType() && PinA == other.PinA && PinB == other.PinB;
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
            new Connection("motorJ","amperOut",0),

        };

    List<Connection> currentConnections = new();
    private void Start()
    {

        currentConnections.Clear();

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            PrintConnections();
        }
    }

    public void OnConnectionMade(string cableStart, string cableEnd, int cableId)
    {
        var newConnection = new Connection(cableStart, cableEnd, cableId);
        currentConnections.Add(newConnection);
        

    }

    public void OnConnectionRemoved(string cableStart, string cableEnd, int cableId)
    {

        if (currentConnections.Contains(new Connection(cableStart, cableEnd, cableId)))
        {
            currentConnections.Remove(new Connection(cableStart, cableEnd, cableId));
        }
        

    }


    void PrintConnections()
    {
        foreach (var connection in currentConnections)
        {
            Debug.Log(connection.ToString());
        }
    }


}



