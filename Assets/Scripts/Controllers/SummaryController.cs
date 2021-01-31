using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SummaryController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text subtitleText;
    public TMP_Text scoreText;
    public Image satisfactionLevel;
    public TMP_Text correctItemsText;
    public TMP_Text incorrectItemsText;
    public Image solomonImage;
    public Button nextLevelButton;
    
    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        GameManager.Instance.Retry();
        gameObject.SetActive(false);
    }

    public void ReturnToTitleScreen()
    {
        GameManager.Instance.ReturnToTitle();
        gameObject.SetActive(false);
    }
}
