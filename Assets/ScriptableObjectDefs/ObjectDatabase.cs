using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    RANGE,
    RANDOM_STRING
}

public enum ObjectProperty
{
    COLOR,
    WEIGHT,
    HEIGHT,
    SEX,
    SPECIES,
    EDIBLE,
    AGE,
    ORIGIN
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
    public Dictionary<ObjectProperty, PropertyDetail> properties;
}

[CreateAssetMenu]
public class ObjectDatabase : ScriptableObject
{
    public List<ObjectDefinition> objects;
    public Dictionary<ObjectProperty, List<string>> propertiesValues;
    public Dictionary<string, Color> colorMappings;
}
