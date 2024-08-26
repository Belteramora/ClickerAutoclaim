using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutoClaimerData", menuName = "GameData/AutoClaimerData")]
public class AutoClaimerData : ScriptableObject
{
    public float baseValue;
    public float intervalInSeconds;
}
