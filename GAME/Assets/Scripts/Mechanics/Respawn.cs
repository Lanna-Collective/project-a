using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;
    public Vector3 respawnPosition;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
            transform.position = respawnPosition;
    }
}