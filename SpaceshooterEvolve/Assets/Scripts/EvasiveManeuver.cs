using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float targetManeuver;
    private float currentSpeed;
    public float smoothing;
    public float tilt;

    public Boundary boundary;

    private Rigidbody rb;

    public float dodge;
    

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
        currentSpeed = rb.velocity.z;
	}
	
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime*smoothing);
        rb.velocity = new Vector3(newManeuver, rb.velocity.y, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.x_min, boundary.x_max),
            rb.position.y,
            Mathf.Clamp(rb.position.z, boundary.z_min, boundary.z_max));
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
