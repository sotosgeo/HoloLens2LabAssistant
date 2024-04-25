using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuikGraph;
using System;


public class ConnectionManager : MonoBehaviour
{
    public static float connectionTime = 3;

    private Dictionary<String, String[]> motorExcitementDict = new()
    {
        //Network
        { "networkPos", new[]{"switchPosIn"}},
        { "networkNeg", new[]{"switchnNegIn"} },
        //Switch
        { "switchPosIn", new[]{"networkPos", "switchPosOut"} },
        { "switchNegIn" , new[]{"networkNeg, switchNegOut"}},
        { "switchPosOut" , new[]{"switchPosIn, resPos"}},
        { "switchNegOut" , new[]{"switchNegIn", "resNeg"}},
        //VariableResistance
        { "resPos" , new[]{"switchPosOut","amperIn"}},
        { "resGnd" , new[]{"resNeg","motorK"}},
        { "resNeg" , new[]{"resGnd","switchNegOut"}},
        //Amperometer
        { "amperIn" , new[]{"amperOut","resPos" }},
        { "amperOut" , new[]{"amperIn","motorJ" }},
        //Motor
        { "motorJ" , new[]{"amperOut"}},
        { "motorK" , new[]{"motorK"}}

    };
    



    private void Start()
    {

        
        var motorExcitementGraph = motorExcitementDict.ToDelegateVertexAndEdgeListGraph(kv => Array.ConvertAll(kv.Value, v => new TaggedEdge<String,String>(kv.Key, v,"cable")));
       
        
       


        
        


        PrintBidirectionalGraph(motorExcitementGraph);
       
    }



    void PrintBidirectionalGraph(DelegateVertexAndEdgeListGraph<string, TaggedEdge<string,string>> graph)
    {
        foreach (string vertex in graph.Vertices)
        {
            
            foreach (TaggedEdge<string,string> edge in graph.OutEdges(vertex))
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



