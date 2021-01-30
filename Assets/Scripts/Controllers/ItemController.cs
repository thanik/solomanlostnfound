using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter;

public class ItemController : MonoBehaviour
{
    public Vector3 startPos; // spawn position
    public Vector3 endPos; // solomon position

    public float normalizedTime; // 0 - 1: 1 = start time, 0 = time out
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(startPos, endPos, 1 - normalizedTime);
    }

    public void Destroy()
    {

    }

    public void Chop()
    {
        // cut the item
        SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
        {
            lineStart = new Vector2(6f, 3.08f),
            lineEnd = new Vector2(8f, 3.08f),
            gameObject = gameObject,
            gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
        });

        if (output != null && output.secondSideGameObject != null)
        {
            output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
            newRigidbody.bodyType = RigidbodyType2D.Kinematic;
            newRigidbody.velocity = -output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
