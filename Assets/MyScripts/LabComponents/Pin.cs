using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material collisionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;
    public string pinId;

    Coroutine collisionTimer;
    


    //Starts a timer when OnTriggerEnter is triggered that makes the connection
    //after a certain connectionTime has passed
    private IEnumerator CollisionTimer(Collider other)
    {
        yield return new WaitForSeconds(ConnectionManager.connectionTime);
        pinVisual.GetComponent<MeshRenderer>().material = connectionFinalizedMaterial;
        Debug.Log("Collision Finalized with " + other.gameObject.name);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //When a collision is detected between this and another Collider 
        Debug.Log("Collision Detected with " + other.gameObject.name);
        collisionTimer = StartCoroutine(CollisionTimer(other));
        if (pinVisual != null)
        {
            pinVisual.GetComponent<MeshRenderer>().material = collisionDetectedMaterial;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision Stopped with" + other.gameObject.name);
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        if (collisionTimer != null)
        {
            StopCoroutine(collisionTimer);
            collisionTimer = null;
        }
       
    }


}
