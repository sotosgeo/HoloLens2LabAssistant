using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackingDistance : MonoBehaviour
{


    Plane cameraPlane = new();
    public TextMeshPro myText;

    [SerializeField] GameObject cableTracker;
    private float distanceFromCamera;

    private ArucoTracker arucoTracker;
    private MediaCapturer mediaCapturer;
    private void Start()
    {
        arucoTracker = cableTracker.GetComponent<ArucoTracker>();
        mediaCapturer = cableTracker.GetComponent<MediaCapturer>();
    }


    void Update()
    {
        cameraPlane.SetNormalAndPosition(Camera.main.transform.forward, Camera.main.transform.position);
        distanceFromCamera = Mathf.Abs(cameraPlane.GetDistanceToPoint(this.transform.position));
        myText.text = distanceFromCamera.ToString();
        if(distanceFromCamera <= 1.1)
        {
            arucoTracker.enabled = true;
        }
        else
        {
            arucoTracker.enabled = false;
        }
    }


   

}
