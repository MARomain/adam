using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_lazerfighter : MonoBehaviour
{
    public bool canAim = true;
    public bool canAttack = true;
    public bool canMove = true;
    public bool canBekilled = true;

    public Animator animator;

    public int weapontype = 1;
    public float attackRange = 3f;
    public float knockbackforce = 5;
    public float livegivedback = 10;

    public LineRenderer lazer;

 


    //tire
    public bool startedtoaim=false;
    public float précision = 3;
    public bool insight = false;
    public GameObject projectile;
    public Transform Cannondirection;
    public GameObject cannontip;
    public bool canshoot = true;
    public float firerate = 1;
    [SerializeField]
    public LayerMask ingnorelayer;



    //faceplayer
    public bool lookplayer = true;
    public GameObject gyroscope; //ca c'est du debug bien sale ,moyen opti ,qui est pas tres beau,qui sert pour le calcule de rotation 
    public bool faceright = true;
    public GameObject bassin;
    public Vector3 originalrotation;
    private Vector3 calculaterotation;
    public Rigidbody rb;
    public Library lb;

    public AudioClip LoadClip;
    public AudioClip FireClip;
    public AudioClip ClingClip;
    public AudioSource audioSource;


    void Start()
    {
        ingnorelayer = ~ingnorelayer;
        rb = GetComponent<Rigidbody>();
        lb = FindObjectOfType<Library>();

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


         

            if (lb.cibleplayer.transform.position.x > this.transform.position.x)
            {
                if (faceright)
                {
                    this.transform.Rotate(0, 180, 0);
                    faceright = false;
                }

                calculaterotation = new Vector3(gyroscope.transform.rotation.eulerAngles.x + originalrotation.x, -originalrotation.y, originalrotation.z);
            }
            else if (lb.cibleplayer.transform.position.x < this.transform.position.x)
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



        RaycastHit hit;
      

        if (Physics.Raycast(Cannondirection.position, -Cannondirection.right, out hit, attackRange, ingnorelayer))
        {
           
            lazer.SetPosition(0, Cannondirection.position);
            lazer.SetPosition(1, hit.point);
     


            if (hit.collider.gameObject.tag == "Player")
            {
                insight = true;

                Debug.DrawRay(Cannondirection.position, -Cannondirection.right * attackRange, Color.red);

            }
            else
            {
                insight = false;
                Debug.DrawRay(Cannondirection.position, -Cannondirection.right * attackRange, Color.white);

            }
        }

        if (insight)
        {
            animator.SetTrigger("fire");
            startedtoaim = true;
        }
        else
        {
            animator.SetTrigger("stopfire");
            audioSource.Stop();
            if (startedtoaim==true)
            {
                animator.SetTrigger("stopfire");
                startedtoaim = false;
                audioSource.Stop();
            }

        }


    }

    public void Fire()
    {
        //TO DO la balle part toujours en direction du joueur
        //PROBLEME plus ou moins volontaire ,la balle part d'en face du cannon peut importe si il lui fait face:


        GameObject go = Instantiate(projectile, Cannondirection.transform.position, Cannondirection.rotation);
    }
    public void AimLeftRight()
    {
        if (lb.cibleplayer.transform.position.x - this.transform.position.x <= 0)
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
    public void Playchargesound()
    {
        audioSource.PlayOneShot(LoadClip);
    }
    public void PlayFireSound()
    {
        audioSource.PlayOneShot(FireClip);
    }
    public void PlayClingSound()
    {
        audioSource.PlayOneShot(ClingClip);
    }

}

