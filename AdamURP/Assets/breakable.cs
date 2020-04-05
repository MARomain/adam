using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public Animator animator;
    public float health = 3f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            animator.SetTrigger("die");


        }
        else
        {
            animator.SetTrigger("hit");

        }




    }
    public void Destroy()//pour l'animator
    {
        Destroy(this);
    }
}
