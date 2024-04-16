using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    private MeshRenderer pinVisualRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material collisionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;


    public GameObject cableConnectedTo = null;

    float currentTime;
    Coroutine timer;
    public float maxTime = 5f;

    private IEnumerator CollisionTimer(Collider other)
    {
        yield return new WaitForSeconds(maxTime);
        pinVisual.GetComponent<MeshRenderer>().material = connectionFinalizedMaterial;
        Debug.Log("Collision Finalized with " + other.gameObject.name);
        cableConnectedTo = other.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When a collision is detected between this and another Collider 
        Debug.Log("Collision Detected with " + other.gameObject.name);
        timer = StartCoroutine(CollisionTimer(other));
        if (pinVisual != null)
        {
            pinVisual.GetComponent<MeshRenderer>().material = collisionDetectedMaterial;
        }
       

        

    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision Stopped with" + other.gameObject.name);
        pinVisual.GetComponent<MeshRenderer>().material = defaultMaterial;
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        cableConnectedTo = null;
    }


}
