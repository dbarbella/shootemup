using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //public GameObject[] hazards;
    public Vector3 spawnValues;
    public Vector2 spawnAngles;
    public int hazardCount;
    //public float spawnWait;
    //public float startWait;
    public float waveWait;

    public Text scoreText;
    private int score;
    public int startingScore;

    public Text restartText;
    public Text gameOverText;

    public bool gameOver;
    public bool restart;

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    public int getScore()
    {
        return score;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Thank you for playing!\n\nDesign: David Barbella\nConcept Design: Michelle Tong\nMusic: Baris Yaman";
        gameOver = true;
    }

	// Use this for initialization
	void Start ()
    {
        score = startingScore;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        //StartCoroutine(SpawnWaves());
	}
	
}
