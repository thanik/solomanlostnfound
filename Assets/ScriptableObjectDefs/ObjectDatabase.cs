using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    RANGE,
    RANDOM_STRING
}

[Serializable]
public struct PropertyDetail
{
    public PropertyType type;
    public List<string> values;
}

[Serializable]
public class ObjectDefinition
{
    public Sprite spriteImage;
    public Sprite leftSpriteImage;
    public Sprite rightSpriteImage;
    public string name;
    public Dictionary<string, PropertyDetail> properties;
}

[CreateAssetMenu]
public class ObjectDatabase : ScriptableObject
{
    public List<ObjectDefinition> objects;
}
