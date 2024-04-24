using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material connectionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;

    public GameObject pinConnectedTo;
    Coroutine collisionTimer;
    public string pinId;

    private IEnumerator CollisionTimer(Collider other)
    {
        yield return new WaitForSeconds(ConnectionManager.connectionTime);
        pinVisual.GetComponent<MeshRenderer>().material = connectionFinalizedMaterial;
        Debug.Log("Collision Finalized with " + other.gameObject.GetComponent<Pin>().pinId);
        pinConnectedTo = other.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When a collision is detected between this and another Collider 
        Debug.Log("Collision Detected with " + other.gameObject.GetComponent<Pin>().pinId);
        collisionTimer = StartCoroutine(CollisionTimer(other));
        if (pinVisual != null)
        {
            pinVisual.GetComponent<MeshRenderer>().material = connectionDetectedMaterial;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision Stopped with" + other.gameObject.GetComponent<Pin>().pinId);
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        if (collisionTimer != null)
        {
            StopCoroutine(collisionTimer);
            collisionTimer = null;
        }
        pinConnectedTo = null;
    }

}
