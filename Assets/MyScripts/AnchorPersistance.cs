using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Microsoft.MixedReality.OpenXR;
using UnityEngine.XR.ARSubsystems;
using ARSessionOrigin = Unity.XR.CoreUtils.XROrigin;
using Microsoft.MixedReality.OpenXR.ARFoundation;


/// <summary> 
/// Code From Microsoft.MixedReality.OpenXR.Sample
/// This sample detects air taps, creating new unpersisted anchors at the locations. Air tapping 
/// again near these anchors toggles their persistence, backed by the <c>XRAnchorStore</c>.
/// </summary>

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARSessionOrigin))]

public class AnchorPersistance : MonoBehaviour
{

    private ARSessionOrigin m_arSessionOrigin; // Used for ARSessionOrigin.trackablesParent
    private ARAnchorManager m_arAnchorManager;
    private List<ARAnchor> m_anchors = new List<ARAnchor>();
    private XRAnchorStore m_anchorStore = null;
    private Dictionary<TrackableId, string> m_incomingPersistedAnchors = new Dictionary<TrackableId, string>();

    private ARAnchor labSurfaceAnchor;


    private int _anchorCount = 0;
    [SerializeField] GameObject labSurface;
    protected async void OnEnable()
    {
        // Set up references in this script to ARFoundation components on this GameObject.
        m_arSessionOrigin = GetComponent<ARSessionOrigin>();
        if (!TryGetComponent(out m_arAnchorManager) || !m_arAnchorManager.enabled || m_arAnchorManager.subsystem == null)
        {
            Debug.Log($"ARAnchorManager not enabled or available; Anchor functionality will not be enabled.");
            return;
        }

       

   

        m_anchorStore = await XRAnchorStore.LoadAnchorStoreAsync(m_arAnchorManager.subsystem);

        if (m_anchorStore == null)
        {
            Debug.Log("XRAnchorStore not available, Anchor persistence functionality will not be enabled.");
            return;
        }

        // Request all persisted anchors be loaded once the anchor store is loaded.
        foreach (string name in m_anchorStore.PersistedAnchorNames)
        {
            // When a persisted anchor is requested from the anchor store, LoadAnchor returns the TrackableId which
            // the anchor will use once it is loaded. To later recognize and recall the names of these anchors after
            // they have loaded, this dictionary stores the TrackableIds.
            TrackableId trackableId = m_anchorStore.LoadAnchor(name);
            m_incomingPersistedAnchors.Add(trackableId, name);
        }
    }

    public void AddAnchor()
    {
       
    }

    public void PersistAnchors()
    {
      
    }

    private void AnchorsChanged(ARAnchorsChangedEventArgs eventArgs)
    {
        foreach (var added in eventArgs.added)
        {
            Debug.Log($"Anchor added from ARAnchorsChangedEvent: {added.trackableId}, OpenXR Handle: {added.GetOpenXRHandle()}");
           
        }

        foreach (var removed in eventArgs.removed)
        {
            Debug.Log($"Anchor removed: {removed.trackableId}");
            m_anchors.Remove(removed);
        }
    }

    public void AnchorStoreClear()
    {
        m_anchorStore.Clear();
        Debug.Log("Anchor Store Cleared");
       
    }
}

