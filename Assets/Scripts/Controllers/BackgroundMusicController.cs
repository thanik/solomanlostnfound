using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource bgm;
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.DOFade(1f, 1f);
    }

    public void FadeOut()
    {
        bgm.DOFade(0f, 1f);
    }
}
