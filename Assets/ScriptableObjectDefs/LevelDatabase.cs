using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDatabase : ScriptableObject
{
    public List<LevelDefinition> levels;
}

[Serializable]
public struct LevelDefinition
{
    public int levelTimeSeconds;
    public string levelName;
    public int satisfactionValueRequirement;
    public int[] scoreStars;
    public int satisfactionValueIncreaseAmountOnGivingSuccess;
    public int satisfactionValueDecreaseAmountOnGivingFail;
    public int satisfactionValueDecreaseAmountOnInterval;
    public int satisfactionValueDecreaseAmountInterval;
}