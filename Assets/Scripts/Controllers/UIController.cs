using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Satisfaction
{
    EXTREMELYDISAPPOINTED = -2,
    VERYDISAPPOINTED,
    NEUTRAL,
    VERYSATISFIED,
    EXTREMELYSATISFIED
}

public class UIController : MonoBehaviour
{
    const int spriteArraySize = 7;
    public Image[] satisfactionSprites = new Image[spriteArraySize];
    [SerializeField]
    private Image satisfactionSprite;
    [SerializeField]
    private TextMeshProUGUI levelDateText;
    [SerializeField]
    private TextMeshProUGUI levelTimeText;
    [SerializeField]
    private TextMeshProUGUI levelScore;
    private Satisfaction satisfactionState;
    public ComputerScreenController computerScreenController;

    private void OnValidate()
    {
        if (satisfactionSprites.Length != spriteArraySize)
        {
            Debug.LogWarning("Don't change the 'satisfactionSprites' field's array size!");
            Array.Resize(ref satisfactionSprites, spriteArraySize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        satisfactionState = Satisfaction.NEUTRAL;
    }

    // Event to Update Satisfaction
    public void UpdateSatisfaction(Satisfaction sState)
    {
        satisfactionState = sState;

        // Update UI
        UpdateSatisfactionSprite();
    }

    // Function to update sprite
    public void UpdateSatisfactionSprite()
    {
        switch (satisfactionState)
        {
            case Satisfaction.EXTREMELYDISAPPOINTED:
                Debug.Log("Extremely Disappointed");
                break;
            case Satisfaction.VERYDISAPPOINTED:
                Debug.Log("Very Disappointed");
                break;
            case Satisfaction.NEUTRAL:
                Debug.Log("Neutral");
                break;
            case Satisfaction.VERYSATISFIED:
                Debug.Log("Very Satisfied");
                break;
            case Satisfaction.EXTREMELYSATISFIED:
                Debug.Log("Extremely Satisfied");
                break;
            default:
                break;
        }
    }

    public void UpdateDate(string date)
    {
        levelDateText.text = date;
    }

    public void UpdateScore(int score)
    {
        levelScore.text = "Score: " + score;
    }

    public void UpdateLevelTime(float levelTime)
    {
        float minutes = Mathf.FloorToInt(levelTime / 60);
        float seconds = Mathf.FloorToInt(levelTime % 60);
        
        levelTimeText.text = "TIME:  " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowItemData(LostObject lostObject)
    {
        computerScreenController.UpdateText(lostObject);
    }

    //TODO
    public void ShowSummary()
    {
        Debug.Log("Show Summary!");
        // event if level lost, change content of summary
    }

    //TODO
    public void ShowPauseMenu(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Debug.Log("Game Paused");
                break;
            case GameState.Paused:
                Debug.Log("Playing!");
                break;
            default:
                break;
        }
    }

}
