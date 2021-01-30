using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public enum Satisfaction
    {
        EXTREMELYDISAPPOINTED = -3,
        VERYDISAPPOINTED,
        NOTSATISFIED,
        NEUTRAL,
        SLIGHTLYSATISFIED,
        VERYSATISFIED,
        EXTREMELYSATISFIED
    }

    const int spriteArraySize = 7;
    public Image[] satisfactionSprites = new Image[spriteArraySize];
    [SerializeField]
    private Image satisfactionSprite;
    [SerializeField]
    private TextMeshProUGUI levelDateText;
    [SerializeField]
    private TextMeshProUGUI levelTimeText;
    private Satisfaction satisfactionState;
    private int satisfactionScale = 0;

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
    public void UpdateSatisfaction(int point)
    {
        satisfactionScale += point;

        if (satisfactionScale >= 3)
        {
            satisfactionScale = 3;
            satisfactionState = Satisfaction.EXTREMELYSATISFIED;
        }
        else if (satisfactionScale <= -3)
        {
            satisfactionScale = -3;
            satisfactionState = Satisfaction.EXTREMELYDISAPPOINTED;
        }

        if (satisfactionScale < -1 && satisfactionScale >= -2)
        {
            satisfactionState = Satisfaction.VERYDISAPPOINTED;
        }
        else if (satisfactionScale < 0 && satisfactionScale >= -1)
        {
            satisfactionState = Satisfaction.NOTSATISFIED;
        }
        else if (satisfactionScale < 1 && satisfactionScale >= 0)
        {
            satisfactionState = Satisfaction.NEUTRAL;
        }
        else if (satisfactionScale < 2 && satisfactionScale >= 1)
        {
            satisfactionState = Satisfaction.SLIGHTLYSATISFIED;
        }
        else if (satisfactionScale < 3 && satisfactionScale >= 2)
        {
            satisfactionState = Satisfaction.VERYSATISFIED;
        }

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
            case Satisfaction.NOTSATISFIED:
                Debug.Log("Not Satisfied");
                break;
            case Satisfaction.NEUTRAL:
                Debug.Log("Neutral");
                break;
            case Satisfaction.SLIGHTLYSATISFIED:
                Debug.Log("Slightly Satisfied");
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

    public void UpdateLevelTime(float levelTime)
    {
        float minutes = Mathf.FloorToInt(levelTime / 60);
        float seconds = Mathf.FloorToInt(levelTime % 60);
        
        levelTimeText.text = "TIME:  " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //TODO
    public void ShowSummary(bool state)
    {
        if (state)
            Debug.Log("Show Summary!");
        // event if level lost, change content of summary
    }

    //TODO
    public void ShowPauseMenu()
    {

    }

}
