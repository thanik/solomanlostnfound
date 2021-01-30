using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PersonController : MonoBehaviour
{
    public Person personData;
    public Image headBaseRenderer;
    public Image eyesRenderer;
    public Image noseRenderer;
    public Image mouthRenderer;
    public Image outfitRenderer;
    public GameObject bubble;
    public TMP_Text answerText;

    public void ShowAnswer(ObjectProperty property)
    {
        bubble.SetActive(true);
        bubble.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f, 2);
        answerText.text = personData.answers[property];
    }

    public void Select()
    {
        //GameManager.Instance.SelectPerson(personData.isLegitOwner);
    }

    public void FadeIn()
    {
        headBaseRenderer.DOFade(1f, 1f);
        eyesRenderer.DOFade(1f, 1f);
        noseRenderer.DOFade(1f, 1f);
        mouthRenderer.DOFade(1f, 1f);
        outfitRenderer.DOFade(1f, 1f);
    }

    public void FadeOut()
    {
        headBaseRenderer.DOFade(0f, 1f);
        eyesRenderer.DOFade(0f, 1f);
        noseRenderer.DOFade(0f, 1f);
        mouthRenderer.DOFade(0f, 1f);
        outfitRenderer.DOFade(0f, 1f);
    }
}
