using System.Collections;
using System.Collections.Generic;
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


    public Rigidbody rb;
    public Library lb;

    // Start is called before the first frame update
    void Start()
    {
      //  originalrotation = new Vector3(bassin.transform.rotation.eulerAngles.x, bassin.transform.rotation.eulerAngles.y, bassin.transform.rotation.eulerAngles.z);
        rb = GetComponent<Rigidbody>();
        lb = FindObjectOfType<Library>();

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
        //TO DO teleporter le joueur sur la postion de l'ennemie ->lancer les animations ->depop ennemie ->change item 
        Debug.Log("Glorykill");
        animator.SetTrigger("glorykill");
        GameObject[] playerGO;
        playerGO = GameObject.FindGameObjectsWithTag("Player");
        Player player;
        player = playerGO[0].GetComponent<Player>();
        player.Heal(livegivedback);
        EnnemyRangeFighter ERF = GetComponent<EnnemyRangeFighter>();
        if (ERF != null)
        {
            player.weapontype = ERF.weapontype;
            player.ammoleft = lb.weapon1munitions;
        }
        Ennemy_lazerfighter EZF = GetComponent<Ennemy_lazerfighter>();
        if (EZF != null)
        {
            player.weapontype = EZF.weapontype;
            player.ammoleft = lb.weapon2munitions;
        }

    }
}
