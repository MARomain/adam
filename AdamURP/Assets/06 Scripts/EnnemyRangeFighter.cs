using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyRangeFighter : MonoBehaviour
{
    public bool canAttack = true;
    public bool canMove = true;
    public bool canBekilled = true;

    public Animator animator;

    public int weapontype = 1;
    public float attackRange = 3f;
    public float knockbackforce = 5;
    public float livegivedback = 10;





    //tire
    public float précision = 3;
    public bool insight = false;
    public GameObject projectile;
    public Transform Cannondirection;
    public GameObject cannontip;
    public bool canshoot = true;
    public float firerate = 1;

    //faceplayer
    public bool lookplayer = true;
    public GameObject gyroscope; //ca c'est du debug bien sale ,moyen opti ,qui est pas tres beau,qui sert pour le calcule de rotation 
    public bool faceright = true;
    public GameObject bassin;
    public Vector3 originalrotation;
    private Vector3 calculaterotation;
    public Rigidbody rb;
    public Library lb;

    // Start is called before the first frame update
    void Start()
    {
        //  originalrotation = new Vector3(bassin.transform.rotation.eulerAngles.x, bassin.transform.rotation.eulerAngles.y, bassin.transform.rotation.eulerAngles.z);
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


            //calculaterotation = new Vector3(gyroscope.transform.rotation.eulerAngles.x + originalrotation.x, originalrotation.y, originalrotation.z);

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
                Debug.DrawRay(Cannondirection.position, -Cannondirection.right * attackRange, Color.white);

            }
        }

        if (insight)
        {
            Firecheck();
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
  
}
