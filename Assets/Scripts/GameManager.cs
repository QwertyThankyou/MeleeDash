using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public UnityEvent enemyDestroy;  // Вызывается при смерти enemy

	public DataManager dataManager;
	public Text health;
	public Text damage;
	
	Camera cam;

	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;
	
	[Header("AudioManager")] 
	public AudioManager audioManager;

	private List<GameObject> portals = new List<GameObject>(); // Лист с GameObject порталами
	private int currentRoom = 0; // Номер комнаты, в которой находится игрок, по умолчанию спавнимся в первой (нулевой)
	public int countPortal = 1;  // Количество порталов на сцене
	public int bossRoom;
	
	[Header("Canvas")]
	public GameObject BossHP;
	public Text money;
	
	public List<int> enemyCountInRoom = new List<int>();  // Лист с колличеством врагов в каждой комнате
	private int enemyKill = 0;  // Ситает убийства в текущей комнате

	bool isDragging = false;

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//---------------------------------------
	void Start ()
	{
		cam = Camera.main;
		ball.DesactivateRb ();
		BossHP.SetActive(false);
		PortalFill();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			isDragging = true;
			OnDragStart ();
		}
		if (Input.GetMouseButtonUp (0)) {
			isDragging = false;
			OnDragEnd ();
		}

		if (isDragging) {
			OnDrag ();
		}

		money.text = Bank.money.ToString();

		// damage.text = ball.damage.ToString();
		// health.text = ball.health.ToString();
	}

	public void SaveButton()
	{
		dataManager.data.heatlh = ball.health;
		dataManager.Save();
	}

	public void LoadButton()
	{
		dataManager.Load();
		ball.damage = Bank.damage;
		ball.health = Bank.health;
	}

	public void MenuButton()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void EnemyKill()  // Вызывается при уничтожении enemy
	{
		enemyKill++;
		if (currentRoom == bossRoom)
		{
			StartCoroutine(LoadMenu());
		}
		else if (enemyCountInRoom[currentRoom] == enemyKill)
		{
			Bank.isDone = true;
			enemyKill = 0;
			PortalActive();
		}
	}

	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene("MainMenu");
	}

	private void PortalFill()  // Заполнение листа с порталами
	{
		for (int i = 1; i <= countPortal; i++)
		{
			portals.Add(GameObject.Find("Portal" + i));
		}

		foreach (var portal in portals)
		{
			portal.SetActive(false);	
		}
	}

	private void PortalActive() // Активация портала в текущей комнате
	{
		portals[currentRoom].SetActive(true);
		currentRoom++;
		if (currentRoom == bossRoom) BossHP.SetActive(true);
	}

	//-Drag--------------------------------------
	void OnDragStart ()
	{
		ball.DesactivateRb ();
		startPoint = cam.ScreenToWorldPoint (Input.mousePosition);

		trajectory.Show ();
	}

	void OnDrag ()
	{
		endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		//Debug.DrawLine (startPoint, endPoint);


		trajectory.UpdateDots (ball.pos, force);
	}

	void OnDragEnd ()
	{
		//push the ball
		ball.ActivateRb ();

		ball.Push (force);

		trajectory.Hide ();
	}

	private void OnDisable()
	{
		portals.Clear();
		enemyCountInRoom.Clear();
	}
}
