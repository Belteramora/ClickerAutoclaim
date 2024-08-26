using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClickData", menuName = "GameData/ClickData")]
public class ClickData : ScriptableObject
{
    [Header("Modifiers")]
    public float baseValue;
    public float baseModifier;
    public float autoClaimerValueAddPercent;
    public float divider;

    [Header("Energy costs")]
    public int onClickEnergyCost;

    public float GetClickValue()
    {
        return baseValue * baseModifier;
    }
}
