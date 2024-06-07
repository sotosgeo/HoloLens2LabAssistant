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
        myCableStart.OnConnectionMade += OnCableStartConnected;
        myCableEnd.OnConnectionMade += OnCableEndConnected;

        myCableStart.OnConnectionStopped += OnCableStartDisconnected;
        myCableEnd.OnConnectionStopped += OnCableEndDisonnected;

    }


    private void OnDisable()
    {
        myCableStart.OnConnectionMade -= OnCableStartConnected;
        myCableEnd.OnConnectionMade  -= OnCableEndConnected;

        myCableStart.OnConnectionStopped -= OnCableStartDisconnected;
        myCableEnd.OnConnectionStopped -= OnCableEndDisonnected;

    }


    private void ConnectionCheck()
    {

        if (cableStartConnected & cableEndConnected)
        {
            // Debug.Log("Connection Detected Between " + cableStartConnectedTo.ToString() + " and " + cableEndConnectedTo.ToString() + "  via cable " + cableId.ToString());

            if (myConnectionManager != null)
            {
                myConnectionManager.OnConnectionMade(cableStartConnectedTo, cableEndConnectedTo, this);
            }


            _cableConnected = true;
        }


        if ((cableStartConnected == false | cableEndConnected == false) & _cableConnected)
        {
            // Debug.Log("Connection removed between" + cableStartConnectedTo.ToString() + " and " + cableEndConnectedTo.ToString() + "  via cable " + cableId.ToString());

            if (myConnectionManager != null)
            {
                myConnectionManager.OnConnectionRemoved(cableStartConnectedTo, cableEndConnectedTo, this);
            }

            _cableConnected = false;
        }
    }
}
