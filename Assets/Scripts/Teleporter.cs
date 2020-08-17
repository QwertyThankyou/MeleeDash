using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Teleporter : MonoBehaviour
{
    // The other teleporter
    public GameObject pointB;

    // The effects you're using - remove it if you don't want effects
    public GameObject effects;

    // Checks if the player has either entered/exited
    public bool entered;

    // Sprite renderer of the object, used to get the colour for your effects
    SpriteRenderer spr;
    // MATERIAL VERSION
    // Material mat;
    AudioSource audS;

    // Sound effect you're using - remove it if you don't want sound
    public AudioClip teleport;

    // Gets components - effects and sound will not work without these
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        audS = gameObject.GetComponent<AudioSource>();
        // mat = gameObject.GetComponent<Renderer>().material;
    }

    // Player and teleporter collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!entered)
        {
            // Moves the object that entered - change the offset if your player gets stuck inside
            float offset = 0.1f;
            collision.gameObject.transform.position = pointB.transform.position + new Vector3(0f, offset, 0f);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            // Plays the sound - only if it's defined
            audS.clip = teleport;
            audS.Play();
        }

        // Sets both ends to be classed as entered.
        // Not setting both ends to true will make your character bounce back and forth endlessly
        // Point A has been entered
        entered = true;
        // Point B has been entered

    }

    // The player has left the teleporter
    void OnCollisionExit2D(Collision2D collision)
    {
        // So we set the flag to be false
        // Point B does it automatically as you have left Point B to get to Point A, and vice versa
        entered = false;
    }
}