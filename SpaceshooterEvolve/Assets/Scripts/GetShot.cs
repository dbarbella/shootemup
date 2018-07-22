/* Hazards use this script to handle being shot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour {

    public float hitPoints;
    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerShot"))
        {


            // We need to decrement the hitPoints by the damage of the playerShot
            // How can we define this? How can we get this?
            // Do we just have a PlayerShotProperties script?
            // We may need something like rb = GetComponent<Rigidbody>(); here?
            PlayerShotProperties shotProps = other.GetComponent<PlayerShotProperties>();
            //Debug.Log(hitPoints);
            hitPoints -= shotProps.ReportDamage();
            //Debug.Log(hitPoints);
            if (hitPoints <= 0)
            {
                Debug.Log("HP is less than 0");
                GetDestroyed();
            }
            // This will eventually need to test for piercing shots and not destroy the player shot if it
            // is a piercing shot.
            DestroyBullet(other.gameObject, shotProps);
        }
    }

    void DestroyBullet(GameObject other, PlayerShotProperties shotProps)
    {
        Instantiate(shotProps.poof, other.transform.position, other.transform.rotation);
        Destroy(other);
    }

    void GetDestroyed ()
    {
        Destroy(gameObject);
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        gameController.AddScore(scoreValue);
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
