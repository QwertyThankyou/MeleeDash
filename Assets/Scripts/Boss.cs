﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private GameManager _gameManager;
    
    [Header("Stats")]
    public int health = 20;     // Здоровье врага
    public int damage = 1;     // Урон
    public float speed = 2f;   // Скорость передвижения
    public float viewRad = 40f;  // Радиус обнаружения

    private Transform _player;
    private Ball _ball;

    [Header("Particle")]
    public GameObject particle;
    
    public Slider slider;

    private Animator _animator;

    [Header("AudioManager")] 
    public AudioManager audioManager;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.enemyDestroy.AddListener(delegate {  });

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _player.position) < viewRad && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attackboss1")) Angry();

        slider.value = health;
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
    }
    

    private void TakeDamage(int x) // Получает урон
    {
        health -= x;
    }

    private void GiveDamage(int x) // Наносит урон
    {
        _ball.isHurt = true;
        _ball.health -= x;
        _ball.PlayerHurt();
    }

    private void OnCollisionEnter2D(Collision2D other)  // Получает урон от игрока
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(_ball.damage);
            Instantiate(particle, transform.position, Quaternion.identity);
            if (health <= 0)
            {
                CinemachineShake.Instance.ShakeCamera(5f, 1f);
                _gameManager.enemyDestroy.Invoke();
                slider.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    public void GiveDamageGun()
    {
        GiveDamage(damage);
        if (_ball.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
