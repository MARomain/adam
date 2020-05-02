using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed = 20f;

    public Rigidbody rb; 

    private void FixedUpdate()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Ennemy>() != null)
        {
            Ennemy ennemy = other.GetComponentInParent<Ennemy>();
            ennemy.TakeDamage(damage);
        }

        Die();
    }

    public void Die()
    {
        //death particles
        //death sound effects
        Destroy(this.gameObject);
    }
}
