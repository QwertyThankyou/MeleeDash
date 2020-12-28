using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooter : MonoBehaviour
{
    private GameManager _gameManager;
    
    [Header("Stats")]
    public int health = 2;     // Здоровье врага
    public float speed = 2f;   // Скорость передвижения
    public float viewRad = 40f;  // Радиус обнаружения

    private Transform _player;
    private Ball _ball;
    
    [Header("Rotate")]
    public float speedRotate = 1f; // Скорость поворота
    public int rotationOffset = -90; // Каким боком будет идти в сторону игрока
    private float _rot;              
    
    [Header("Ammo")]
    public GameObject ammo;           // Будет хранить ссылку на боеприпас
    public GameObject spawnPointAmmo; // Позиция появления боеприпаса
    public float ammoSpeed;           // Скорость 
    public float startTimeBtwShots = 2;
    private float _timeBtwShots;

    [Header("Particle")]
    public GameObject particle;
    
    [Header("Money")]
    public GameObject money;
    public int moneyForDeath = 1;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _ball = GameObject.FindWithTag("Player").GetComponent<Ball>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.enemyDestroy.AddListener(delegate {  });
        
        _timeBtwShots = startTimeBtwShots;
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
        
        if(_timeBtwShots <= 0)
        {
            CreateAmmo();
            _timeBtwShots = startTimeBtwShots;
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
        }
    }
    
    void CreateAmmo()
    {
        Vector2 ammoPosition = spawnPointAmmo.transform.position; // Позиция появления боеприпаса
        Vector2 ammoForse; // Направление силы, которая будет применена к объекту

        // Определяем вектор, по котором должен лететь боеприпас (снаряд)
        float x = spawnPointAmmo.transform.position.x - transform.position.x;
        float y = spawnPointAmmo.transform.position.y - transform.position.y;

        ammoForse = new Vector2(x, y); // Сила с вычисленными параметрами

        // Создаем объект с помощью Instantiate и сохраняем его в переменную createAmmo
        GameObject createAmmo = Instantiate(ammo, ammoPosition, transform.rotation) as GameObject;

        // Применяем силу к боеприпасу (снаряду)
        createAmmo.GetComponent<Rigidbody2D>().AddForce(ammoForse * ammoSpeed, ForceMode2D.Impulse);
    }

    private void TakeDamage(int x) // Наносит урон
    {
        health -= x;
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
}
