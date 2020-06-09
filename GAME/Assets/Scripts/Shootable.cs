using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {
    // Start is called before the first frame update

    public float health = 50f;
    public GameObject impactEffect;
    public bool damageable;
    public string objectType;

    void Die(string deathBehavior) {
        if (deathBehavior == "delete") {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(float damage) {
        if (!damageable) {
            return;
        }
        health -= damage;
        if (health <= 0) {
            if (objectType == "human enemy")
                Die("delete");
        }

    }

    // Update is called once per frame
    void Update() {

    }
}