using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float rof;
    private bool canShoot = true;

    public float movementSpeed;

    public float jumpForce = 20f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool jumpRequest;
    public float distToGround;
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
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject go = Instantiate(bulletPrefab, canonTransform);
        go.transform.SetParent(null);
        Destroy(go, 6f);
        yield return new WaitForSeconds(rof);
        canShoot = true;
    }

    //problème "GetButtonDown" pour le mouvement
    void Movement()
    {
        movementInput = controls.Player.Movement.ReadValue<Vector2>();
        //transform.Translate(new Vector3(movementInput.x, 0f, 0f) * movementSpeed * Time.deltaTime);

        rb.velocity = new Vector2(movementInput.x * movementSpeed * Time.deltaTime, rb.velocity.y);
    }

    void Aim()
    {
        //neutre
        if (movementInput.x == 0f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(canonTransform.localPosition.x, canonTransform.localPosition.y, 0f);
        }

        //droite
        if (movementInput.x >= 0.5f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(0.75f, 0.3f, 0f);
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
            canonTransform.localPosition = new Vector3(-0.75f, 0.3f, 0f);
        }

        //gauche
        if (movementInput.x < 0f && movementInput.y == 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 180);
            canonTransform.localPosition = new Vector3(-0.75f, 0.3f, 0f);
        }

        //bas gauche
        if (movementInput.x < 0f && movementInput.y < 0f)
        {
            canonTransform.eulerAngles = new Vector3(0f, 0f, 225f);
            canonTransform.localPosition = new Vector3(-0.75f, -0.3f, 0f);
        }

        //bas
        if (movementInput.x == 0f && movementInput.y < 0f)
        {
            canonTransform.eulerAngles = Vector3.zero;
            canonTransform.localPosition = new Vector3(canonTransform.localPosition.x, -0.3f, 0f);
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
            //rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.velocity = Vector3.up * jumpForce;

        }

        if(IsGrounded())
        {
            //jumpCount = 0;
        }

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

private void Update()
    {

        //ALERTE DESTRUCTION DE TOUTE OPTIMITATION -ALERTE DESTRUCTION DE TOUTE OPTIMITATION -ALERTE DESTRUCTION DE TOUTE OPTIMITATION -
        //a but de demo rapide je rajoute un input.getkeydown
       if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;

        }
        //ALERTE DESTRUCTION DE TOUTE OPTIMITATION -ALERTE DESTRUCTION DE TOUTE OPTIMITATION -ALERTE DESTRUCTION DE TOUTE OPTIMITATION -

        Aim();

        if (controls.Player.Shoot.ReadValue<float>() >= 1f && canShoot == true)
        {
            StopCoroutine("Shoot");
            StartCoroutine("Shoot");
        }

        Debug.Log(IsGrounded());
        Debug.Log(distToGround);
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        IsGrounded();
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

}
