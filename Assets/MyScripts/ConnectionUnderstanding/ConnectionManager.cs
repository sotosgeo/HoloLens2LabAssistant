using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuikGraph;
using System;


public class ConnectionManager : MonoBehaviour
{
    public static float connectionTime = 3;

   

    private void Start()
    {
        CablePin cablePin = GetComponent<CablePin>();
        var motorExcitementEdges= new[] { 
            //J to Pos
            new TaggedEdge<string,string>("motorJ", "amperOut","cable") ,
            new TaggedEdge<string,string>("amperOut", "amperIn","cable"),
            new TaggedEdge<string,string>("amperOut", "resPos","cable"),
            new TaggedEdge<string,string>("resPos", "switchPosOut","cable"),
            new TaggedEdge<string,string>("switchPosOut", "switchPosIn","cable"),

            new TaggedEdge<string,string>("switchPosIn", "networkPos","cable"),
        

        //K to Neg
            new TaggedEdge<string,string>("motorK", "resGnd","cable") ,
            new TaggedEdge<string,string>("resGnd", "resNeg","cable"),
            new TaggedEdge<string,string>("resNeg", "switchNegOut","cable"),
            new TaggedEdge<string,string>("switchNegOut", "switchNegIn","cable"),
            new TaggedEdge<string,string>("switchNegIn", "networkNeg","cable"),
        };

        var motorExcitementGraph = motorExcitementEdges.ToAdjacencyGraph<string, TaggedEdge<string, string>>();

        //PrintGraph(motorExcitementGraph);
    }



    public void OnConnectionMade()
    {

    }



    void PrintGraph(AdjacencyGraph<string, TaggedEdge<string, string>> graph)
    {
        foreach (string vertex in graph.Vertices)
        {

            foreach (TaggedEdge<string, string> edge in graph.OutEdges(vertex))
            {

                Debug.Log(edge);
            }
        }
    }


    //Starting Graph edges need to be just simple string? maybe just cable for all of them
    //The Runtime graph needs to add specific cable id to each edge etc "cable1", "cable2" etc
    //Vertices -> String -> pinId
    //Edges -> int? -> cableId


}



