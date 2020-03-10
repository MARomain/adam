using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float rof;
    private bool canShoot = true;

    public int weapontype =1;
    public float movementSpeed;
    public float health = 100f;
    public float maxhealth = 100f;
    public Text healthtext;
    public float degatcoup = 1;
    private bool canattack = true;
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


    public Transform canonTransform;

    public Rigidbody rb;

    public InputMaster controls;

    Vector2 movementInput;

    // Documentation
    // Pour des actions qui ne se lance qu'une fois, setup un callback qui lance une fonction associé à l'action est good
    // Pour une action de type continue (il faut avoir l'info à toutes les frames) on la choppe avec .ReadValue<>()





    private void Awake()
    {

        //On instancie InputMaster
        controls = new InputMaster();

        // ** NE FONCTIONNE PAS ** 
        // Ne permet pas de laisser le bouton appuyer pour tirer en continue
        // Quand un bouton est laissé enfoncé sa valeur ne change pas, le callback n'est donc pas update
        //callback qui éxécute Shoot() quand l'action shoot est performed
        //controls.Player.Shoot.performed += ctx => Shoot();
        //controls.Player.Shoot.performed += ctx => Shoot();


        // ** NE FONCTIONNE PAS ** 
        // InputAction.performed n'est appelé que quand une valeur change. 
        // SI le joueur maintient la touche de déplacement (ou le stick) la valeur ne change pas et donc le callback n'est pas émis 
        // On perd donc l'information et on ne peut pas avoir les données sur les inputs continuellement
        // Utilisation de controls.Player.Movement.ReadValue<Vector2>(); à la place

        //callback qui éxécute Movement() quand l'action Movement est performed et récupère la valeur de Movement et la passe dans le paramètre de la fonction Movement()
        //controls.Player.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        //.Player.Movement.canceled += ctx => Movement(Vector2.zero);


        controls.Player.Jump.performed += ctx => JumpRequest();
        controls.Player.attack.performed += ctx => Attack();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        healthtext = GameObject.Find("PlayerHpText").GetComponent<Text>();
    }

    private void Update()
    {
        Movement();

        Aim();

        if (controls.Player.Shoot.ReadValue<float>() >= 1f && canShoot == true)
        {
            StopCoroutine("Shoot");
            StartCoroutine("Shoot");
        }

    }
    
    private void FixedUpdate()
    {
        if (!disabledinput) { Jump(); }
      
        IsGrounded();
    }

    IEnumerator Shoot()
    {
        if (!disabledinput)
        {
            canShoot = false;
            GameObject go = Instantiate(bulletPrefab, canonTransform);
            go.transform.SetParent(null);
            Destroy(go, 6f);
            yield return new WaitForSeconds(rof);
            canShoot = true;
        }

    }

    //problème "GetButtonDown" pour le mouvement
    void Movement()
    {
        if (!disabledinput)
        {
            movementInput = controls.Player.Movement.ReadValue<Vector2>();
            //transform.Translate(new Vector3(movementInput.x, 0f, 0f) * movementSpeed * Time.deltaTime);

            rb.velocity = new Vector2(movementInput.x * movementSpeed * Time.deltaTime, rb.velocity.y);
        }
   
    }

    void Aim()
    {
        //neutre
        if (movementInput.x == 0f && movementInput.y == 0f)
        {
            //canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(canonTransform.localPosition.x, canonTransform.localPosition.y, 0f);
        }

        //droite
        if (movementInput.x >= 0.5f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
   
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
        
        }

        //haut
        if (movementInput.x == 0f && movementInput.y >= 0.5f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 90f);
            canonTransform.localPosition = new Vector3(0f, 1.2f, 0f);
        }

        //haut gauche 
        if (movementInput.x < 0f && movementInput.y >= 0.5f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 135f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
        }

        //gauche
        if (movementInput.x < 0f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 180);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
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
        }

        //bas
        if (movementInput.x == 0f && movementInput.y < 0f)
        {
            //canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(canonTransform.localPosition.x, 0.3f, 0f);
        }

        //bas droit
        if (movementInput.x >= 0.5f && movementInput.y < 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 315f);
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
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

        }

        if(IsGrounded())
        {
            animator.SetBool("grounded", true);
        
            //jumpCount = 0;
        }
        else
        {
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
            health -= amount;
            healthtext.text = "Player hp : " + health.ToString();

            if (health <= 0f)
            {
                Die();
            }
        }

    }

    void Die()
    {
        //death particles
        //death sound effects
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
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
                                Ennemy ennemy = hit.collider.gameObject.GetComponentInParent<Ennemy>();
                                ennemy.TakeDamage(degatcoup);
                                if (ennemy.opennedtoglorykill)//si il etait stun
                                {
                                    ennemy.Glorykill();//glorykill;
                                    Debug.Log("glorykill");
                                    animator.SetTrigger("glorykill");
                                }
                                else
                                {
                                    animator.SetTrigger("kick"); Debug.Log("kick");
                                    ennemy.TakeDamage(degatcoup); //retire de la vie
                                }
                            }
                        }
                        else
                        {
                            animator.SetTrigger("kick");
                            Debug.Log("empty kick");
                        }
                    }
                animator.SetTrigger("kick");
                Debug.Log("empty dsd");

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
                                Ennemy ennemy = hit.collider.gameObject.GetComponentInParent<Ennemy>();
                                ennemy.TakeDamage(degatcoup);
                                if (ennemy.opennedtoglorykill)//si il etait stun
                                {
                                    ennemy.Glorykill();//glorykill;
                                    Debug.Log("glorykill");
                                    animator.SetTrigger("glorykill");
                                }
                                else
                                {
                                    animator.SetTrigger("kick"); Debug.Log("kick");
                                    ennemy.TakeDamage(degatcoup); //retire de la vie
                                }
                            }

                        }
                        else
                        {
                            animator.SetTrigger("kick");
                            Debug.Log("empty kick");
                        }
                    }
                animator.SetTrigger("kick");


            }
        }
       


    }
    public void Reattack()
    {
        canattack = true;
    }

  
    public void Attackdegat()
    {
        //tu m'a bien dit que je fesait ce que je voulais mais je devais commenter 
        //TO DO feedback attack
        //Debug.Log("attack cac");
    }

    public void Heal(float healamount)
    {
        Debug.Log("player was healed of " + healamount + " pv");
        Debug.Log(health + " + " + healamount);
        health = health + healamount;
       
 
        if(health > maxhealth)
        {
            health = maxhealth;
        }
        healthtext.text = "Player hp : " + health.ToString();
    }
    
}
