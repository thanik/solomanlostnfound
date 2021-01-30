using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        answerText.text = personData.answers[property];
    }

    public void Select()
    {
        //GameManager.Instance.SelectPerson(personData.isLegitOwner);
    }
}
