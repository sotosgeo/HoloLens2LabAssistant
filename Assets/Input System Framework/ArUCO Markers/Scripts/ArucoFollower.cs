using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArucoFollower : MonoBehaviour
{
    private Vector3 pos;
    private Quaternion rot;
    [SerializeField] ArucoTracker tracker;
    [SerializeField] TMP_Text txt;

    private void OnEnable()
    {
        tracker.onDetectionFinished += OnBoard;
    }

    private void OnDisable()
    {
        tracker.onDetectionFinished -= OnBoard;
    }

    private void OnBoard(IReadOnlyDictionary<int, Marker> detectedMarkers)
    {
        if (detectedMarkers.Count == 0)
            return;
        var firstMarker = detectedMarkers[-1];
        pos = firstMarker.position;
        rot = firstMarker.rotation;
        transform.SetPositionAndRotation(pos, rot);

        if (txt != null)
            txt.text = $"ID: {firstMarker.id}";
    }
}
