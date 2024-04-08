using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotorAnimationController : MonoBehaviour
{



    [SerializeField]
    private Animator motorAnimator = null;


    private void Start()
    {
        if (motorAnimator == null)
        {
            motorAnimator = GetComponent<Animator>();
            motorAnimator.speed = 0;

        }
    }



    // Update is called once per frame
    void Update()
    {
        float animationTime = motorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //Debug.Log("animationTime (normalized) is " + animationTime);
    }

    public void OnValueChanged(SliderEventData eventData)
    {
        motorAnimator.SetFloat("OpenValue", eventData.NewValue);

       
    }






}
