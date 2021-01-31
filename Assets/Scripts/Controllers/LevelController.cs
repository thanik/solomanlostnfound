using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public enum GameState { Paused, Playing, End }
public enum ClearEvent { Giving, TimeExpire }

public class LevelController : MonoBehaviour
{
    public ObjectDatabase objDB;
    public SpriteColorDatabase sprColorDB;
    public Generator generator;
    public UIController uiController;
    public ItemController item;
    public Animator solomonAnimator;

    public List<PersonController> PersonControllers = new List<PersonController>();

    private int levelIndex = 0;
    private float levelTime = 0;
    private bool isDestroyingItem = false;
    private bool isGivingItem = false;
    private int levelScore = 0;
    private int satisfactionValue = 50;
    private int returnedItems = 0;
    private int lostItems = 0;

    GameState gState;

    public GameObject itemPrefab;
    public float itemTime = 30.0f;
    public Transform itemSpawnPoint;
    public GameObject personPrefab;
    public Transform personSpawnPoint;

    // defines function and parameters if required
    public delegate void OnSatisfactionUpdateHandler(Satisfaction sState);
    public delegate void OnDateUpdateHandler(string date);
    public delegate void OnPauseCallHandler(GameState state);
    public delegate void OnScoreUpdatedHandler(int score);
    public delegate void OnShowItemCreateHandler(LostObject lostObject);
    public delegate void OnQuestionStateChangeHandler();
    // event to subsbribe to
    public event OnSatisfactionUpdateHandler OnSatisfactionUpdated;
    public event OnDateUpdateHandler OnDateUpdated;
    public event OnPauseCallHandler OnPauseCalled;
    public event OnScoreUpdatedHandler OnScoreUpdated;
    public event OnShowItemCreateHandler OnShowItemCreated;
    public event OnQuestionStateChangeHandler OnQuestionStateChanged;


    private void OnEnable()
    {
        OnSatisfactionUpdated += uiController.UpdateSatisfaction;
        OnDateUpdated += uiController.UpdateDate;
        OnScoreUpdated += uiController.UpdateScore;
        OnShowItemCreated += uiController.ShowItemData;
        OnPauseCalled += uiController.ShowPauseMenu;
        OnQuestionStateChanged += uiController.EnableQuestions;
    }

    private void OnDisable()
    {
        OnSatisfactionUpdated -= uiController.UpdateSatisfaction;
        OnDateUpdated -= uiController.UpdateDate;
        OnScoreUpdated -= uiController.UpdateScore;
        OnShowItemCreated -= uiController.ShowItemData;
        OnPauseCalled -= uiController.ShowPauseMenu;
        OnQuestionStateChanged -= uiController.EnableQuestions;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ShowText(GameManager.Instance.levelsDB.levels[GameManager.Instance.GetLevelIndex()].levelName);
        PrepareLevel();
        StartCoroutine(TransitionToLevel());
    }

    IEnumerator TransitionToLevel()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (gState == GameState.Playing)
        {
            UpdateLevelTime();
            UpdateItemTimeAndPosition();
        }
        CheckInput();
        // do timer stuff.. decrease satisfaction every x seconds trickle effect kind of
        // update satisfaction by a point if item is returned +/-
        // point based on action so can invoke whenever needed  -- can also be called when collider goes over wrong or right person
        if (Input.GetKeyDown(KeyCode.W))
            OnSatisfactionUpdated?.Invoke((Satisfaction)1);
        if (Input.GetKeyDown(KeyCode.S))
            OnSatisfactionUpdated?.Invoke((Satisfaction)(-1));
    }

    public void QuestionClicked(int qIndex)
    {
        if (!isGivingItem || !isDestroyingItem)
        {
            Debug.Log("Question " + ((ObjectProperty) qIndex).ToString() + " clicked!");
            foreach (var personController in PersonControllers)
            {
                personController.ShowAnswer((ObjectProperty) qIndex);
            }
        }
    }

    void PrepareLevel()
    {
        // Set level index
        levelIndex = GameManager.Instance.GetLevelIndex();

        // Level Name
        OnDateUpdated?.Invoke(GameManager.Instance.levelsDB.levels[levelIndex].levelName);

        // Level Time
        levelTime = GameManager.Instance.levelsDB.levels[levelIndex].levelTimeSeconds;


        // Level Score
        OnScoreUpdated?.Invoke(levelScore);

        // Set Satisfaction
        OnSatisfactionUpdated?.Invoke(CalculateSatisfaction());
    }
    
    void StartLevel()
    {
        // Set game state
        gState = GameState.Playing;

        GenerateItem();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gState == GameState.Playing)
        {
            OnPauseCalled?.Invoke(gState);
            gState = GameState.Paused;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gState == GameState.Paused)
        {
            OnPauseCalled?.Invoke(gState);
            gState = GameState.Playing;
        }
    }

    void GenerateItem()
    {
        // Initialise Lost Object
        generator.InitializeObject(GameManager.Instance.levelsDB.levels[levelIndex].numberOfPeople);
        item = Instantiate(itemPrefab, itemSpawnPoint).GetComponent<ItemController>();
        SpriteRenderer sprRenderer = item.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = generator.lostObjects[0].objectDefinition.spriteImage;

        if (generator.lostObjects[0].objectDefinition.redColorReplace.StartsWith("#"))
        {
            Color color = Color.red;
            ColorUtility.TryParseHtmlString(generator.lostObjects[0].objectDefinition.redColorReplace, out color);
            sprRenderer.material.SetColor("_RedColorReplace", color);
        }
        else if (generator.lostObjects[0].objectDefinition.redColorReplace == "MAPPING")
        {
            sprRenderer.material.SetColor("_RedColorReplace", sprColorDB.colorMappings[generator.lostObjects[0].properties[ObjectProperty.COLOR]]);
        }

        if (generator.lostObjects[0].objectDefinition.greenColorReplace.StartsWith("#"))
        {
            Color color = Color.green;
            ColorUtility.TryParseHtmlString(generator.lostObjects[0].objectDefinition.greenColorReplace, out color);
            sprRenderer.material.SetColor("_GreenColorReplace", color);
        }
        else if (generator.lostObjects[0].objectDefinition.greenColorReplace == "MAPPING")
        {
            sprRenderer.material.SetColor("_GreenColorReplace", sprColorDB.colorMappings[generator.lostObjects[0].properties[ObjectProperty.COLOR]]);
        }

        if (generator.lostObjects[0].objectDefinition.blueColorReplace.StartsWith("#"))
        {
            Color color = Color.blue;
            ColorUtility.TryParseHtmlString(generator.lostObjects[0].objectDefinition.blueColorReplace, out color);
            sprRenderer.material.SetColor("_BlueColorReplace", color);
        }
        else if (generator.lostObjects[0].objectDefinition.blueColorReplace == "MAPPING")
        {
            sprRenderer.material.SetColor("_BlueColorReplace", sprColorDB.colorMappings[generator.lostObjects[0].properties[ObjectProperty.COLOR]]);
        }

        itemTime = GameManager.Instance.levelsDB.levels[levelIndex].timePerItem;
        // Generate Persons
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
            pCtrl.levelController = this;
            PersonControllers.Add(pCtrl);
        }
        OnShowItemCreated?.Invoke(generator.lostObjects[0]);
    }

    //TO DO
    void ClearLevel()
    {
        gState = GameState.Paused;


        // play state is false paused
        // call when time left is less than 0
        // level failed \ player does not meet threshold
        // next level
    }

    void UpdateLevelTime()
    {
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time has run out!");
            levelTime = 0;
            gState = GameState.End;
            uiController.ShowSummary(GameManager.Instance.levelsDB.levels[levelIndex].scoreStars, levelScore, returnedItems, lostItems, CheckWinCondition());
        }
        uiController.UpdateLevelTime(levelTime);
    }

    bool CheckWinCondition()
    {
        return satisfactionValue > GameManager.Instance.levelsDB.levels[levelIndex].satisfactionValueRequirement;
    }

    Satisfaction CalculateSatisfaction()
    {
        int satisfaction = GetSatisfactionValue();
        if (satisfaction >= 0 && satisfaction < 20)
        {
            return Satisfaction.EXTREMELYDISAPPOINTED;
        }
        else if (satisfaction >= 20 && satisfaction < 40)
        {
            return Satisfaction.VERYDISAPPOINTED;
        }
        else if (satisfaction >= 40 && satisfaction < 60)
        {
            return Satisfaction.NEUTRAL;
        }
        else if (satisfaction >= 60 && satisfaction < 80)
        {
            return Satisfaction.VERYSATISFIED;
        }
        else
        {
            return Satisfaction.EXTREMELYSATISFIED;
        }
    }

    void UpdateItemTimeAndPosition()
    {
        if (!isGivingItem)
        {
            if (itemTime > 0)
            {
                itemTime -= Time.deltaTime;
                item.normalizedTime = itemTime / GameManager.Instance.levelsDB.levels[levelIndex].timePerItem;
            }
            else if (!isDestroyingItem)
            {
                StartCoroutine(ClearItem(ClearEvent.TimeExpire));
                isDestroyingItem = true;
                SetSatisfactionValue(GetSatisfactionValue() - GameManager.Instance.levelsDB.levels[levelIndex].satisfactionValueDecreaseAmountOnItemDestroyed);
                lostItems++;
            }
        }
    }

    public void SelectPerson(bool isLegitOwner)
    {
        int satisfaction = GetSatisfactionValue();
        int score = GetLevelScoreValue();
        if (isLegitOwner)
        {
            SetLevelScoreValue(score + GameManager.Instance.levelsDB.goodPersonScoreValue);
            SetSatisfactionValue(satisfaction + GameManager.Instance.levelsDB.levels[levelIndex].satisfactionValueIncreaseAmountOnGivingSuccess);
            returnedItems++;
        }
        else
        {
            SetLevelScoreValue(score - GameManager.Instance.levelsDB.badPersonScoreValue);
            SetSatisfactionValue(satisfaction - GameManager.Instance.levelsDB.levels[levelIndex].satisfactionValueDecreaseAmountOnGivingFailure);
            lostItems++;
        }
        OnScoreUpdated?.Invoke(levelScore);
        isGivingItem = true;
        StartCoroutine(ClearItem(ClearEvent.Giving));
    }

    public void SetLevelScoreValue(int value)
    {
        levelScore = value;
        if (levelScore < 0)
        {
            levelScore = 0;
        }
    }

    public int GetLevelScoreValue()
    {
        return levelScore;
    }

    public void SetSatisfactionValue(int value)
    {
        satisfactionValue = value;
        if (satisfactionValue > 100)
        {
            satisfactionValue = 100;
        }
        else if (satisfactionValue < 0)
        {
            satisfactionValue = 0;
        }
        OnSatisfactionUpdated?.Invoke(CalculateSatisfaction());
    }

    public int GetSatisfactionValue()
    {
        return satisfactionValue;
    }

    private IEnumerator ClearItem(ClearEvent ce)
    {
        // TO DO Disable Question Buttons
        OnQuestionStateChanged?.Invoke();
        if (ce == ClearEvent.Giving)
        {
            //prevent clicking on the person again when it is clicked
            foreach (PersonController p in PersonControllers)
            {
                p.selectButton.interactable = false;
            }
            yield return new WaitForSeconds(1f);
            item.DestroyFromGame();
            // wait for animation then fade out all person
        }
        else
        {
            // play solomon animation
            solomonAnimator.SetTrigger("Chop");
            yield return new WaitForSeconds(0.83f);
            item.Chop();
            yield return new WaitForSeconds(1f);
            itemTime = GameManager.Instance.levelsDB.levels[levelIndex].timePerItem;
            SetLevelScoreValue(GetLevelScoreValue() - GameManager.Instance.levelsDB.destroyedScoreValue);
            isDestroyingItem = false;
            item.DestroyFromGame();
        }
        OnScoreUpdated?.Invoke(levelScore);
        foreach (PersonController p in PersonControllers)
        {
            p.DestroyFromGame();
        }

        yield return new WaitForSeconds(1f);
        
        PersonControllers.Clear();
        isGivingItem = false;
        GenerateItem();
        OnQuestionStateChanged?.Invoke();
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public GameState GetGameState()
    {
        return gState;
    }
}
