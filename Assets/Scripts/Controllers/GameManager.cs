using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GameState { Paused, Playing }

public class GameManager : Singleton<GameManager>
{
    public LevelDatabase levels;
    private int levelIndex;
    private float levelTime = 0;
    GameState gState;

    // defines function and parameters if required
    public delegate void OnSatisfactionUpdateHandler(int point);

    // event to subsbribe to
    public event OnSatisfactionUpdateHandler OnSatisfactionUpdated;

    // Start is called before the first frame update
    void Start()
    {
        gState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        // do timer stuff.. decrease satisfaction every x seconds trickle effect kind of
        // update satisfaction by a point if item is returned +/-
        // point based on action so can invoke whenever needed  -- can also be called when collider goes over wrong or right person
        if (Input.GetKeyDown(KeyCode.W))
            OnSatisfactionUpdated?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.S))
            OnSatisfactionUpdated?.Invoke(-1);
    }

    /*
    public Days GetDay()
    {
        return dayLevel;
    }
    */

    public void QuestionClicked(int qIndex)
    {
        Debug.Log("Question " + qIndex + " clicked!");
    }

    public GameState GetGameState()
    {
        return gState;
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
