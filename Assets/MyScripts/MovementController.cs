using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _movementValue = 0.1f;
    [SerializeField] private float _rotationValue = 1f;
    [SerializeField] Transform objectToMoveTransform;
    [SerializeField] private float _incrementValue = 0.1f;

    

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

    public void RotateLeft()
    {
        objectToMoveTransform.Rotate(Vector3.up * _rotationValue, Space.Self);
    }

    public void RotateRight()
    {
        objectToMoveTransform.Rotate(Vector3.up * (-_rotationValue), Space.Self);
    }


    public void IncreaseMovementValue()
    {

    }

    public void DecreaseMovementValue()
    {

    }

}
