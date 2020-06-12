using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shootable : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    public GameObject impactEffect;
    public bool damageable;

    [Tooltip("Delete = 0, HumanEnemy = 1, Explosive = 2, Enviroment = 3")]
    public int objectType;

    public enum objectTypeEnum
    {
        delete,         //0
        humanEnemy,     //1
        explosives,     //2
        enviroment      //3
    }
    void Start()
    {

    }

    /// <summary>
    /// Kills the Shootable depending on what type of object it is. See objectTypeEnum
    /// </summary>
    /// <param name="deathBehavior">objectTypeEnum.</para  m>
    /// <returns>Returns an integer based on the passed value.</returns>
    public abstract void Die();
   
    public void TakeDamage(float damage)
    {
        if (!damageable)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}