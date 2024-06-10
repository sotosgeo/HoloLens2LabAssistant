using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarkerFollower : MonoBehaviour
{
    private Vector3 pos;
    private Quaternion rot;

    [SerializeField] ArucoTracker tracker;

    public int markerId;

    private void OnEnable()
    {
        tracker.onDetectionFinished += OnMarkersDetected;
    }

    private void OnDisable()
    {
        tracker.onDetectionFinished -= OnMarkersDetected;

    }

    private void OnMarkersDetected(IReadOnlyDictionary<int, Marker> detectedMarkers)
    {
        if (detectedMarkers.Count == 0)
            return;

        if (!detectedMarkers.TryGetValue(markerId, out Marker marker))
            return;

        transform.SetPositionAndRotation(marker.position, marker.rotation);
    }
}
