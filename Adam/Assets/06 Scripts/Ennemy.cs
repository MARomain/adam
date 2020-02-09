using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float health = 3f;




    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        //death particles
        //death sound effects
        Destroy(this.gameObject);
    }

}
