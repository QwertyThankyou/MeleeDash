using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Teleporter : MonoBehaviour
{
    public GameObject pointB;
    public GameObject effects;
    public bool entered;

    AudioSource audS;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!entered)
        {
            float offset = 0.1f;
            collision.gameObject.transform.position = pointB.transform.position + new Vector3(0f, offset, 0f);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            Bank.isDone = false;
        }

        entered = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        entered = false;
    }
}