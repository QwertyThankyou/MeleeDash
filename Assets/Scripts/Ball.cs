using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;
	[HideInInspector] public float timeSpeed = 0.8f;

	[HideInInspector] public Vector2 pos { get { return transform.position; } }

	 public int health = 1;
	 public int damage = 1;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
		
		StatsChanger();
	}

	private void StatsChanger()  // Подгружает значения здоровья и урона из json
	{
		 return;
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
		rb.angularVelocity = 0.2f;
		Time.timeScale = timeSpeed;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Trap"))
		{
			CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
			health--;
			if (health <= 0)
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}
