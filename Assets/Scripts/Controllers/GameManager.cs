using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GameState { Paused, Playing }

public class GameManager : Singleton<GameManager>
{
    public LevelDatabase levelsDB;
    public ObjectDatabase objDB;
    public SpriteColorDatabase sprColorDB;
    public Generator generator;

    public List<PersonController> PersonControllers = new List<PersonController>();

    private int levelIndex = 0;
    private float levelTime = 0;
    GameState gState;

    public GameObject itemPrefab;
    public Transform itemSpawnPoint;
    public GameObject personPrefab;
    public Transform personSpawnPoint;

    // defines function and parameters if required
    public delegate void OnSatisfactionUpdateHandler(int point);
    public delegate void OnDateUpdateHandler(string date);
    // event to subsbribe to
    public event OnSatisfactionUpdateHandler OnSatisfactionUpdated;
    public event OnDateUpdateHandler OnDateUpdated;
    
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnDateUpdated?.Invoke(levelsDB.levels[levelIndex].levelName); 
            if(levelIndex < levelsDB.levels.Count) // there is a BUG here
            levelIndex++;
        }

        // if esc, call show pause menu in ui
    }

    public void QuestionClicked(int qIndex)
    {
        Debug.Log("Question " + ((ObjectProperty)qIndex).ToString() + " clicked!");
        foreach (var personController in PersonControllers)
        {
            personController.ShowAnswer((ObjectProperty) qIndex);
        }
    }

    void StartLevel()
    {
        gState = GameState.Playing;
        generator.InitializeObject(levelsDB.levels[levelIndex].numberOfPeople);
        ItemController item = Instantiate(itemPrefab, itemSpawnPoint).GetComponent<ItemController>();
        foreach (Person person in generator.people)
        {
            GameObject p = Instantiate(personPrefab, personSpawnPoint);
            PersonController pCtrl = p.GetComponent<PersonController>();
            pCtrl.personData = person;
            pCtrl.headBaseRenderer.sprite = sprColorDB.headBaseSprites[Random.Range(0, sprColorDB.headBaseSprites.Count)];
            pCtrl.eyesRenderer.sprite = sprColorDB.eyesSprites[Random.Range(0, sprColorDB.eyesSprites.Count)];
            pCtrl.noseRenderer.sprite = sprColorDB.noseSprites[Random.Range(0, sprColorDB.noseSprites.Count)];
            pCtrl.mouthRenderer.sprite = sprColorDB.mouthSprites[Random.Range(0, sprColorDB.mouthSprites.Count)];
            pCtrl.outfitRenderer.sprite = sprColorDB.shirtSprites[Random.Range(0, sprColorDB.shirtSprites.Count)];
            PersonControllers.Add(pCtrl);
        }
    }

    //TO DO
    void CleanLevel()
    {
        // play state is false paused
        // call when time left is less than 0
        // level failed \ player does not meet threshold
        // next level
    }

    public GameState GetGameState()
    {
        return gState;
    }
}
