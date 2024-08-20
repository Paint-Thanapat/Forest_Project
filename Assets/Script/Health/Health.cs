using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject bodyModel;
    public GameObject deadModel;

    [HideInInspector] public Collider col;
    [HideInInspector] public Rigidbody rb;
    public bool isDead;
    void Awake()
    {
        currentHealth = maxHealth;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        isDead = false;
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (bodyModel)
        {
            bodyModel.SetActive(false);
        }
        if (deadModel)
        {
            deadModel.SetActive(true);
        }

        if (rb)
        {
            rb.isKinematic = true;
        }
        if (col)
        {
            col.enabled = false;
        }
    }
}
