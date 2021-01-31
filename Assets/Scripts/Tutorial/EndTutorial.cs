using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    public bool endTutorial = false;
    public bool ended = false;

    // Update is called once per frame
    void Update()
    {
        if (endTutorial && !ended) 
        {
            GameManager.Instance.ReturnToTitle();
            endTutorial = false;
            ended = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ReturnToTitle();

        }
    }
}
