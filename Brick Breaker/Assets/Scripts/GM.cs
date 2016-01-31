using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	public int lives = 3;
	public int bricks = 20;
	public float resetDelay = 1f; //time in sec after the game ends before reset next thing
	public Text livesText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject paddle;
	public GameObject deathParticles;
	public static GM instance = null;

	private GameObject clonePaddle;
	
	void Awake () {	
		//enforce Singleton pattern
		if (instance == null) //check if we have a GM yet
			instance = this; //if not, then we set to this
		else if (instance != this) //GM already exists
			Destroy (gameObject); //want to get rid of that GM, good habit to get into this! do not end up with two copies of GM

		Setup ();
	}

	public void Setup()
	{
			//quaternion.identity for no rotation
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject; //cast as game object
		Instantiate (bricksPrefab, transform.position, Quaternion.identity);
	}

	void CheckGameOver()
	{
		if (bricks < 1) {
			youWon.SetActive (true);
			Time.timeScale = .25f; //go into slow motion for cosmetic effect
			Invoke ("Reset", resetDelay);
	
		}
		if (lives <1) {
			gameOver.SetActive(true);
			Time.timeScale = .25f; //go into slow motion for cosmetic effect
			Invoke ("Reset", resetDelay);
			
		}
	}

	void Reset()
	{
		Time.timeScale = 1f; //go back to normal time
		Application.LoadLevel (Application.loadedLevel); //loads the level that was just loaded

	}

	public void LoseLife() 
	{
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate (deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy (clonePaddle);
		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver ();
	}

	void SetupPaddle()
	{
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject; //cast as game object

	}

	//called from bricks gameobject when they get destroyed
	public void DestroyBrick()
	{
		bricks--;
		CheckGameOver ();
	}


}
