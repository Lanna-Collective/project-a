using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;
    public float blastRadius = 1f;
    public float blastForce = 1f;

    private float bulletDamage;
    private Shootable targetObject;
    private Vector3 gunPos;

    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }

    }

    public void setTarget(Vector3 target, Shootable s, float gunDamage, GameObject gun)
    {
        m_target = target;
        targetObject = s;
        bulletDamage = gunDamage;
        gunPos = gun.transform.position;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);

            if (targetObject is Shootable)
            {
                GameObject impactGO = Instantiate(targetObject.impactEffect, transform.position, transform.rotation);
                Destroy(impactGO, 1f);
                targetObject.TakeDamage(bulletDamage);
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider c in colliders)
            {
                Rigidbody rb = c.GetComponent<Rigidbody>() as Rigidbody;
                if (rb != null)
                {
                    // rb.AddExplosionForce(blastForce, transform.position, blastRadius);
                    Vector3 forceDirection = transform.position - gunPos;
                    rb.AddForceAtPosition(forceDirection * blastForce, transform.position);
                }
            }
            Destroy(explosion, 1f);
            Destroy(gameObject);
        }
    }

}