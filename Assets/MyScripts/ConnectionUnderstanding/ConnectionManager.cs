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



    [SerializeField] ConnectionSystem currentConnections;


    [SerializeField] ConnectionSystem motorExcitementConnections;

    public List<PinConnection> wrongConnections = new();
    public List<PinConnection> missingConnections = new();

    public List<string[]> motorExcitementStrPins = new()
    {

        new string[]{"Network.Pos","Switch1.PosIn" },
        new string[]{"Network.Neg","Switch1.NegIn"},

        new string[] {"Switch1.PosOut", "VariableResistance.Pos" },
        new string[] {"Switch1.NegOut", "VariableResistance.Neg" },

        new string[] { "VariableResistance.Pos", "Amperometer.In" },
        new string[] { "VariableResistance.Gnd", "Motor.K" },

        new string[] {"Amperometer.Out", "Motor.J" }

    };

    public List<string[]> motorDrumStrPins = new()
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


    private PinConnectionComparer myComparer = new();



    private void Start()
    {
        currentConnections.ClearSystem();
        missingConnections.Clear();
        wrongConnections.Clear();


    }

    public void SceneReset()
    {
        currentConnections.ClearSystem();
        missingConnections.Clear();
        wrongConnections.Clear();
    }

    [ContextMenu("Check Current System")]
    public void CheckCurrentConnection()
    {

        missingConnections.Clear();
        wrongConnections.Clear();

        //Wrong connections are the ones in current connections, not found in the correct system
        foreach (var connection in currentConnections.connections)
        {
            if (!motorExcitementConnections.connections.Contains(connection, myComparer))
            {
                wrongConnections.Add(connection);
            }

        }

        //Missing connections are the correct ones, not found in the current system
        foreach (var connection in motorExcitementConnections.connections)
        {
            if (!currentConnections.connections.Contains(connection, myComparer))
            {
                missingConnections.Add(connection);
            }
        }

        //Duplicate connections



    }





    public void OnConnectionMade(GameObject cableStart, GameObject cableEnd, Cable connectingCable)
    {
        var newConnection = new PinConnection(cableStart.GetComponent<Pin>(), cableEnd.GetComponent<Pin>(), connectingCable);
        if (!currentConnections.connections.Contains(newConnection))
        {
            currentConnections.connections.Add(newConnection);
        }
            
        
        PrintConnections();
    }

    public void OnConnectionRemoved(GameObject cableStart, GameObject cableEnd, Cable connectingCable)
    {

        if (currentConnections.connections.Contains(new PinConnection(cableStart.GetComponent<Pin>(), cableEnd.GetComponent<Pin>(), connectingCable)))
        {
            currentConnections.connections.Remove(new PinConnection(cableStart.GetComponent<Pin>(), cableEnd.GetComponent<Pin>(), connectingCable));
        }

        PrintConnections();
    }

    private void PrintConnections()
    {
        connectionText.text = "";
        foreach (var connection in currentConnections.connections)
        {

            connectionText.text += connection.ToString() + '\n';
        }
    }


    private void DebugPrintConnections(List<PinConnection> connections)
    {
        foreach (var connection in connections)
        {
            Debug.Log(connection.ToString());
        }
    }


   public void ClearConnection()
    {
        currentConnections.ClearSystem();
        PrintConnections();
    }


}



