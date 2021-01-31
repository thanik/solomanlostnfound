using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Satisfaction
{
    EXTREMELYDISAPPOINTED,
    VERYDISAPPOINTED,
    NEUTRAL,
    VERYSATISFIED,
    EXTREMELYSATISFIED
}

public class UIController : MonoBehaviour
{
    const int titleArraySize = 2;
    const int satisfactionArraySize = 5;
    public Sprite[] satisfactionSprites = new Sprite[satisfactionArraySize];
    [SerializeField]
    private Image satisfactionImage;
    [SerializeField]
    private TextMeshProUGUI levelDateText;
    [SerializeField]
    private TextMeshProUGUI levelTimeText;
    [SerializeField]
    private TextMeshProUGUI levelScore;
    //private Satisfaction satisfactionState;
    public SummaryController summaryController;
    public ComputerScreenController computerScreenController;
    public Canvas pauseCanvas;
    public Image[] starSprites;
    public Sprite[] solomonSprites;
    public string[] titleStatements = new string[titleArraySize];
    public string[] EODStatements = new string[satisfactionArraySize];

    private string eodStatement;

    private void OnValidate()
    {
        if (satisfactionSprites.Length != satisfactionArraySize)
        {
            Debug.LogWarning("Don't change the 'satisfactionSprites' field's array size!");
            Array.Resize(ref satisfactionSprites, satisfactionArraySize);
        }
        if (EODStatements.Length != satisfactionArraySize)
        {
            Debug.LogWarning("Don't change the 'EODStatements' field's array size!");
            Array.Resize(ref EODStatements, satisfactionArraySize);
        }
        if (titleStatements.Length != titleArraySize)
        {
            Debug.LogWarning("Don't change the 'titleStatements' field's array size!");
            Array.Resize(ref EODStatements, satisfactionArraySize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //satisfactionState = Satisfaction.NEUTRAL;
        //satisfactionSprite = satisfactionSprites[(satisfactionSprites.Length - 1) / 2];
    }

    // Event to Update Satisfaction

    // Function to update sprite
    public void UpdateSatisfaction(Satisfaction sState)
    {
        satisfactionImage.sprite = satisfactionSprites[(int) sState];
        eodStatement = EODStatements[(int) sState];
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


    public void ShowSummary(int[] scoreStars, int levelScore, int returnedItems, int lostItems, bool winState)
    {
        // Calculate Stars
        if (levelScore > scoreStars[0])
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

        summaryController.titleText.text = winState ? titleStatements[0] : titleStatements[1];
        summaryController.subtitleText.text = eodStatement;
        summaryController.scoreText.text = levelScore.ToString();
        summaryController.satisfactionLevel.sprite = satisfactionImage.sprite;
        summaryController.correctItemsText.text = returnedItems.ToString();
        summaryController.incorrectItemsText.text = lostItems.ToString();
        summaryController.solomonImage.sprite = winState ? solomonSprites[0] : solomonSprites[1];

        summaryController.nextLevelButton.gameObject.SetActive(winState);

        summaryController.gameObject.SetActive(true);
    }

    //TODO
    public void ShowPauseMenu(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Debug.Log("Game Paused");
                pauseCanvas.gameObject.SetActive(true);
                // toggle UI Pause
                break;
            case GameState.Paused:
                Debug.Log("Playing!");
                pauseCanvas.gameObject.SetActive(false);
                // toggle false
                break;
            default:
                break;
        }
    }

}
