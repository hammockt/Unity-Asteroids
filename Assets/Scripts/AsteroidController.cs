using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

	public AudioClip destroySound;
	public GameObject smallAsteroid;

	private GameController gameController;

	// Use this for initialization
	void Start ()
	{
		//Get a reference to the game controller object and the script
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent<GameController> ();

		//push the asteroid in the direction it is facing
		GetComponent<Rigidbody2D> ().AddForce (transform.up * Random.Range (-50.0f, 150.0f));

		//Give a random angular velocity/rotation
		GetComponent<Rigidbody2D> ().angularVelocity = Random.Range (0.0f, 90.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.gameObject.tag.Equals ("Bullet")) {
			//Destroy the bullet
			Destroy (c.gameObject);

			//If large asteroid, spawn new ones
			if (tag.Equals ("Large Asteroid")) {
				//spawn small asteroids
				Instantiate (smallAsteroid,
					new Vector3 (transform.position.x - 0.5f, transform.position.y - 0.5f, 0),
					Quaternion.Euler (0, 0, 90));

				//spawn another small asteroid
				Instantiate (smallAsteroid,
					new Vector3 (transform.position.x + 0.5f, transform.position.y + 0.5f, 0),
					Quaternion.Euler (0, 0, 0));

				//spawn the last small asteroid
				Instantiate (smallAsteroid,
					new Vector3 (transform.position.x + 0.5f, transform.position.y - 0.5f, 0),
					Quaternion.Euler (0, 0, 270));

				gameController.SplitAsteroids ();
			} else {
				//We were a small asteroid, so it gets destroyed
				gameController.DecrementAsteroids ();
			}

			//play our destroy sound
			AudioSource.PlayClipAtPoint (destroySound, Camera.main.transform.position);

			//add to the score
			gameController.IncrementScore ();

			//Destory the current asteroid
			Destroy (gameObject);
		}
	}
}
