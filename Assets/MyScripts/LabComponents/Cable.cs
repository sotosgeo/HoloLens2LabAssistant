using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public int cableId = 0;
    public Pin pinStart;
    public Pin pinEnd;

    public GameObject pinConnectedTo;


    Coroutine collisionTimer;
    private IEnumerator CollisionTimer(Collider other)
    {
        yield return new WaitForSeconds(ConnectionManager.connectionTime);
        Debug.Log("Collision Finalized with " + other.gameObject.name);
        pinConnectedTo = other.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When a collision is detected between this and another Collider 
        Debug.Log("Collision Detected with " + other.gameObject.name);
        collisionTimer = StartCoroutine(CollisionTimer(other));
        //if (pinVisual != null)
        //{
        //    pinVisual.GetComponent<MeshRenderer>().material = collisionDetectedMaterial;
        //}
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision Stopped with" + other.gameObject.name);
        //pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        if (collisionTimer != null)
        {
            StopCoroutine(collisionTimer);
            collisionTimer = null;
        }
        pinConnectedTo = null;
    }



}
