using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public UnityEvent enemyDestroy;
	
	Camera cam;

	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;

	private List<GameObject> portals = new List<GameObject>();
	private int currentRoom = 0;
	public int countPortal = 1;

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

		for (int i = 1; i <= countPortal; i++)
		{
			portals.Add(GameObject.Find("Portal" + i));
		}

		foreach (var portal in portals)
		{
			portal.SetActive(false);	
		}
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
	}

	public void PortalActive()
	{
		portals[currentRoom].SetActive(true);
		currentRoom++;
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

}
