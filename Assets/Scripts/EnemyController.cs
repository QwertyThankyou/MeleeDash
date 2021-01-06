using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private GameManager _gameManager;
    
    [Header("Stats")]
    public int health = 2;     // Здоровье врага
    public int damage = 1;     // Урон
    public float speed = 2f;   // Скорость передвижения
    public float viewRad = 40f;  // Радиус обнаружения

    private Transform _player;
    private Ball _ball;
    
    [Header("Rotate")]
    public float speedRotate = 1f; // Скорость поворота
    public int rotationOffset = -90; // Каким боком будет идти в сторону игрока
    private float _rot;              // Хуй знает что такое
    
    [Header("Particle")]
    public GameObject particle;
    
    [Header("Money")]
    public GameObject money;
    public int moneyForDeath = 1;
    
    [Header("AudioManager")] 
    public AudioManager audioManager;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.enemyDestroy.AddListener(delegate {  });
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _player.position) < viewRad) Angry();
    }
    
    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
        
        Vector2 difference = _player.position - transform.position;
        _rot = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
 
        Quaternion rotation = Quaternion.AngleAxis (_rot + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * speedRotate);
    }

    private void TakeDamage(int x) // Наносит урон
    {
        health -= x;
    }

    private void GiveDamage(int x) // Получает урон
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
                for (int i = 0; i < moneyForDeath; i++)
                {
                    Instantiate(money, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)  // Наносит урон игроку и перезапускает сцену
    {
        if (other.CompareTag("Player") && _ball.isHurt == false)
        {
            GiveDamage(damage);
            if (_ball.health <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}