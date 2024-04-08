using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartSelection", menuName = "ScriptableObjects/Question/PartSelection", order = 2)]

public class PartSelectionData : ScriptableObject
{
    public string question;
    [Tooltip("Choose Part Index based on the order you have them in the InteractableToogleList")]
    public int correctAnswerIndex;
}

