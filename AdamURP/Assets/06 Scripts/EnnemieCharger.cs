using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieCharger : MonoBehaviour
{
    public bool canJump = true;
    public bool canAttack = true;
    public bool canMove = true;
    public Animator animator;

    public float dammage;
    public float speed;
    public float distancetocharge = 5;
    public float reach = 5;
    public float knockback;

    public bool attacking = false;
    public Rigidbody rb;
    private bool faceright=false;
    private float distancefromplayer = 0;
    public GameObject target;
    public Library lb;

    // Start is called before the first frame update
    void Start()
    {
        lb = FindObjectOfType<Library>();
        rb = GetComponent<Rigidbody>();
        if (distancetocharge < reach)
        {
            reach = distancetocharge;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        distancefromplayer= Vector3.Distance(lb.cibleplayer.transform.position, transform.position);
        if (distancefromplayer < distancetocharge)
        {
          
            if (distancefromplayer < reach)
            {
                Attack();
                animator.SetBool("run", false);
            }
            else
            {
                if (attacking == false)
                {
                    if (lb.player.transform.position.y <= (this.transform.position.y +1.4))
                    {
                        Moving();
                        animator.SetBool("run", true);
                    }
                    else
                    {
                        animator.SetBool("run", false);
                    }

                }
              
            }
        }
        else
        {
           // animator.SetBool("run", false);
        }
    }




   public void Moving()
    {
        if (canMove) {
       
                if (this.transform.position.x > lb.player.transform.position.x)
                {
                    //joueur a droite
                    rb.velocity = (new Vector2(-speed, rb.velocity.y));
                    if (!faceright)
                    {
                        this.transform.eulerAngles = new Vector3(0, 0, 0);
                        faceright = true;
                    }
                }
                else
                {
                    rb.velocity = (new Vector2(speed, rb.velocity.y));


                    if (faceright)
                    {
                        this.transform.eulerAngles = new Vector3(0, -180, 0);
                        faceright = false;
                    }
                }
            

        }
    }


    public void Attack()
    {
        animator.SetTrigger("attack");

    }




    public void Damage()
    {
        if (distancefromplayer < reach)
        {
            lb.player.gameObject.GetComponent<Player>().TakeDamage(dammage);
            Vector3 pushdirection = transform.position - lb.player.transform.position;
            lb.player.gameObject.GetComponent<Rigidbody>().AddForce(pushdirection.normalized * knockback);
        }
     
    }



    public void Attackingtrue()
    {
        attacking = true;
    }
    public void Attackingfalse()
    {
        attacking = false;
    }
}
