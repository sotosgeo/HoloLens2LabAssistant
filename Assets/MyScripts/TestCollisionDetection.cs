using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestCollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Unity Events
    public float maxTime = 5f;
    public UnityEvent connected;
    public UnityEvent disconnected;

    //Property Fields
    public bool IsConnected
    {
        get;
        private set;
    }
    float currentTime;
    Coroutine timer;

    void Start()
    {
        
    }

    IEnumerator BeginTimer()
    {
        currentTime = 0;
        while (maxTime > currentTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
        }
        IsConnected = true;
        connected.Invoke();
        timer = null;
    }



    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        Debug.Log("Collision Detected with " + other.gameObject.name);
        

        timer = StartCoroutine(BeginTimer());


    }


    private void OnTriggerExit(UnityEngine.Collider other)
    {
        Debug.Log("Collision Stopped with" + other.gameObject.name);
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        else
        {
            disconnected.Invoke();
            IsConnected = false;
        }
    }



}
