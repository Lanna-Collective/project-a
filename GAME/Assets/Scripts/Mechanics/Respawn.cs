using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;
    public Vector3 respawnPosition;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = respawnPosition;
            if (rb != null)
            {
                rb.velocity = new Vector3(0, 10, 0);
            }
        }
    }
}