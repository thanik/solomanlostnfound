using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
