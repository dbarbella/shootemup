using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{

    public int missScoreValue;
    private GameController gameController;
    public GameObject enemySplatExplosion;

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        
        if (other.CompareTag("KetEnemy") || other.CompareTag("MayoEnemy"))
        {
            // This should also play a sad sound
            gameController.AddScore(-missScoreValue);
            Instantiate(enemySplatExplosion, other.transform.position, other.transform.rotation);
        }
        Destroy(other.gameObject);
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
