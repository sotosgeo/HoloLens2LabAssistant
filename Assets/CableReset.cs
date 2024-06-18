using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableReset : MonoBehaviour
{

    [SerializeField] GameObject[] cables;


    void Start()
    {
        cables = GameObject.FindGameObjectsWithTag("Cable");
    }

    public void ResetCables()
    {
        //Move the cables far away
        foreach (var cable in cables)
        {
            cable.transform.position = new Vector3(20,20,20);
            //Need to also activate the ArucoFollower script on each cable start and end
            cable.transform.GetChild(0).GetComponent<MarkerFollower>().enabled = true;
            cable.transform.GetChild(1).GetComponent<MarkerFollower>().enabled = true;
        }

        

    }


}
