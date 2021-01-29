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
    public int satisfactionLevelRequirement;
    public int[] scoreStars;
    public int satisfactionLevelIncreaseAmountOnGivingSuccess;
    public int satisfactionLevelDecreaseAmountOnGivingFail;
    public int satisfactionLevelDecreaseAmountOnInterval;
    public int satisfactionLevelDecreaseAmountInterval;
}