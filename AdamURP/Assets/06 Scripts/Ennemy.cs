using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    public bool canAttack = true;
    public bool canMove = true;
    public bool canBekilled = true;

    public Animator animator;
    public float health = 3f;

    public int weapontype = 1;
    public float detectionRange = 5f;
    public float attackRange = 3f;
    public float knockbackforce = 5;

    //glorykill
    public float glorykilllife = 1f;
    public float livegivedback = 10f;
    public bool opennedtoglorykill = false;
    public float timetodieafterstun = 3;
    private float timer;

    public Vector3 lookpos;
    public bool lookplayer = true;
    public GameObject gyroscope; //ca c'est du debug bien sale ,moyen opti ,qui est pas tres beau,qui sert pour le calcule de rotation 
    public bool faceright = true;
  
    public GameObject bassin;
    public Vector3 originalrotation;
    public Vector3 calculaterotation;
    public Rigidbody rb;
    public GameObject projectile;
    public GameObject target;
    public Transform canonTransform;

    // Start is called before the first frame update
    void Start()
    {
      //  originalrotation = new Vector3(bassin.transform.rotation.eulerAngles.x, bassin.transform.rotation.eulerAngles.y, bassin.transform.rotation.eulerAngles.z);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lookplayer == true)
        {
            //calculaterotation = new Vector3(gyroscope.transform.rotation.eulerAngles.x + originalrotation.x, originalrotation.y, originalrotation.z);

            if (target.transform.position.x > this.transform.position.x)
            {
                if (faceright)
                {
                    this.transform.Rotate(0, 180, 0);
                    faceright = false;
                }
            
              calculaterotation = new Vector3(gyroscope.transform.rotation.eulerAngles.x + originalrotation.x, -originalrotation.y, originalrotation.z);
            }
            else if (target.transform.position.x < this.transform.position.x) 
            {
                if (!faceright)
                {
                    this.transform.Rotate(0, 180, 0);
                    faceright = true;
                }
                calculaterotation = new Vector3(gyroscope.transform.rotation.eulerAngles.x + originalrotation.x, originalrotation.y, originalrotation.z);
              
            }
            bassin.transform.eulerAngles = calculaterotation;

        }
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
    public void Glorykill()
    {
        //TO DO teleporter le joueur sur la postion de l'ennemie ->lancer les animations ->depop ennemie ->change item 
        Debug.Log("Glorykill");
        animator.SetTrigger("glorykill");
        GameObject[] playerGO;
        playerGO = GameObject.FindGameObjectsWithTag("Player");
        Player player;
        player = playerGO[0].GetComponent<Player>();
        player.ammoleft = player.weaponmaxammo;
        player.Heal(livegivedback);
        player.weapontype = weapontype;

    }
}
