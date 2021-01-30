using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpriteColorDatabase : ScriptableObject
{
    public ColorMappingDict colorMappings;
    public List<Sprite> headBaseSprites;
    public List<Sprite> eyesSprites;
    public List<Sprite> noseSprites;
    public List<Sprite> mouthSprites;
    public List<Sprite> shirtSprites;
}

[Serializable]
public class ColorMappingDict : SerializableDictionary<string, Color>
{
    ColorMappingDict()
    {
        this.Add("pink", new Color());
        this.Add("brown", new Color());
        this.Add("silver", new Color());
        this.Add("bronze", new Color());
        this.Add("black", new Color());
        this.Add("gold", new Color());
        this.Add("yellow", new Color());
        this.Add("white", new Color());
        this.Add("red", new Color());
        this.Add("blue", new Color());
        this.Add("green", new Color());
        this.Add("grey", new Color());
        this.Add("beige", new Color());
    }
}