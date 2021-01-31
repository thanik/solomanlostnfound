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
    
    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void Retry()
    {
        GameManager.Instance.Retry();
    }

    public void ReturnToTitleScreen()
    {
        GameManager.Instance.ReturnToTitle();
    }

}
