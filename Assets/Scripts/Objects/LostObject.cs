using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LostObject 
{
    public string name;
    public LostObjectPropertiesDict properties;
}

[Serializable]
public class LostObjectPropertiesDict : SerializableDictionary<ObjectProperty, string> { }