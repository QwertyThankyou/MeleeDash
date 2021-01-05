using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;
	[HideInInspector] public float timeSpeed = 0.8f;

	[HideInInspector] public Vector2 pos { get { return transform.position; } }
	
	 public int health = 1;
	 public int damage = 1;

	 [HideInInspector]public bool isHurt = false;
	 
	 [Header("AudioManager")] 
	 public AudioManager audioManager;

	 [Header("Canvas")]
	 public Slider healthT;

	 [Header("Skins")] 
	 public List<Sprite> sprites = new List<Sprite>();
	 
	 private GameObject _particleMagnit;
	 private ParticleSystem _particleSystem;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
		_particleMagnit = transform.Find("HurtPlayer").gameObject;
		_particleSystem = _particleMagnit.GetComponent<ParticleSystem>();
		_particleMagnit.SetActive(false);
		GetComponent<SpriteRenderer>().sprite = sprites[Bank.currentSkin];

		//StatsChanger();
	}

	private void Update()
	{
		healthT.value = health;
	}

	public void PlayerHurt()
	{
		StartCoroutine(HurtPlayer());
	}
	
	private IEnumerator HurtPlayer()
	{
		_particleMagnit.SetActive(true);
		yield return new WaitForSeconds(_particleSystem.main.duration);
		_particleMagnit.SetActive(false);
		isHurt = false;
	}

	private void StatsChanger()  // Подгружает значения здоровья и урона из json
	{
		health = Bank.health;
		damage = Bank.damage;
	}

	public void Push (Vector2 force)
	{
		rb.AddForce (force, ForceMode2D.Impulse);
	}

	public void ActivateRb ()
	{
		Time.timeScale = 1.0f;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0.5f;
		Time.timeScale = timeSpeed;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Trap"))
		{
			CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
			StartCoroutine(HurtPlayer());
			health--;
			if (health <= 0)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		// if (other.gameObject.CompareTag("Enemy"))
		// {
		// 	audioManager.Play("");
		// }
		// if (other.gameObject.CompareTag("Wall"))
		// {
		// 	audioManager.Play("");
		// }
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
