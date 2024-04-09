using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlaceManager : MonoBehaviour
{
    [SerializeField] GameObject[] placedObjects;
    [SerializeField] GameObject placementSurface;

    private bool _toggled = true;

    public void ToggleTapToPlace()
    {
        if(_toggled == true)
        {
            _toggled=false;
        }
        else
        {
            _toggled = true;
        }
        foreach (GameObject obj in placedObjects)
        {
            obj.GetComponent<TapToPlace>().enabled =_toggled;
            obj.GetComponent<SolverHandler>().enabled = _toggled;

        }
    }


    public void OnPlacementFinished()
    {
        _toggled = true;
        ToggleTapToPlace();
        foreach (GameObject obj in placedObjects)
        {
            obj.transform.parent = placementSurface.transform;

        }

    }
}
