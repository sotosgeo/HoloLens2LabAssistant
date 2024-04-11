using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] GameObject[] placedComponentObjects;
    [SerializeField] GameObject labBench;
    private Collider _benchCollider;
    private bool localToggle = true;


    private void Start()
    {
        _benchCollider = labBench.GetComponent<Collider>();
    }

    //Call when bench is placed in space
    //
    public void OnBenchPlacementFinished(bool forceToggle = true)
    {
        _benchCollider.enabled = forceToggle;

    }
}
