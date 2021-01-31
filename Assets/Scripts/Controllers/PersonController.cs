using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PersonController : MonoBehaviour
{
    public Person personData;
    public RectTransform PersonSpritesRectTransform;
    public Image headBaseRenderer;
    public Image eyesRenderer;
    public Image noseRenderer;
    public Image mouthRenderer;
    public Image outfitRenderer;
    public GameObject bubble;
    public TMP_Text answerText;
    public LevelController levelController;
    public Button selectButton;

    public AudioClip[] answerSound;
    private AudioSource audioSrc;
    private Animator animator;

    //private bool selected = false;
    private bool answerShowed = false;
    public void ShowAnswer(ObjectProperty property)
    {
        Sequence mySequence = DOTween.Sequence();
        if (!answerShowed)
        {
            bubble.SetActive(true);

            mySequence.Append(PersonSpritesRectTransform.DOAnchorPos(new Vector2(-90f, 0), 0.25f));
            mySequence.Join(bubble.GetComponent<RectTransform>().DOAnchorPos(new Vector2(115f, 0), 0.25f));
            answerShowed = true;
        }
        mySequence.Append(bubble.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f, 2));
        answerText.text = personData.answers[property];
    }

    public void Select()
    {
        //selected = true;
        if (levelController.item)
        {
            animator.SetTrigger(personData.isLegitOwner ? "Correct" : "Wrong");
            audioSrc.PlayOneShot(personData.isLegitOwner ? answerSound[0]: answerSound[1]);
            levelController.item.isOnConveyorBelt = false;
            Vector3 personPos = Camera.main.ScreenToWorldPoint(GetComponent<RectTransform>().position);
            levelController.item.transform.DOJump(personPos, 2f, 1, 0.5f);
            levelController.SelectPerson(personData.isLegitOwner);
        }
    }

    public void DestroyFromGame()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(DelayDestroy(1f));
    }

    IEnumerator DelayDestroy(float secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(this.gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    /*public void FadeIn()
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
    }*/
}
