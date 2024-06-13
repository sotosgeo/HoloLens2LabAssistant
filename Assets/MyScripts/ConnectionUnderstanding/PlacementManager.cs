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
    private bool _tooltipToggle = false;
    private bool _visToggle = true;
    [SerializeField] Material DefaultPinMaterial;   
    

    private void OnEnable()
    {
        ChangeManipulationAndVisualization(true);
    }

    private void OnDisable()
    {
        ChangeManipulationAndVisualization(false);
    }

    public void ChangeManipulationAndVisualization(bool change)
    {
        
        foreach (var component in placedComponentObjects)
        {
            Transform manipulator = component.transform.Find("Manipulation");
            Transform visualization = component.transform.Find("Visualization");
            manipulator.gameObject.SetActive(change);
            visualization.gameObject.SetActive(change);

            //Turn off Pins Visual
            Pin[] pins = component.transform.GetChild(4).GetComponentsInChildren<Pin>();

            foreach (var pin in pins)
            {
                pin.pinVisual.SetActive(change);
               
            }
        }
        
    }


    public void TogglePlacement()
    {
        _placementToggle = !_placementToggle;
        ChangeManipulationAndVisualization(_placementToggle);
       
    }

    public void ToolTipToggle()
    {
        _tooltipToggle = !_tooltipToggle;

        foreach (var component in placedComponentObjects)
        {
            Transform tooltip = component.transform.Find("Tooltip");

            tooltip.gameObject.SetActive(_tooltipToggle);
        }
    }

   

    public void ToggleVisualization()
    {
        _visToggle = !_visToggle;
        foreach (var component in placedComponentObjects)
        {
            Transform visualization = component.transform.GetChild(0);
            visualization.gameObject.SetActive(_visToggle);

            //Turn off Pins Visual
            MeshRenderer[] pinRenderers = component.transform.GetChild(4).GetComponentsInChildren<MeshRenderer>();
            foreach (var pinRenderer in pinRenderers)
            {
                pinRenderer.enabled = _visToggle;
            }
        }       
    }


    public void ChangeComponentVisualization(bool mode)
    {
        foreach (var component in placedComponentObjects)
        {

            Transform visualization = component.transform.Find("Visualization");

            visualization.gameObject.SetActive(mode);
            //Turn off Pins Visual
            //Turn off Pins Visual
            Pin[] pins = component.transform.GetChild(4).GetComponentsInChildren<Pin>();

            foreach (var pin in pins)
            {
                pin.pinVisual.SetActive(mode);
                pin.pinVisual.GetComponent<MeshRenderer>().material = DefaultPinMaterial;

            }
        }


    }

    public void ChangeTooltip(bool mode)
    {
        foreach (var component in placedComponentObjects)
        {
            Transform tooltip = component.transform.Find("Tooltip");

            tooltip.gameObject.SetActive(mode);
        }

        _tooltipToggle = mode;
    }

    public void ChangeDirectionalIndicator(bool mode)
    {
        foreach (var component in placedComponentObjects)
        {
            Transform directionalIndicator = component.transform.Find("DirectionalChevron");
            directionalIndicator.gameObject.SetActive(mode);
        }
    }
}
