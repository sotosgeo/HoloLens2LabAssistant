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
        string component1 = "switch1";
        string component2 = "switch2";
        var motorExcitementGraph = new BidirectionalGraph<string, TaggedEdge<string,string>>();
        motorExcitementGraph.AddVertex(component1);
        motorExcitementGraph.AddVertex(component2);
        var edge1 = new TaggedEdge<string, string>(component1, component2, "cable1");
        motorExcitementGraph.AddEdge(edge1);

        PrintGraph(motorExcitementGraph);
    }



    void PrintGraph(BidirectionalGraph<string, TaggedEdge<string,string>> graph)
    {
        foreach (string vertex in graph.Vertices)
        {
            Debug.Log(vertex);
            foreach (TaggedEdge<string,string> edge in graph.InEdges(vertex))
            {
                Debug.Log(edge);
                Debug.Log(edge.Tag);
            }
        }
    }
    //Starting Graph edges need to be just simple string? maybe just cable for all of them
    //The Runtime graph needs to add specific cable id to each edge etc "cable1", "cable2" etc
    //Vertices -> String -> pinId
    //Edges -> int? -> cableId


}



