using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public float damage;
    [Range(0.0f, 1.0f)]
    public float speedmultiply;
    public float speed = 20f;


    public Rigidbody rb;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = -transform.right *( speed*speedmultiply);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(damage);
        }

        Die();
    }

    private void Update()
    {
        rb.velocity = -transform.right * (speed * speedmultiply);
    }
    public void Die()
    {
        //death particles
        //death sound effects
        Destroy(this.gameObject);
    }
}
