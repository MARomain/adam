using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    public bool canBekilled = true;

    public Animator animator;
    public float health = 3f;
    //glorykill
    public float glorykilllife = 1f;
    public float livegivedback = 10f;
    public bool opennedtoglorykill = false;
    public float timetodieafterstun = 3;
    private float timer;
    public int weapontype = 0;


    public Rigidbody rb;
    public Library lb;

    // Start is called before the first frame update
    void Start()
    {
      //  originalrotation = new Vector3(bassin.transform.rotation.eulerAngles.x, bassin.transform.rotation.eulerAngles.y, bassin.transform.rotation.eulerAngles.z);
        rb = GetComponent<Rigidbody>();
        lb = FindObjectOfType<Library>();


        if (health <= glorykilllife)
        {
            opennedtoglorykill = true;
            animator.SetTrigger("stun");
        }



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
            animator.SetBool("die",true);

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

    public void Glorykill()
    {
        if (opennedtoglorykill)
        {
            //TO DO teleporter le joueur sur la postion de l'ennemie ->lancer les animations ->depop ennemie ->change item 
            Debug.Log("Glorykill");
            animator.SetTrigger("glorykill");



            GameObject[] playerGO;
            playerGO = GameObject.FindGameObjectsWithTag("Player");
            Player player;
            player = playerGO[0].GetComponent<Player>();


            if ((player.health < player.maxhealth)&&(livegivedback>0))
            {
                player.Heal(livegivedback);
            }
            if (weapontype != 0)
            {
                player.weapontype = weapontype;
                player.RefillAmmo();
            }


            opennedtoglorykill = false;
        }
    }
}
