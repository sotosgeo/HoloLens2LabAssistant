using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlaceManager : MonoBehaviour
{
    [SerializeField] GameObject[] placedComponentObjects;
    //[SerializeField] GameObject placementSurface;

    private bool localToggle = true;


    //Call when bench is placed in space
    public void OnBenchPlacementFinished(bool forceToggle = true)
    {
        if (forceToggle == false) { localToggle = false; }
        else localToggle = true;

        foreach (GameObject obj in placedComponentObjects)
        {
            obj.GetComponent<TapToPlace>().enabled = localToggle;
            obj.GetComponent<SolverHandler>().enabled = localToggle;

        }
    }

    //Call when placement of objects is finished
    public void OnComponentPlacementFinished()
    {

      


    }
}
