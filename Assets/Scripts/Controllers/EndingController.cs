using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingController : MonoBehaviour
{
    public Image EmployeeOfTheWeekImage;
    public RectTransform creditText;
    void Start()
    {
        Sequence newSequence = DOTween.Sequence();
        newSequence.Append(EmployeeOfTheWeekImage.DOFade(1f, 1f));
        newSequence.Append(EmployeeOfTheWeekImage.DOFade(1f, 3f));
        newSequence.Append(EmployeeOfTheWeekImage.DOFade(0f, 1f));
        newSequence.Append(creditText.DOAnchorPosY(750f, 15f));
        newSequence.AppendCallback(GameManager.Instance.ReturnToTitle);
    }
}
