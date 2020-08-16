using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private int _health = 2;     // Здоровье врага
    private int _damage = 1;     // Урон
    private float _speed = 1f;   // Скорость передвижения

    private float _viewRad = 7.5f;  // Радиус обнаружения

    private Transform _player;
    private Ball _ball;
    
    private float _speedRotate = 1f; // Скорость поворота
    public int rotationOffset = -90; // Каким боком будет идти в сторону игрока
    private float _rot;              // Хуй знает что такое

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _player.position) < _viewRad) Angry();
    }
    
    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        
        Vector2 difference = _player.position - transform.position;
        _rot = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
 
        Quaternion rotation = Quaternion.AngleAxis (_rot + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * _speedRotate);
    }

    private void TakeDamage(int damage) // Наносит урон
    {
        _health -= damage;
    }

    private void GiveDamage(int damage) // Получает урон
    {
        _ball.health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(_ball.damage);
            if (_health <= 0) Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GiveDamage(_damage);
            SceneManager.LoadScene(0);
        }
    }
}