using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public int cableId = 0;

    //public ConnectionManager connectionManager;
    [SerializeField] CableStart myCableStart;
    [SerializeField] CableEnd myCableEnd;
    [SerializeField] ConnectionManager myConnectionManager;


    public Action<String, String, int> OnCableConnected;
    public Action<String,String,int> OnCableDisconnected;

    private bool cableStartConnected = false;
    private bool cableEndConnected = false;

    public string cableStartConnectedTo = null;
    public string cableEndConnectedTo = null;

    private void OnCableStartConnected(string pinConnectedTo)
    {
        cableStartConnectedTo = pinConnectedTo;
        cableStartConnected = true;
        ConnectionCheck();
    }

    private void OnCableEndConnected(string pinConnectedTo)
    {
        cableEndConnectedTo = pinConnectedTo;
        cableEndConnected = true;
        ConnectionCheck();
    }

    private void OnCableStartDisconnected(string pinConnectedTo)
    {
        cableStartConnectedTo = null;
        cableStartConnected = false;
        ConnectionCheck();
    }

    private void OnCableEndDisonnected(string pinConnectedTo)
    {
        cableEndConnectedTo = null;
        cableEndConnected = false;
        ConnectionCheck();
    }


    private void Start()
    {

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


    private void ConnectionCheck()
    {
        if (cableStartConnected & cableEndConnected)
        {
            Debug.Log("Connection Detected Between " + cableStartConnectedTo + " and " + cableEndConnectedTo + " via cable " + cableId);
            myConnectionManager.OnConnectionMade(cableStartConnectedTo, cableEndConnectedTo,cableId);
        }
        else if (cableStartConnected == false & cableEndConnected == false)
        {
            Debug.Log("Connection removed between" + cableStartConnectedTo + " and " + cableEndConnectedTo + " via cable " + cableId);
            myConnectionManager.OnConnectionRemoved(cableStartConnectedTo, cableEndConnectedTo, cableId);
        }
    }
}
