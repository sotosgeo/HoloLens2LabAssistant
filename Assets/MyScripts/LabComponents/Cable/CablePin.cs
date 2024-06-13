using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class CablePin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material connectionFinalizedMaterial;

    [SerializeField] MarkerFollower myFollower;


    [SerializeField] Mesh connectionDetectedShape;
    [SerializeField] Mesh defaultShape;

    public GameObject pinConnectedTo = null;

    
    public Action<GameObject> OnConnectionMade;
    public Action<GameObject> OnConnectionStopped;

    private float distanceFromPort;

    private void Start()
    {
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        
    }


    private void Update()
    {
        //If not connected then keep trying to find it
        if(pinConnectedTo == null)
        {
            myFollower.enabled = true;
        }
        //If its connected get the distance of the user from the port that its connected to
        else if (pinConnectedTo != null)
        {
            distanceFromPort = Vector3.Distance(Camera.main.transform.position, pinConnectedTo.transform.position);
            
            //If the distance is bigger than something, turn off the follower
            if (distanceFromPort <= 0.5f)
            {
                myFollower.enabled = true;
            }
            else
            {
                myFollower.enabled = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject.ToString() + "collided with " + other.gameObject.ToString());
        pinConnectedTo = other.gameObject;
        pinVisual.GetComponent<MeshRenderer>().material = connectionFinalizedMaterial;
        pinVisual.GetComponent<MeshFilter>().mesh = connectionDetectedShape;
        OnConnectionMade?.Invoke(pinConnectedTo);
        
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.gameObject.ToString() + "stop colliding with" + other.gameObject.ToString());

        OnConnectionStopped?.Invoke(pinConnectedTo);
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        pinVisual.GetComponent<MeshFilter>().mesh = defaultShape;
        pinConnectedTo = null;
    }

}
