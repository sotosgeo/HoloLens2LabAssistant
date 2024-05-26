using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeftyConnections;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public int cableId = 0;

    
    [SerializeField] CablePin myCableStart;
    [SerializeField] CablePin myCableEnd;
    [SerializeField] ConnectionManager myConnectionManager;
    [SerializeField] ConnectionSystemComponent myConnectionSystem;

  

    private bool cableStartConnected = false;
    private bool cableEndConnected = false;

    public GameObject cableStartConnectedTo = null;
    public GameObject cableEndConnectedTo = null;

    private bool _cableConnected = false;


    private void OnCableStartConnected(GameObject pinConnectedTo)
    {
        cableStartConnectedTo = pinConnectedTo;
        cableStartConnected = true;
        ConnectionCheck();
    }

    private void OnCableEndConnected(GameObject pinConnectedTo)
    {
        cableEndConnectedTo = pinConnectedTo;
        cableEndConnected = true;
        ConnectionCheck();
    }

    private void OnCableStartDisconnected(GameObject pinConnectedTo)
    { 
        cableStartConnected = false;
        ConnectionCheck();
        cableStartConnectedTo = null;
    }

    private void OnCableEndDisonnected(GameObject pinConnectedTo)
    {
        cableEndConnected = false;
        ConnectionCheck();
        cableEndConnectedTo = null;
    }


    private void OnEnable()
    {
        myCableStart.OnConnectionFinalized += OnCableStartConnected;
        myCableEnd.OnConnectionFinalized += OnCableEndConnected;

        myCableStart.OnConnectionStopped += OnCableStartDisconnected;
        myCableEnd.OnConnectionStopped += OnCableEndDisonnected;

    }


    private void OnDisable()
    {
        myCableStart.OnConnectionFinalized -= OnCableStartConnected;
        myCableEnd.OnConnectionFinalized -= OnCableEndConnected;

        myCableStart.OnConnectionStopped -= OnCableStartDisconnected;
        myCableEnd.OnConnectionStopped -= OnCableEndDisonnected;

    }
    private void Update()
    {
        
    }


    private void ConnectionCheck()
    {
       
        if (cableStartConnected & cableEndConnected)
        {
            Debug.Log("Connection Detected Between " + cableStartConnectedTo.GetComponent<Port>().FullTag + " and " + cableEndConnectedTo.GetComponent<Port>().FullTag + "  via cable " + cableId.ToString());
            //myConnectionManager.OnConnectionMade(cableStartConnectedTo, cableEndConnectedTo,cableId);
            myConnectionSystem.ConnectionSystem.Connect(cableStartConnectedTo.GetComponent<Port>(), cableEndConnectedTo.GetComponent<Port>(), cableId);
            //Set the connection between the cable ends at each pin
           
            _cableConnected = true;
        }


        if ((cableStartConnected == false | cableEndConnected == false) & _cableConnected)
        {
            Debug.Log("Connection removed between" + cableStartConnectedTo.GetComponent<Port>().FullTag + " and " + cableEndConnectedTo.GetComponent<Port>().FullTag + "  via cable " + cableId.ToString());
            //myConnectionManager.OnConnectionRemoved(cableStartConnectedTo, cableEndConnectedTo, cableId);
            myConnectionSystem.ConnectionSystem.Disconnect(cableStartConnectedTo.GetComponent<Port>(), cableEndConnectedTo.GetComponent<Port>(), cableId);
            _cableConnected = false;
        }
    }
}
