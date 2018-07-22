using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotator : MonoBehaviour
{

    public float rate;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0.0f, rate, 0.0f);
    }
}
