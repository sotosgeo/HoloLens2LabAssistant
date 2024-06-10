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
    public LabComponent parentComponent;
    private string fullTag;

    public string FullTag
    {
        get
        {
            if (parentComponent == null)
                return $"none.{pinTag}";
            return fullTag;
        }
    }

    private void Awake()
    {
        fullTag = $"{parentComponent.componentTag}.{pinTag}";
    }


    public override string ToString()
    {
        return FullTag;
    }
}
