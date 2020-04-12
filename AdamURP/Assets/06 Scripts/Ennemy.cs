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

    public bool lookplayer = true;
    public GameObject gyroscope; //ca c'est du debug bien sale ,moyen opti ,qui est pas tres beau,qui sert pour le calcule de rotation 
    public bool faceright = true;


    //tire
    public float précision=3;
    public bool insight = false;
    public GameObject projectile;
    public Transform Cannondirection;
    public GameObject cannontip;
    public bool canshoot = true;
    public float firerate = 1;

    public GameObject bassin;
    public Vector3 originalrotation;
    public Vector3 calculaterotation;
    public Rigidbody rb;
    public GameObject target;
    public Library lb;

    // Start is called before the first frame update
    void Start()
    {
      //  originalrotation = new Vector3(bassin.transform.rotation.eulerAngles.x, bassin.transform.rotation.eulerAngles.y, bassin.transform.rotation.eulerAngles.z);
        rb = GetComponent<Rigidbody>();
        lb = FindObjectOfType<Library>();
        target = lb.cibleplayer;
        animator.SetFloat("firerate", firerate);
    }

    // Update is called once per frame
    void Update()
    {
        
        //regarde le joueur
        if (lookplayer == true)
        {
            //oriante le gyro (ca fait partie des choses pas propre
            var lookPos = lb.cibleplayer.transform.position - gyroscope.transform.position;

            var rotation = Quaternion.LookRotation(lookPos);
            gyroscope.transform.rotation = Quaternion.Slerp(gyroscope.transform.rotation, rotation, Time.deltaTime * précision);

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

        // BUG PRESENT AU NIVEAU DU "INSIGHT" QUI S ACTIVE ET CE DESACTIVE DE FACON ALEATOIRE ,VIENS SURMENT DE L APPELLE DU RAYCAST
        RaycastHit hit;
        Debug.DrawRay(Cannondirection.position, -Cannondirection.right * attackRange, Color.white);
        if (Physics.Raycast(Cannondirection.position, -Cannondirection.right, out hit, attackRange))
        {
           
   
            if (hit.collider.gameObject.tag == "Player")
            {
                insight = true;
              
                Debug.DrawRay(Cannondirection.position, -Cannondirection.right * attackRange, Color.red);
            }
            else 
            {
                insight = false;
               
               
            }
        }

        if (insight)
        {
            Firecheck();
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

    public void Firecheck()
    {
        if (canshoot)
        {
            animator.SetTrigger("fire");
        }
    }
    public void Fire()
    {
        GameObject go = Instantiate(projectile, cannontip.transform.position,Cannondirection.rotation );
    }
    public void Die()
    {
        Destroy(this.gameObject);
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
