using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuikGraph;
using System;
using System.ComponentModel.Design;


public class ConnectionManager : MonoBehaviour
{
    public static float connectionTime = 3;

    //Knows the correct Connection, and 

    //AdjacencyGraph<string, TaggedEdge<string, string>> currentGraph = new AdjacencyGraph<string, TaggedEdge<string, string>>();


    public class Connection : IEquatable<Connection>
    {

        public string PinA {  get; private set; }
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
            return PinA + " - > " + PinB + "via cable" + ConnectingCable.ToString();
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
        
        
        //var motorExcitementEdges = new[] { 
        //    //J to Pos
        //    new TaggedEdge<string,string>("motorJ", "amperOut","cable") ,
        //    new TaggedEdge<string,string>("amperOut", "amperIn","cable"),
        //    new TaggedEdge<string,string>("amperOut", "resPos","cable"),
        //    new TaggedEdge<string,string>("resPos", "switchPosOut","cable"),
        //    new TaggedEdge<string,string>("switchPosOut", "switchPosIn","cable"),

        //    new TaggedEdge<string,string>("switchPosIn", "networkPos","cable"),
        

        ////K to Neg
        //    new TaggedEdge<string,string>("motorK", "resGnd","cable") ,
        //    new TaggedEdge<string,string>("resGnd", "resNeg","cable"),
        //    new TaggedEdge<string,string>("resNeg", "switchNegOut","cable"),
        //    new TaggedEdge<string,string>("switchNegOut", "switchNegIn","cable"),
        //    new TaggedEdge<string,string>("switchNegIn", "networkNeg","cable"),
        //};

        //var motorExcitementGraph = motorExcitementEdges.ToAdjacencyGraph<string, TaggedEdge<string, string>>();

        //PrintGraph(motorExcitementGraph);





    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
       
    }

    public void OnConnectionMade(string cableStart, string cableEnd, int cableId)
    {
        //var newEdge = new TaggedEdge<string, string>(cableStart, cableEnd, "cable" + cableId);
        var newConnection = new Connection(cableStart, cableEnd, cableId);
        currentConnections.Add(newConnection);
        PrintConnections();
        //currentGraph.AddVerticesAndEdge(newEdge);
        //PrintGraph(currentGraph);
    }

    public void OnConnectionRemoved(string cableStart, string cableEnd, int cableId)
    {
        //currentGraph.RemoveEdge(new TaggedEdge<string, string>(cableStart, cableEnd, "cable" + cableId));
        if(currentConnections.Contains(new Connection(cableStart,  cableEnd, cableId)))
        {
            currentConnections.Remove(new Connection(cableStart, cableEnd, cableId));
        }
        PrintConnections();
        //PrintGraph(currentGraph);
        
    }

    //void PrintGraph(AdjacencyGraph<string, TaggedEdge<string, string>> graph)
    //{
    //    foreach (string vertex in graph.Vertices)
    //    {

    //        foreach (TaggedEdge<string, string> edge in graph.OutEdges(vertex))
    //        {

    //            Debug.Log(edge);
    //        }
    //    }
    //}
    
    void PrintConnections()
    {
        foreach (var connection in currentConnections)
        {
            Debug.Log(connection.ToString());
        }
    }
    //Starting Graph edges need to be just simple string? maybe just cable for all of them
    //The Runtime graph needs to add specific cable id to each edge etc "cable1", "cable2" etc
    //Vertices -> String -> pinId
    //Edges -> int? -> cableId


}



