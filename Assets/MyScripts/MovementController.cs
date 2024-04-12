using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _movementValue;
    [SerializeField] Transform objectToMoveTransform;


    private void Start()
    {

    }
    private void Update()
    {

    }



    public void MoveUp()
    {
        objectToMoveTransform.localPosition += Vector3.up * _movementValue;
    }

    public void MoveDown()
    {
        objectToMoveTransform.localPosition += Vector3.down * _movementValue;
    }

    public void MoveLeft()
    {
        objectToMoveTransform.localPosition += Vector3.left * _movementValue;
    }

    public void MoveRight()
    {
        objectToMoveTransform.localPosition += Vector3.right * _movementValue;
    }

    public void MoveForward()
    {
        objectToMoveTransform.localPosition += Vector3.forward * _movementValue;
    }

    public void MoveBack()
    {
        objectToMoveTransform.localPosition += Vector3.back * _movementValue;
    }

}
