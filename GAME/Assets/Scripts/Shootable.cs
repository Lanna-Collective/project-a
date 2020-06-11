using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    public GameObject impactEffect;
    public bool damageable;
    public string objectType;

    void Die(string deathBehavior)
    {
        if (deathBehavior == "delete")
        {
            Destroy(gameObject);
        }
        if (deathBehavior == "ragDoll")
        {
            GetComponent<Animator>().enabled = false;
            setRigidBodyState(false);
            setColliderState(true);
            Destroy(gameObject, 10f);
        }

    }

    // similar to setColliderState, 
    // sets all the rigidbodies of the children to state, while changing the parent/controller to !state, as the controller is presumeably a rigidbody controller
    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rd in rigidbodies)
        {
            rd.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }

    // similar to setRigidBodyState,
    // sets the bool of the colliders to state, while changing the parent/controller to !state, as the controller is presumeably a rigidbody controller
    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            GetComponent<Collider>().enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    public void TakeDamage(float damage)
    {
        if (!damageable)
        {
            return;
        }
        health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (objectType == "human enemy")
                Die("ragDoll");
        }
    }
}