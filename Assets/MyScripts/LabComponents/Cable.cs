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

    private bool cableStartConnected = false;
    private bool cableEndConnected = false;

    public string cableStartConnectedTo = null;
    public string cableEndConnectedTo = null;

    private void OnCableStartConnected(string pinConnectedTo)
    {
        cableStartConnectedTo = pinConnectedTo;
        cableStartConnected = true;
        CheckForConnections.Invoke();
    }

    private void OnCableEndConnected(string pinConnectedTo) {
        cableEndConnectedTo = pinConnectedTo;
        cableEndConnected = true;
        CheckForConnections.Invoke();
    }


    private Action CheckForConnections;


    private void Start()
    {
        
    }

    private void OnEnable()
    {
        myCableStart.OnConnectionFinalized += OnCableStartConnected;
        myCableEnd.OnConnectionFinalized += OnCableEndConnected;
        CheckForConnections += ConnectionCheck;

    }

    

    private void OnDisable()
    {
        myCableStart.OnConnectionFinalized -= OnCableStartConnected;
        myCableEnd.OnConnectionFinalized -= OnCableEndConnected;
        CheckForConnections -= ConnectionCheck;
    }


    private void ConnectionCheck()
    {
        if(cableStartConnected & cableEndConnected)
        {
            Debug.Log("Connection Detected Between " + cableStartConnectedTo + " and " + cableEndConnectedTo + " via cable " + cableId);
        }
    }
}
