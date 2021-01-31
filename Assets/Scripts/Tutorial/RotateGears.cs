using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGears : MonoBehaviour
{
    public RectTransform gear;

    // Start is called before the first frame update
    void Start()
    {
        gear = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        gear.Rotate(new Vector3(0, 0, 0.1f));
    }
}
