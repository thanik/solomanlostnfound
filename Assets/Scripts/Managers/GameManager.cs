using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    public LevelDatabase levelsDB;
    private int levelIndex = 0;
    public Image blackScreen;

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
        SceneManager.LoadScene(1, LoadSceneMode.Single);
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
