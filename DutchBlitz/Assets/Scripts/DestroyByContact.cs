using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is now primarily for destroying the player.
// It belongs to the player now.

public class DestroyByContact : MonoBehaviour
{
    //public GameObject explosion;
    public GameObject playerExplosion;
    //public int scoreValue;
    private GameController gameController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KetEnemy") || other.CompareTag("MayoEnemy"))
        {
            /*
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            gameController.AddScore(scoreValue);
            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            */
            Destroy(gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
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
