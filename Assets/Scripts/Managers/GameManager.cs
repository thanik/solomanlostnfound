using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Animator = UnityEngine.Animator;


public class GameManager : Singleton<GameManager>
{
    public LevelDatabase levelsDB;
    private int levelIndex = 0;
    public Image blackScreen;
    public TMP_Text levelTitleText;
    public Animator animator;

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        // intro?
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(1f, 1f);
    }

    public void FadeOut()
    {
        blackScreen.DOFade(0f, 1f);
        blackScreen.gameObject.SetActive(false);
    }

    public void ShowText(string text)
    {
        levelTitleText.gameObject.SetActive(true);
        levelTitleText.text = text;
        animator.SetTrigger("ShowAndHideText");
        StartCoroutine(ShowTextCoroutine());
    }

    IEnumerator ShowTextCoroutine()
    {
        yield return new WaitForSeconds(4f);
        levelTitleText.gameObject.SetActive(false);
    }
   
    public void LoadNextLevel()
    {
        if (levelIndex < levelsDB.levels.Count - 1)
        {
            SceneManager.LoadScene("Main");
            levelIndex++;
        }
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }

    

    // keep track of high score
}
