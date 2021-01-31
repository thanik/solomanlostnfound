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
    const int spriteArraySize = 5;
    public Sprite[] satisfactionSprites = new Sprite[spriteArraySize];
    [SerializeField]
    private Sprite satisfactionSprite;
    [SerializeField]
    private TextMeshProUGUI levelDateText;
    [SerializeField]
    private TextMeshProUGUI levelTimeText;
    [SerializeField]
    private TextMeshProUGUI levelScore;
    private Satisfaction satisfactionState;
    public ComputerScreenController computerScreenController;
    public Canvas pauseCanvas;
    public Canvas summaryCanvas;
    public Image[] starSprites;
    public GameObject[] summaryTableValues;

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
        satisfactionSprite = satisfactionSprites[(satisfactionSprites.Length - 1) / 2];
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
    public void ShowSummary(int[] scoreStars, int levelScore, int returnedItems, int lostItems)
    {
        // Calculate Stars
        if (levelScore > 0)
        {
            starSprites[0].gameObject.SetActive(true);
        }
        if (levelScore >= scoreStars[1])
        {
            starSprites[1].gameObject.SetActive(true);
        }
        if (levelScore >= scoreStars[2])
        {
            starSprites[2].gameObject.SetActive(true);
        }

        summaryTableValues[0].GetComponent<TextMeshProUGUI>().text = levelScore.ToString();
        summaryTableValues[1].GetComponent<Image>().sprite = satisfactionSprite;
        summaryTableValues[2].GetComponent<TextMeshProUGUI>().text = returnedItems.ToString();
        summaryTableValues[3].GetComponent<TextMeshProUGUI>().text = lostItems.ToString();
        //Debug.Log("Show Summary!");
        //satisfactionsprite
        // event if level lost, change content of summary
        summaryCanvas.gameObject.SetActive(true);
    }

    //TODO
    public void ShowPauseMenu(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Debug.Log("Game Paused");
                // toggle UI Pause
                break;
            case GameState.Paused:
                Debug.Log("Playing!");
                // toggle false
                break;
            default:
                break;
        }
    }

}
