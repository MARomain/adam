using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieCharger : MonoBehaviour
{
    public bool canAttack = true;
    public bool canMove = true;
    public bool canBekilled = true;
    public Animator animator;
    public float health = 3f;

    public int weapontype = 1;
    public float detectionRange = 5f;
    public float attackRange = 3f;

    //glorykill
    public float glorykilllife = 1f;
    public float livegivedback = 10f;
    public bool opennedtoglorykill = false;
    public float timetodieafterstun = 3;
    private float timer;

    public Rigidbody rb;
    public GameObject projectile;
    public GameObject target;
    public Transform canonTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            animator.SetTrigger("die");
            // Die(); Die est maintenant dans l'animator

        }
        else if (health <= glorykilllife)
        {
            opennedtoglorykill = true;

            animator.SetTrigger("hit");
            animator.SetTrigger("stun");
        }
        else
        {
            animator.SetTrigger("hit");
        }


    }
    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void FindTarget()
    {
        target = GameObject.FindObjectOfType<Player>().gameObject;
    }
    public void AimLeftRight()
    {
        if (target.transform.position.x - this.transform.position.x <= 0)
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            this.transform.localPosition = new Vector3(-1f, 0.3f, 0f);
        }
        else
        {
            this.transform.eulerAngles = Vector3.zero;
            this.transform.localPosition = new Vector3(1f, 0.3f, 0f);
        }
    }
}
