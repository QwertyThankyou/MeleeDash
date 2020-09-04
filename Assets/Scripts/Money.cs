using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Transform _player;
    private float _speed = 20f;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        if (Bank.isDone == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Bank.money++;
            Destroy(this.gameObject);
        }
    }
}
