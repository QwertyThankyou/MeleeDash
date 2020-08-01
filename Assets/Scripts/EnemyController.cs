using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int _health = 2;
    private int _damage;
    private float _speed = 1f;

    private float _viewRad = 7.5f;
    private float _patrolRad = 2.5f;

    private Transform _player;
    private Transform _point;

    private Ball _ball;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();
        _point = transform;
    }

    void Update()
    {
        //if (Vector2.Distance(transform.position, _point.position) < _patrolRad) Chill();
        if (Vector2.Distance(transform.position, _player.position) < _viewRad) Angry();
        else GoBack();
    }

    void Chill()
    {
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);

    }

    private void TakeDamage(int damage)
    {
        _health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(_ball.damage);
            if (_health <= 0) Destroy(this.gameObject);
        }
    }
}