/* Hazards use this script to handle being shot
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour {

    public float hitPoints;
    public GameObject goodExplosion;
    public GameObject badExplosion;
    public int goodScoreValue;
    public int badScoreValue;
    private GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MayoShot") || other.CompareTag("KetShot"))
        {
            PlayerShotProperties shotProps = other.GetComponent<PlayerShotProperties>();
            hitPoints -= shotProps.ReportDamage();
            if ((other.CompareTag("MayoShot") && CompareTag("MayoEnemy")) ||
                (other.CompareTag("KetShot") && CompareTag("KetEnemy")))
            {
                GoodDestroyed();
            }
            if ((other.CompareTag("KetShot") && CompareTag("MayoEnemy")) ||
                (other.CompareTag("MayoShot") && CompareTag("KetEnemy")))
            {
                BadDestroyed();
            }
            DestroyBullet(other.gameObject, shotProps);
        }
    }

    void DestroyBullet(GameObject other, PlayerShotProperties shotProps)
    {
        Instantiate(shotProps.poof, other.transform.position, other.transform.rotation);
        Destroy(other);
    }

    void GoodDestroyed ()
    {
        Destroy(gameObject);
        if (goodExplosion != null)
        {
            Instantiate(goodExplosion, transform.position, transform.rotation);
        }
        gameController.AddScore(goodScoreValue);
    }

    void BadDestroyed()
    {
        Destroy(gameObject);
        if (badExplosion != null)
        {
            Instantiate(badExplosion, transform.position, transform.rotation);
        }
        gameController.AddScore(-badScoreValue);
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
