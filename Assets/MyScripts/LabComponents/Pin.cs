using Mirror.Examples.AdditiveLevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material connectionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;

   
    public string pinTag;
    public LabComponent parent;
    private string fullTag;

    public string FullTag
    {
        get
        {
            if (parent == null)
                return $"none.{pinTag}";
            return fullTag;
        }
    }

    private void Awake()
    {
        fullTag = $"{parent.componentTag}.{pinTag}";
    }


    public override string ToString()
    {
        return FullTag;
    }
}
