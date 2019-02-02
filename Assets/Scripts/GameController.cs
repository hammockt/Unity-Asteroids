using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject asteroid;

	private int score;
	private int highScore;
	private int asteroidsRemaining;
	private int lives;
	private int wave;
	private int increaseEachWave = 4;

	public Text scoreText;
	public Text livesText;
	public Text waveText;
	public Text highScoreText;

	// Use this for initialization
	void Start ()
	{
		highScore = PlayerPrefs.GetInt ("highScore", 0);
		BeginGame ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Quit if player presses escape
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	void BeginGame ()
	{
		score = 0;
		lives = 3;
		wave = 1;

		//prepare the HUD
		scoreText.text = "SCORE: " + score;
		highScoreText.text = "HIGHSCORE: " + highScore;
		livesText.text = "LIVES: " + lives;
		waveText.text = "WAVE: " + wave;

		SpawnAsteroids ();
	}

	void SpawnAsteroids ()
	{
		DestroyExistingAsteroids ();

		//Decide how many asteroids to spawn
		//If any asteroids left over the previous game, subtract them
		asteroidsRemaining = (wave * increaseEachWave);

		for (int i = 0; i < asteroidsRemaining; i++) {
			//spawn an asteroid
			Instantiate (asteroid,
				new Vector3 (Random.Range (-9.0f, 9.0f), Random.Range (-6.0f, 6.0f), 0),
				Quaternion.Euler (0, 0, Random.Range (0.0f, 359.0f)));
		}

		waveText.text = "WAVE: " + wave;
	}

	public void IncrementScore ()
	{
		score++;

		scoreText.text = "SCORE: " + score;

		if (score > highScore) {
			highScore = score;
			highScoreText.text = "HIGHSCORE: " + highScore;
		}

		//Has the player destroyed all the asteroids?
		if (asteroidsRemaining < 1) {
			//start the next wave
			wave++;
			SpawnAsteroids ();
		}
	}

	public void DecrementLives ()
	{
		lives--;
		livesText.text = "LIVES: " + lives;

		//has the player run out of lives?
		if (lives < 1) {
			//restart the game
			BeginGame ();
		}
	}

	public void DecrementAsteroids ()
	{
		asteroidsRemaining--;
	}

	public void SplitAsteroids ()
	{
		//Two extra asteroids
		// - big one
		// + 3 little ones
		// = 2
		asteroidsRemaining += 2;
	}

	void DestroyExistingAsteroids ()
	{
		GameObject[] largeAsteroids = GameObject.FindGameObjectsWithTag ("Large Asteroid");

		foreach (GameObject largeAsteroid in largeAsteroids)
			GameObject.Destroy (largeAsteroid);

		GameObject[] smallAsteroids = GameObject.FindGameObjectsWithTag ("Small Asteroid");
		foreach (GameObject smallAsteroid in smallAsteroids)
			GameObject.Destroy (smallAsteroid);
	}
}
