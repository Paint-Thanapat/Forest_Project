using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerCharacter;

    public override void Awake()
    {
        base.Awake();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DamageArea(Vector3 point, float damage, float radius)
    {
        // - add force
        Collider[] collidersToHealth = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToHealth)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
    public void DamageArea(Vector3 point, float damage, float radius, LayerMask attackLayer)
    {
        // - add force
        Collider[] collidersToHealth = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToHealth)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                if (health.gameObject.layer == attackLayer)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void ExplosionForce(Vector3 point, float force, float radius)
    {
        // - add force
        Collider[] collidersToMove = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.gameObject.layer == 10)
                    rb.AddExplosionForce(force * 200f, point, radius);
            }
        }
    }
}
