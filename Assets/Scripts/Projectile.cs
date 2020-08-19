using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{

    // public float speed;
    //
    // private Transform player;
    // private Vector2 target;
    //
    // void Start()
    // {
    //     player = GameObject.FindGameObjectWithTag("Player").transform; 
    //     target = new Vector2(player.position.x, player.position.y);
    // }
    //
    // void Update()
    // {
    //     transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //     if (transform.position.x == target.x && transform.position.y == target.y)
    //     {
    //         DestroyProjectile();
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
}