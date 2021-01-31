using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.NewGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Tutorial()
    {

    }
}
