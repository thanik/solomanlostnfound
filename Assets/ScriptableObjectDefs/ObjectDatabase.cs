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

    public PropertyDetail(PropertyType type, List<string> values)
    {
        this.type = type;
        this.values = values;

    }
}

[Serializable]
public class ObjectDefinition
{
    public Sprite spriteImage;
    public Sprite leftSpriteImage;
    public Sprite rightSpriteImage;
    public string name;
    public PropertiesDict properties;
}

[CreateAssetMenu]
public class ObjectDatabase : ScriptableObject
{
    public List<ObjectDefinition> objects;
    public PropertiesValuesPoolDict propertiesValues;
    public ColorMappingDict colorMappings;
}

[Serializable]
public class PropertiesDict : SerializableDictionary<ObjectProperty, PropertyDetail> { }

[Serializable]
public class PropertiesValuesPoolDict : SerializableDictionary<ObjectProperty, PropertyDetail> { }

[Serializable]
public class ColorMappingDict : SerializableDictionary<string, Color> { }
