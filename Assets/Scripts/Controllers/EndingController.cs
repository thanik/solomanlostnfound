using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingController : MonoBehaviour
{
    public Image EmployeeOfTheWeekImage;
    public RectTransform creditText;
    public int step;
    public GameObject mouseIcon;
    private Animator animator;
    private AudioSource bgm;
    void Start()
    {
        animator = GetComponent<Animator>();
        bgm = GetComponent<AudioSource>();
        bgm.DOFade(1f, 1f);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            if (step == 0)
            {
                animator.SetTrigger("EndingIn");
            }
            else
            {
                mouseIcon.SetActive(false);
                animator.SetTrigger("EndingOut");
                Sequence newSequence = DOTween.Sequence();
                newSequence.Append(EmployeeOfTheWeekImage.DOFade(0f, 1f));
                newSequence.Append(creditText.DOAnchorPosY(750f, 15f).SetEase(Ease.Linear));
                newSequence.Append(bgm.DOFade(0f, 0.25f));
                newSequence.AppendCallback(GameManager.Instance.ReturnToTitle);
            }
            step++;
            
        }
    }
}
