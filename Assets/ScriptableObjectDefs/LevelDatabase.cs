using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDatabase : ScriptableObject
{
    public List<LevelDefinition> levels;
    public int goodPersonScoreValue;
    public int badPersonScoreValue;
    public int destroyedScoreValue;
}

[Serializable]
public struct LevelDefinition
{
    public int levelTimeSeconds;
    public string levelName;
    public int satisfactionValueRequirement;
    public int[] scoreStars;
    public int numberOfPeople;
    public int timePerItem;
    public int satisfactionValueIncreaseAmountOnGivingSuccess;
    public int satisfactionValueDecreaseAmountOnGivingFailure;
    public int satisfactionValueDecreaseAmountOnItemDestroyed;
    public int satisfactionValueDecreaseAmountOnInterval;
    public int satisfactionValueDecreaseInterval;
}