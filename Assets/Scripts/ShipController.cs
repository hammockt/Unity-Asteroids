using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

	float rotationSpeed = 100.0f;
	float thrustForce = 3f;

	public AudioClip crashSound;
	public AudioClip shootSound;

	public GameObject bullet;

	private GameController gameController;

	// Use this for initialization
	void Start ()
	{
		//Get a reference to the game controller object and the script
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent<GameController> ();
	}

	//suposedly this handles WASD controls
	void FixedUpdate ()
	{
		//Rotate the ship if necessary
		transform.Rotate (0, 0, -Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime);

		//thrust the ship if necssary
		GetComponent<Rigidbody2D> ().AddForce (transform.up * thrustForce * Input.GetAxis ("Vertical"));

		//has a bullet been fired?
		if (Input.GetKey (KeyCode.Space))
			ShootBullet ();
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		//anything except a bullet is an asteroid
		if (c.gameObject.tag != "Bullet") {
			AudioSource.PlayClipAtPoint (crashSound, Camera.main.transform.position);

			//Move the sip to the center of the screen
			transform.position = new Vector3 (0, 0, 0);

			//Remove all velocity from the ship
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);

			gameController.DecrementLives ();
		}
	}

	void ShootBullet ()
	{
		//spawn a bullet
		Instantiate (bullet,
			new Vector3 (transform.position.x, transform.position.y, 0),
			transform.rotation);

		//play a shot sound
		AudioSource.PlayClipAtPoint (shootSound, Camera.main.transform.position);
	}
}
