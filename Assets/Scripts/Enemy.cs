using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Enemy
{
    public int health;
    public int damage;
    public int speed;

    public void TakeDamage(int x)
    {
        health -= x;
    }

    // public void Spawn(Vector2 begin, Vector2 end)
    // {
    //     Random random = new Random();
    //     float randX = Random.Range(begin.x, end.x);
    //     float randY = Random.Range(begin.y, end.y);
    //     
    //     Vector2 enemyPos = new Vector3(randX, randY);
    //
    //     Instantiate(dummy, dummyPos, Quaternion.Euler(0, -90, 0));
    // }
}
