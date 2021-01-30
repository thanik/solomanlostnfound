using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComputerScreenController : MonoBehaviour
{
    public TMP_Text[] propertyTexts;

    public void UpdateText(LostObject lostObjectData)
    {
        foreach (ObjectProperty key in lostObjectData.properties.Keys)
        {
            switch (key)
            {
                case ObjectProperty.COLOR:
                    propertyTexts[0].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.WEIGHT:
                    propertyTexts[1].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.HEIGHT:
                    propertyTexts[2].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.SEX:
                    propertyTexts[3].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.SPECIES:
                    propertyTexts[4].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.EDIBLE:
                    propertyTexts[5].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.AGE:
                    propertyTexts[6].text = lostObjectData.properties[key];
                    break;
                case ObjectProperty.ORIGIN:
                    propertyTexts[7].text = lostObjectData.properties[key];
                    break;
            }
        }
    }
}
