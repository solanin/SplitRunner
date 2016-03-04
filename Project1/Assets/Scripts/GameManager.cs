using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

    //Gamestate
    private bool isPaused;
    public bool IsPaused { get { return isPaused; } }
    private bool gameOver; // is it over?
	private bool startedTap = false; //have we begun touching the screen
    public bool GameOver { get { return gameOver; } }
    public void EndGame() { gameOver = true; } // Creates the end state
    private Player player;
    private ObstacleManager OM;
    private GameObject scoreText;
    private GameObject shieldIcon;
    private GameObject multiplierIcon;
    private GameObject slowIcon;
    private float score;
    private bool doubleScore = false;
    private bool shield = false;
    private bool slow = false;
    public float shieldTimer = 0.0f;
    public float multiplierTimer = 0.0f;
    public float slowTimer = 0.0f;
    private float timer;

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameManager s_Instance = null;

    /// <summary>
    /// This defines a static instance property that attempts to find the manager object in the scene and
    /// returns it to the caller.
    /// </summary>
    /// <value>The instance.</value>
    public static GameManager instance {
        get {
            if (s_Instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            // If it is still null, create a new instance
            if (s_Instance == null) {
                GameObject obj = new GameObject("GameManager");
                s_Instance = obj.AddComponent(typeof(GameManager)) as GameManager;
                Debug.Log("Could not locate a GameManager object. GameManager was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    // Use this for initialization
    void Start() {
        gameOver = false;
        isPaused = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        OM = new ObstacleManager();
        OM.gm = this;
        scoreText = GameObject.FindGameObjectWithTag("Score");
        shieldIcon = GameObject.Find("ShieldIcon");
        multiplierIcon = GameObject.Find("MultiplierIcon");
        slowIcon = GameObject.Find("SlowIcon");

        shieldIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;
        multiplierIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;
        slowIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;

        score = 0;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (!isPaused) {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                player.MoveLeft();

            if (Input.GetKeyUp(KeyCode.RightArrow))
                player.MoveRight();

            if (Input.GetKeyUp(KeyCode.Space))
                player.toggleSplit();

				
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startedTap = true;
            }

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && startedTap)
            {
                if (Input.GetTouch(0).deltaPosition.x < -5)
                {
                    player.MoveLeft();
                    startedTap = false;
                }
                if (Input.GetTouch(0).deltaPosition.x > 5)
                {
                    player.MoveRight();
                    startedTap = false;
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && startedTap)
            {
                if (Input.GetTouch(0).deltaPosition.x > -5 && Input.GetTouch(0).deltaPosition.x < 5)
                {
                    player.toggleSplit();
                    startedTap = false;
                }
            }
			
            timer += Time.deltaTime;
            scoreText.GetComponentInChildren<TextMesh>().text = "Score: " + score.ToString("0.##");

            if (shield)
            {
                shieldTimer -= Time.deltaTime;

                if (shieldTimer <= 1.5f)
                {
                    player.FlashShield();
                }

                if (shieldTimer <= 0)
                {
                    shield = false;
                    player.TurnOffShield();
                    shieldIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }

            if (doubleScore)
            {
                multiplierTimer -= Time.deltaTime;

                if (multiplierTimer <= 0)
                {
                    doubleScore = false;
                    multiplierIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }

            if (slow)
            {
                slowTimer -= Time.deltaTime;

                if (slowTimer <= 0)
                {
                    slow = false;
                    slowIcon.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }
        }

        if (GameOver && !isPaused) {
            Pause();
        }
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause() {
		isPaused = true;
		GameObject.Find ("btnPause").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("btnPlay");

		GameObject.Find ("Paused").GetComponent<Renderer> ().enabled = true;
		GameObject.Find ("Menu").GetComponent<Renderer> ().enabled = true;
		GameObject.Find ("btnBack").GetComponent<SpriteRenderer> ().enabled = true;
		GameObject.Find ("btnBack").GetComponent<BoxCollider2D> ().enabled = true;

		if (GameOver) {
			GameObject.Find ("btnPause").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.Find ("btnReplay").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject.Find ("btnReplay").GetComponent<BoxCollider2D> ().enabled = true;
			GameObject.Find ("Paused").GetComponent<TextMesh> ().text = "Game\nOver";

			// Load current scores
			float[] highscore = new float[HighScoreManager.AMT_SAVED];
			for (int i=0; i < highscore.Length; i++) {
				highscore [i] = PlayerPrefs.GetFloat ("Score " + i);
			}

			bool gotHighScore = false;
			int atLoc = highscore.Length;
			for (int i=0; i < highscore.Length; i++) {
				if (score > highscore [i] && !gotHighScore) {
					gotHighScore = true;
					atLoc = i;
				}
			}

			// did you reach a highscore?
			if (gotHighScore && (atLoc < highscore.Length)) {
				insertHighScore (highscore, atLoc);
				
				GameObject.Find ("Paused").GetComponent<TextMesh> ().text = "High\nScore";

				//Save New
				for (int i=0; i < HighScoreManager.AMT_SAVED; i++) {
					PlayerPrefs.SetFloat("Score " + i, highscore [i]);
				}
				PlayerPrefs.Save ();
				Debug.Log("SAVE");
			}
		}
	}

	public void insertHighScore (float[] highscore, int insertAt) {
		for (int i=highscore.Length; i < insertAt; i--) {
			highscore[i] = highscore[i-1];
			Debug.Log(i +" is replaced by " + (i-1));
		}
		highscore[insertAt] = score;
	}

    /// <summary>
    /// Plays the game.
    /// </summary>
    public void Play() {
        isPaused = false;
        GameObject.Find("btnPause").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("btnPause");

        GameObject.Find("Paused").GetComponent<Renderer>().enabled = false;
		GameObject.Find("Menu").GetComponent<Renderer>().enabled = false;
		GameObject.Find ("btnBack").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("btnBack").GetComponent<BoxCollider2D> ().enabled = false;
    }

    ///<summary>
    ///Get Score
    /// </summary>
    public float GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return timer;
    }
    public void addScore()
    {
        score++;
        if (doubleScore)
        {
            score++;
        }
    }
    public void activateShield()
    {
        shield = true;
        shieldTimer = 5.0f;
        player.TurnOnShield();
        player.SetUpFlash();
        shieldIcon.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    public void activateMultiplier()
    {
        doubleScore = true;
        multiplierTimer = 5.0f;
        multiplierIcon.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    public bool Shield() { return shield; }

    public void ActivateSlow()
    {
        slow = true;
        slowTimer = 5.0f;
        slowIcon.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    public bool Slow() { return slow; }

    public void DeactivateShield()
    {
        shieldTimer = 0.05f;
    }
}
