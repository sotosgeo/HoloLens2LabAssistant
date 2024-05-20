using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    /// <summary>
    /// - Turns off BoundsControl and ObjectManipulator, and Visualisation when component placement is finalized
    /// - Maybe event that listens to InstantiatePrefab(Component) and adds the placed component in the
    /// placedComponentObjects
    /// </summary>
    public GameObject[] placedComponentObjects;

    private bool _placementToggle = false;
    private bool _tooltipToggle = true;
    public void OnLabComponentInstantiated()
    {
        if (placedComponentObjects != null)
        {

        }
    }

    public void OnPlacementFinished()
    {
        foreach (var component in placedComponentObjects)
        {
            Transform manipulator = component.transform.GetChild(0);
            Transform visualization = component.transform.GetChild(1);
            manipulator.gameObject.SetActive(_placementToggle);
            visualization.gameObject.SetActive(_placementToggle);
        }
        _placementToggle = !_placementToggle;
    }

    public void ToolTipToggle()
    {
        foreach (var component in placedComponentObjects)
        {
            Transform tooltip = component.transform.GetChild(2);

            tooltip.gameObject.SetActive(_tooltipToggle);
        }
        _tooltipToggle = !_tooltipToggle;
    }


}
