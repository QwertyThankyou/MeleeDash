using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    private Ball _ball;
    private Boss _boss;
    private void Start()
    {
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();
        _boss = GetComponentInParent<Boss>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)  // Наносит урон игроку и перезапускает сцену
    {
        if (other.CompareTag("Player") && _ball.isHurt == false)
        {
            _boss.GiveDamageGun();
        }
    }
}
