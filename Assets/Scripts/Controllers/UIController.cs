using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
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
    private TextMeshProUGUI levelDate;
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

    private void OnEnable()
    {
        GameManager.Instance.OnSatisfactionUpdated += UpdateSatisfaction;
        GameManager.Instance.OnDateUpdated += UpdateDate;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSatisfactionUpdated -= UpdateSatisfaction;
        GameManager.Instance.OnDateUpdated -= UpdateDate;
    }

    // Event to Update Satisfaction
    void UpdateSatisfaction(int point)
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

    void UpdateDate(string date)
    {
        levelDate.text = date;
    }

    // Function to update sprite
    void UpdateSatisfactionSprite()
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

    //TODO
    void ShowSummary()
    {
        // event if level lost, change content of summary
    }

    //TODO
    void ShowPauseMenu()
    {

    }

}
