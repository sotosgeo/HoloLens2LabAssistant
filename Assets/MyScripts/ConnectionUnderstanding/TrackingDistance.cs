using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackingDistance : MonoBehaviour
{


    public GameObject motor;
    public GameObject network;
    public TextMeshPro motorDistanceText;
    public TextMeshPro networkDistanceText;

    [SerializeField] ArucoTracker arucoTracker;
    private float networkFromCamera;
    private float motorFromCamera;
    
    private void Start()
    {
        
    }


    void Update()
    {
        //cameraPlane.SetNormalAndPosition(Camera.main.transform.forward, Camera.main.transform.position);
        motorFromCamera = Mathf.Abs(Vector3.Distance(Camera.main.transform.position ,  motor.transform.position));
        networkFromCamera = Mathf.Abs(Vector3.Distance(Camera.main.transform.position , network.transform.position));
        //myText.text = distanceFromCamera.ToString();

        motorDistanceText.text = "Distance from Motor: " + motorFromCamera.ToString();
        networkDistanceText.text = "Distance from Network: " + networkFromCamera.ToString();


        if ((motorFromCamera <= 1) | (networkFromCamera <= 1))
        {
            arucoTracker.enabled = true;
        }
        else
        {
            arucoTracker.enabled = false;
        }
    }


   

}
