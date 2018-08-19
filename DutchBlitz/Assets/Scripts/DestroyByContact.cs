using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is now primarily for dealing with player collisions with fries.
// It belongs to the player now.

public class DestroyByContact : MonoBehaviour
{
    //public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject enemyExplosion;
    public int collisionScoreValue;
    
    //public int scoreValue;
    private GameController gameController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KetEnemy") || other.CompareTag("MayoEnemy"))
        {
            Destroy(other.gameObject);
            gameController.AddScore(-collisionScoreValue);
            Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
            // Play a sad sound
            // Play an explosion

        }
    }

    private void Update()
    {
        if (gameController.getScore() <= 0)
        {
            Destroy(gameObject);
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        }
    }

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }
    }
}

