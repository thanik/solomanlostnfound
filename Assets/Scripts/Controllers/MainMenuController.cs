using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private AudioSource bgm;
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.DOFade(1f, 2f);
    }
    public void NewGame()
    {
        bgm.DOFade(0f, 0.5f);
        GameManager.Instance.NewGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        bgm.DOFade(0f, 0.5f);
        GameManager.Instance.TransitionToOtherScene("Tutorial");
    }
}
