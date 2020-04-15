﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBullet : MonoBehaviour
{
    public float damage;
    public float speed = 20f;
    public float knockbackforce = 0;

    public Rigidbody rb; 

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(damage);
            Vector3 pushdirection = transform.position - other.transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(pushdirection.normalized * -knockbackforce);
           
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
