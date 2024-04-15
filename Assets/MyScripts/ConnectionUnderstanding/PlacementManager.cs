using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    /// <summary>
    /// - Turns off BoundsControl and ObjectManipulator when component placement is finalized
    /// - Maybe event that listens to InstantiatePrefab(Component) and adds the placed component in the
    /// placedComponentObjects
    /// </summary>
    [SerializeField] GameObject[] placedComponentObjects;
    

    public void OnLabComponentInstantiated()
    {
        if (placedComponentObjects != null)
        {

        }
    }
    
   



    
}
