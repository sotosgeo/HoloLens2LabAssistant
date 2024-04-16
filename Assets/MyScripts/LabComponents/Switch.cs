using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour 
{
    [SerializeField] GameObject[] switchPins;
    [SerializeField] GameObject[] pinVisualization;
    
    public int SwitchId {  get;  private set; }

    private void Start()
    {
        
    }


}
