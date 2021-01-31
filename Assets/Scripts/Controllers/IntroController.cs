using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class IntroController : MonoBehaviour
{
    public Image[] slides;
    public GameObject[] texts;
    private int currentSlide = 0;
    private AudioSource bgm;
    public void NextSlide()
    {
        slides[currentSlide].DOFade(0f, 1f);
        if (currentSlide + 1 < slides.Length)
        {
            slides[currentSlide + 1].gameObject.SetActive(true);
            slides[currentSlide + 1].DOFade(1f, 1f);
            texts[currentSlide].SetActive(false);
            texts[currentSlide+1].SetActive(true);
            currentSlide++;
        }
        else
        {
            bgm.DOFade(0f, 0.25f);
            GameManager.Instance.StartTransitionToLevel();
        }
    }
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.DOFade(1f, 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            NextSlide();
        }
    }
}
