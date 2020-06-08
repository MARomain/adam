using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed = 20f;

    public Rigidbody rb;
    public GameObject effectonground;
    private bool stoped = false;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
     if(!stoped) 
            rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<Ennemy>() != null)
        {
            Ennemy ennemy = other.GetComponentInParent<Ennemy>();
            ennemy.TakeDamage(damage);
        }

        if (other.tag == "reachplayer")
        {
            Destroy(this.gameObject);
       
        }
        else
        {
            Die();
        }

       
    }

    public void Die()
    {
 
        stoped = true;
        GameObject go = Instantiate(effectonground, this.transform.position, this.transform.rotation, null);

        Destroy(this.gameObject);
    }


}
