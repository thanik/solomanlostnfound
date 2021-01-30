using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{

    public Image blackScreen;
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
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
    public LevelDatabase levelsDB;
    private int levelIndex = 0;

    public void LoadNextLevel()
    {
        if (levelIndex <= levelsDB.levels.Count)
        {
            SceneManager.LoadScene("Main");
            levelIndex++;
        }
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }

    // keep track of high score
}
