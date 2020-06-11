using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffMap : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Destroy(other);
    }

}
