using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Shootable
{
    // Start is called before the first frame update
    void Start()
    {
        setRigidBodyState(true);
        setColliderState(false);
        GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    // sets all the rigidbodies of the children to state, while changing the parent/controller to !state, as the controller is presumeably a rigidbody controller
    // sister function to setColliderState, 
    /// </summary>// 
    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rd in rigidbodies)
        {
            rd.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }

    /// <summary>
    // sister function to setRigidBodyState,
    // sets the bool of the colliders to state, while changing the parent/controller to !state, as the controller is presumeably a rigidbody controller
    /// </summary>// 
    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            GetComponent<Collider>().enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    public override void Die()
    {
        if (objectType == (int)objectTypeEnum.humanEnemy)
        {
            GetComponent<Animator>().enabled = false;
            setRigidBodyState(false);
            setColliderState(true);
            Destroy(gameObject, 20f);
        }
    }
}
