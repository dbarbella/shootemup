using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float x_min, x_max, z_min, z_max;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shotKet;
    public GameObject shotMayo;
    public Transform shotSpawnKet;
    public Transform shotSpawnMayo;

    public float fireRate;
    private float nextFireKet;
    private float nextFireMayo;

    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireKet)
        {
            nextFireKet = Time.time + fireRate;
            Instantiate(shotKet, shotSpawnKet.position, shotSpawnKet.rotation);
            audioSource.Play();
        }
        if (Input.GetButton("Fire2") && Time.time > nextFireMayo)
        {
            nextFireMayo = Time.time + fireRate;
            Instantiate(shotMayo, shotSpawnMayo.position, shotSpawnMayo.rotation);
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.x_min, boundary.x_max),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.z_min, boundary.z_max));

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
