﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Library lb;
    public musicmanager musicmanager;
    public UImanager uImanager;
    public float rof;


    public int weapontype =0;
    public float movementSpeed;
    public float maxMovementSpeed;
    public float health = 100f;
    public float maxhealth = 100f;
    public float degatcoup = 1;
    private Ennemy ennemyreached;


    public bool dash=false;
    public float dashcooldown = 1;
    public float dashtimer; //go passer private apres le test


    public float dodgeforce = 10f;
    public float jumpForce = 20f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool jumpRequest;
    public bool faceright = true;
    public float distToGround;
    public Animator animator;
    public float porteecoup=3f;
    public bool invulnaribilite = false;
    public bool disabledinput = false;
    public LayerMask groundLayer;
    public bool isgrounded = true;
    private bool punchconnect = false;
    //WEAPON 1 STAT C EST VRAIMENT CACA LE CODE
    public int weaponmaxammo = 10;
    public int ammoleft = 0;
    public bool shootinputpressed = false;

    public Transform canonTransform;

    public Rigidbody rb;
    public GameObject weapon1;
    public GameObject weapon2;
    public bool weapon2charging = false;
    public GameObject weapon3;
    public InputMaster controls;

    Vector2 movementInput;
    public CinemachineVirtualCamera CVC;
    private CinemachineBasicMultiChannelPerlin CBMP;

    public AudioSource audioSource;
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public AudioClip fallClip;

    public AudioClip InvalidClip;
    public AudioClip punchMissClip;
    public AudioClip punchhitClip;
    public AudioClip deathClip;

    public AudioClip Weapon2charge;
    public AudioClip Weapon1clip;
    public AudioClip Weapon2clip;
    public AudioClip Weapon3clip;
    // Documentation
    // Pour des actions qui ne se lance qu'une fois, setup un callback qui lance une fonction associé à l'action est good
    // Pour une action de type continue (il faut avoir l'info à toutes les frames) on la choppe avec .ReadValue<>()





    private void Awake()
    {
        CBMP = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        controls = new InputMaster();

        if (!disabledinput)
        {
            controls.Player.Jump.performed += ctx => JumpRequest();
            controls.Player.attack.performed += ctx => Attack();
            controls.Player.Feu.performed += ctx => Shoot();
            controls.Player.Action.performed += ctx => Action();
            controls.Player.Feu.canceled += ctx => Shootleave();
        }

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        uImanager.HpupdateUI();


    }

    private void Update()
    {
        if (dashtimer > 0)
        {
            dashtimer = dashtimer - Time.deltaTime;
        }
   
        Aim();
    }
   
    private void FixedUpdate()
    {
        Movement();
            Jump(); 
      
        IsGrounded();
    }
    void Movement()
    {
        if (!disabledinput)
        {
            if (movementInput.x==0)
            {
                animator.SetBool("run", false);
            }
            else
            {
                animator.SetBool("run", true);
            }

      
            movementInput = controls.Player.Movement.ReadValue<Vector2>();


            //player movement
            rb.AddForce(new Vector2(movementInput.x * movementSpeed * Time.deltaTime, rb.velocity.y));

            if (rb.velocity.x > maxMovementSpeed)
            {
                rb.velocity = new Vector2(maxMovementSpeed, rb.velocity.y);
            }

            if (rb.velocity.x < -maxMovementSpeed)
            {
                rb.velocity = new Vector2(-maxMovementSpeed, rb.velocity.y);
            }



        }
    }


    void Aim()
    {
        //netral
        if (movementInput.x == 0f && movementInput.y == 0f)
        {
            if (faceright)
            {
                canonTransform.eulerAngles = Vector3.zero;
                canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
                animator.SetFloat("fireaim", 0);
            }
            else
            {
                canonTransform.eulerAngles = new Vector3(0f, 0f, 180);
                canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
                animator.SetFloat("fireaim", 0);
            }

        }

        //droite
        if (movementInput.x >= 0.5f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", 0);

            if (!faceright)
            {
                this.transform.Rotate(0, -180,0 );
              
          
            }
            faceright = true;
        }

        //haut droit
        if (movementInput.x >= 0.5f && movementInput.y >= 0.5f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 45f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", 1);
            if (!faceright)
            {
                this.transform.Rotate(0, -180, 0);


            }
            faceright = true;

        }

        //haut
        if (movementInput.x == 0f && movementInput.y >= 0.5f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 90f);
            canonTransform.localPosition = new Vector3(0f, 1.6f, 0f);
            animator.SetFloat("fireaim", 2);
        }

        //haut gauche 
        if (movementInput.x < 0f && movementInput.y >= 0.5f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 135f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", 1);
            if (faceright)
            {
                this.transform.Rotate(0, 180, 0);

            }
            faceright = false;
        }

        //gauche
        if (movementInput.x < 0f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 180);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", 0);
            if (faceright)
            {
                this.transform.Rotate(0, 180, 0);
         
            }
            faceright = false;
        }

        //bas gauche
        if (movementInput.x < 0f && movementInput.y < 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 225f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", -1);
            if (faceright)
            {
                this.transform.Rotate(0, 180, 0);

            }
            faceright = false;
        }

        //bas
        if (movementInput.x == 0f && movementInput.y < 0f)
        {
            //canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(canonTransform.localPosition.x, 0.3f, 0f);
            animator.SetFloat("fireaim", -2);
        }

        //bas droit
        if (movementInput.x >= 0.5f && movementInput.y < 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 315f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
            animator.SetFloat("fireaim", -1);
            if (!faceright)
            {
                this.transform.Rotate(0, -180, 0);


            }
            faceright = true;
        }

    }

    void Jump()
    {
        if (jumpRequest)
        {
            jumpRequest = false;
            animator.SetTrigger("jump");
          
            //rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.velocity = Vector3.up * jumpForce;

            audioSource.clip = jumpClip;
            audioSource.Play();
        }

        if(IsGrounded())
        {
            isgrounded = true;
            animator.SetBool("grounded", true);
        
            //jumpCount = 0;
        }
        else
        {
            isgrounded = false;
            animator.SetBool("grounded", false);
        }

        //il faut conserver la structure avec "jumprequest" pour permettre à Jump d'être éxécuter dans l'update
        //et donc que ce code puisse être lu dans l'update
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && controls.Player.Shoot.ReadValue<float>() < 1)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    private void Action()
    {
       
       
        if (dashtimer <= 0)
        {
            if ((weapon2charging)||(lb.weapon2charged))
            {
                InvalidSound();
            }
            else
            {
                animator.SetTrigger("dodge");
                dashtimer = dashcooldown;
            }

        }
        Debug.Log("dodgeinput");
    }
    bool IsGrounded()  
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, groundLayer);
    }

    void JumpRequest()
    {
        if(IsGrounded())
        jumpRequest = true;
    }

    public void TakeDamage(float amount)
    {
        if (!invulnaribilite)
        {
            if (dash == false) { 
                health -= amount;
          
                animator.SetTrigger("hit");
                uImanager.HpupdateUI();

            if (health <= 0f)
            {
                Die();
            }
            }
            else
            {
            //dodge effect
                Debug.Log("dodged bullet");
            }
    }

    }

    void Die()
    {
        disabledinput = true;
        animator.SetTrigger("die");
        Time.timeScale = 0.1f;
        uImanager.deathscreen.SetActive(true);
        musicmanager.Dieeffectonmusic();
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distToGround);

    }
    public void Attack()
    {
        if (!disabledinput)
        {
           
            RaycastHit hit;
            if (faceright)
            {
                if (Physics.Raycast(transform.position, Vector3.right, out hit))
                    if (hit.collider != null)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red); //dessine la portée
                        if (hit.distance < porteecoup)//check si c'est pas trop loin
                        {
                            if (hit.collider.gameObject.GetComponentInParent<Ennemy>() != null) //check si il a touché un ennemie
                            {
                                ennemyreached = hit.collider.gameObject.GetComponentInParent<Ennemy>();
                                if (ennemyreached.opennedtoglorykill)//si il etait stun
                                {
                                   ennemyreached.Glorykill();//glorykill;
                                    animator.SetTrigger("glorykill");
                                    Changeweaponmodel();
                                }
                                else
                                {
                                    punchconnect = true;
                                    animator.SetTrigger("kick");
                                 
                                }
                            }
                        }
                        else
                        {
                            animator.SetTrigger("kick");
                        }
                    }

                animator.SetTrigger("kick");
           

            }
            else
            {
                if (Physics.Raycast(transform.position, -Vector3.right, out hit))
                    if (hit.collider != null)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red); //dessine la portée
                        if (hit.distance < porteecoup)//check si c'est pas trop loin
                        {
                            if (hit.collider.gameObject.GetComponentInParent<Ennemy>() != null) //check si il a touché un ennemie
                            {
                                ennemyreached = hit.collider.gameObject.GetComponentInParent<Ennemy>();
                                if (ennemyreached.opennedtoglorykill)//si il etait stun
                                {
                                    punchconnect = true;
                                    ennemyreached.Glorykill();//glorykill;;
                                    animator.SetTrigger("glorykill");
                                    Changeweaponmodel();
                                 
                                }
                                else
                                {
                                    punchconnect = true;
                                    animator.SetTrigger("kick");
                                
                                }
                            }

                        }
                        else
                        {
                            animator.SetTrigger("kick");
                         
                        }
                    }
                
                animator.SetTrigger("kick");
          

            }
        }


  
    }



    public void Attackdegat()
    {
        ennemyreached.TakeDamage(degatcoup);
        ennemyreached = null;
    }

    public void Heal(float healamount)
    {
        Debug.Log("player was healed of " + healamount + " pv");
        Debug.Log(health + " + " + healamount);
        health = health + healamount;
        uImanager.animator.SetTrigger("heal");
        print("heal");
        uImanager.Healsound();



        if (health > maxhealth)
        {
            health = maxhealth;
        }
        uImanager.HpupdateUI();
    }
    public void Disableplayerinput()
    {

        disabledinput = true;

    }
    public void Punchdamage()
    {
 
    }
    public void Enableplayerinput()
    {

        disabledinput = false;

    }
    public void InvincibleON()
    {
      
        invulnaribilite = true;
    }
    public void InvincibleOFF()
    {

        invulnaribilite = false;
    }
    public void Startdodge()
    {
        dash = true;
    }
    public void Enddodge()
    {
        dash = false;
    }
    public void Bruitdepas()
    {
        //sert dans l'animation (run)
        audioSource.clip = footstepClip;
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
        audioSource.volume = 1f;
        audioSource.pitch = 1f;
    }
    //remplace l'enumerator 
    public void Shoot()
    {
        animator.SetBool("shootinputpressed", true);
        shootinputpressed = true;
 
        if (weapontype != 0)
        {
                animator.SetTrigger("shoot");
        }
    }
    public void Ammocheck()
    {
        if (ammoleft == 0)
        {
            weapontype = 0;
            Changeweaponmodel();
        }
    }
    public void Fire()
    {
        if (!disabledinput)
        {
            switch (weapontype)
            {
                case 0:

                    break;
                case 1:

                    GameObject go = Instantiate(lb.bulletplayertype1, canonTransform);//CHANGER LE PREFAB POUR L ARME

                    //sound
                    audioSource.PlayOneShot(Weapon1clip);

                    go.transform.SetParent(null);
                    Destroy(go,4f);
                    ammoleft = ammoleft - 1;
                    Ammocheck();

                    break;
                case 2:
                   

                    break;
                case 3:
                    GameObject go3 = Instantiate(lb.bulletplayertype3, canonTransform);//CHANGER LE PREFAB POUR L ARME

                    //sound
                    audioSource.PlayOneShot(Weapon3clip);

                    go3.transform.SetParent(null);
                    Destroy(go3, 4f);
                    ammoleft = ammoleft - 1;
                    Ammocheck();

                    break;
            }

        }
    }
    private void Changeweaponmodel()
    {
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
        switch (weapontype)
        {
            case 0:

                animator.SetFloat("weapontype", 0);

                break;
            case 1:
                weapon1.SetActive(true);
                animator.SetFloat("weapontype",1);
                break;
            case 2:
                weapon2.SetActive(true);
                animator.SetFloat("weapontype",2);
                break;
            case 3:
                weapon3.SetActive(true);
                animator.SetFloat("weapontype", 3);
                break;
        }
        Debug.Log("change weapon check");
  
     
    }


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    private void Pushplayer()
    {
        if (faceright)
        {
            rb.AddForce(Vector3.right * dodgeforce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * dodgeforce, ForceMode.Impulse);
        }
    }
    public void Weaponchargedtrue()
    {
        lb.weapon2charged = true;
        weapon2charging = false;
    }
    public void Weapon2chargingTrue()
    {
        weapon2charging = true;
    }

    public void Shootleave()
    {
        animator.SetBool("shootinputpressed", false);
        shootinputpressed = false;
  
        if ((weapontype == 2)&&(lb.weapon2charged==true))
        {
            GameObject go = Instantiate(lb.bulletplayertype2, canonTransform);//CHANGER LE PREFAB POUR L ARME

            Debug.Log("snipershoot");
            audioSource.PlayOneShot(Weapon2clip);

            go.transform.SetParent(null);
            Destroy(go, 4f);
            ammoleft = ammoleft - 1;
            lb.weapon2charged = false;
            lb.weapon2chargevalue = 0;
            Ammocheck();
        }
     else if((weapontype == 2) && (weapon2charging == true))
        {
            Stopsound();
        }
        weapon2charging = false;
    }
    public void ShakeDamage()
    {
        CBMP.m_AmplitudeGain = 3;
        CBMP.m_FrequencyGain = 3;
    }
    public void ShakeHeavyfire()
    {
        CBMP.m_AmplitudeGain = 1.8f;
        CBMP.m_FrequencyGain = 1;
    }
    public void Shakefire()
    {
        CBMP.m_AmplitudeGain = 1.4f;
        CBMP.m_FrequencyGain = 1;
    }
    public void ShakeKick()
    {
        CBMP.m_AmplitudeGain = 2;
        CBMP.m_FrequencyGain = 1;
    }
    public void ShakeStop()
    {
        CBMP.m_AmplitudeGain = 0;
        CBMP.m_FrequencyGain = 0;
    }
    public void Punchsound()
    {
        if (punchconnect)
        {
            audioSource.PlayOneShot(punchhitClip);
            punchconnect = false;
        }
        else {
            audioSource.PlayOneShot(punchMissClip);
        }
    }
    public void Chargesound()
    {
        audioSource.PlayOneShot(Weapon2charge);
    }
    public void InvalidSound()
    {
        audioSource.PlayOneShot(InvalidClip);
    }
    public void Stopsound()
    {
        audioSource.Stop();
    }
}
