using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public int cableId = 0;

    
    [SerializeField] CablePin myCableStart;
    [SerializeField] CablePin myCableEnd;
    [SerializeField] ConnectionManager myConnectionManager;


    public Action<String, String, int> OnCableConnected;
    public Action<String,String,int> OnCableDisconnected;

    private bool cableStartConnected = false;
    private bool cableEndConnected = false;

    public string cableStartConnectedTo = null;
    public string cableEndConnectedTo = null;

    private bool _cableConnected = false;


    private void OnCableStartConnected(GameObject pinConnectedTo)
    {
        cableStartConnectedTo = pinConnectedTo.GetComponent<Pin>().pinId;
        cableStartConnected = true;
        ConnectionCheck();
    }

    private void OnCableEndConnected(GameObject pinConnectedTo)
    {
        cableEndConnectedTo = pinConnectedTo.GetComponent<Pin>().pinId;
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
            Debug.Log("Connection Detected Between " + cableStartConnectedTo + " and " + cableEndConnectedTo + "  via cable " + cableId.ToString());
            myConnectionManager.OnConnectionMade(cableStartConnectedTo, cableEndConnectedTo,cableId);
            _cableConnected = true;
        }


        if ((cableStartConnected == false | cableEndConnected == false) & _cableConnected)
        {
            Debug.Log("Connection removed between" + cableStartConnectedTo + " and " + cableEndConnectedTo + "  via cable " + cableId.ToString());
            myConnectionManager.OnConnectionRemoved(cableStartConnectedTo, cableEndConnectedTo, cableId);
            _cableConnected = false;
        }
    }
}
