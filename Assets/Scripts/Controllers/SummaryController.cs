using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SummaryController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text scoreText;
    public Image satisfactionLevel;
    public TMP_Text correctItemsText;
    public TMP_Text incorrectItemsText;
    public Image solomonImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSummary(bool isWin, int score, int satisfactionLevel, int numOfStars, int numOfCorrectItems, int numOfIncorrectItems)
    {

    }
}
