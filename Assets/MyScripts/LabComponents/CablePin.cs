using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material connectionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;

    public GameObject pinConnectedTo = null;
 

    public Action<GameObject> OnConnectionFinalized;
    public Action<GameObject> OnConnectionStopped;


    private void Start()
    {
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

   



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject.ToString() + "collided with " + other.gameObject.ToString());
        pinConnectedTo = other.gameObject;
        pinVisual.GetComponent<MeshRenderer>().material = connectionFinalizedMaterial;
        
        OnConnectionFinalized?.Invoke(pinConnectedTo);
        
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.gameObject.ToString() + "stop colliding with" + other.gameObject.ToString());

        OnConnectionStopped?.Invoke(pinConnectedTo);
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

}
