using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizeQuestions()
    {
        List<ObjectProperty> propertyQuestions = new List<ObjectProperty>();
        do
        {
            ObjectProperty property = (ObjectProperty)Random.Range(0, 8);
            if (!propertyQuestions.Contains(property))
            {
                propertyQuestions.Add(property);
            }
        } while (propertyQuestions.Count == 6);
        
    }
}
