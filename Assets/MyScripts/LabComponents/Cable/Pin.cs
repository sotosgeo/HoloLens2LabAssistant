using TMPro;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject pinVisual;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material connectionDetectedMaterial;
    [SerializeField] Material connectionFinalizedMaterial;
    public TextMeshPro pinText;
   
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
