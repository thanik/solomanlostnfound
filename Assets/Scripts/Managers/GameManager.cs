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

    public void NewGame()
    {
        levelIndex = 0;
        TransitionToOtherScene("Intro");
    }

    public void TransitionToOtherScene(string sceneName)
    {
        StartCoroutine(TransitionToOtherSceneCoroutine(sceneName));
    }

    public IEnumerator TransitionToOtherSceneCoroutine(string sceneName)
    {
        FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(1f);
        FadeOut();
    }

    public void StartTransitionToLevel()
    {
        FadeToBlack();
        StartCoroutine(TransitionToLevel());
    }

    IEnumerator TransitionToLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Retry()
    {
        FadeToBlack();
        StartCoroutine(TransitionToLevel());
    }

    public void FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(1f, 1f);
    }

    public void FadeOut()
    {
        blackScreen.DOFade(0f, 1f);
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(1f);
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
            levelIndex++;
            FadeToBlack();
            StartCoroutine(TransitionToLevel());
        }
        else
        {
            FadeToBlack();
            StartCoroutine(TransitionToEnding());
        }
    }
    IEnumerator TransitionToEnding()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Ending", LoadSceneMode.Single);
        FadeOut();
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }

    

    // keep track of high score
}
