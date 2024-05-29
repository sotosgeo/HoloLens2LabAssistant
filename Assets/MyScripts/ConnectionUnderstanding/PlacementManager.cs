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
    public List<GameObject> placedComponentObjects;

    private bool _placementToggle = true;
    private bool _tooltipToggle = true;
    private bool _visualizationToggle = false;



    private void OnEnable()
    {
        ChangeManipulationAndVisualization(true);
    }

    private void OnDisable()
    {
        ChangeManipulationAndVisualization(false);
    }

    private void ChangeManipulationAndVisualization(bool change)
    {
        foreach (var component in placedComponentObjects)
        {
            Transform manipulator = component.transform.GetChild(0);
            Transform visualization = component.transform.GetChild(1);
            manipulator.gameObject.SetActive(change);
            visualization.gameObject.SetActive(change);

            //Turn off Pins Visual
            MeshRenderer[] pinRenderers = component.transform.GetChild(3).GetComponentsInChildren<MeshRenderer>();
            foreach (var pinRenderer in pinRenderers)
            {
                pinRenderer.enabled = change;
            }
        }
    }


    public void TogglePlacement()
    {
        ChangeManipulationAndVisualization(_placementToggle);
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

    public void ToggleVisualization()
    {
        _visualizationToggle = !_visualizationToggle;
        ChangeVisualization(_visualizationToggle);
        
    }

    public void ChangeVisualization(bool mode)
    {
        foreach (var component in placedComponentObjects)
        {
            //Turn Off Component Visualization
            Transform visualization = component.transform.GetChild(1);
            visualization.gameObject.SetActive(mode);

            //Turn off Pins Visual
            MeshRenderer[] pinRenderers = component.transform.GetChild(3).GetComponentsInChildren<MeshRenderer>();
            foreach (var pinRenderer in pinRenderers)
            {
                pinRenderer.enabled = mode;
            }
        }
    }

}
