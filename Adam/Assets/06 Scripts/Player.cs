using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    public float movementSpeed;

    public Transform canonTransform;


    public InputMaster controls;
    public InputAction shoot;

    Vector2 movementInput;

    private void Awake()
    {
        //On instancie InputMaster
        controls = new InputMaster();

        //callback qui va alimenté la variable movementInput quand l'action est performed
        //controls.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

        //callback qui éxécute Shoot() quand l'action shoot est performed
        controls.Player.Shoot.performed += ctx => Shoot();

        //callback qui éxécute Movement() quand l'action Movement est performed et récupère la valeur de Movement et la passe dans le paramètre de la fonction Movement()
        controls.Player.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Movement(Vector2.zero);
    }

    void Shoot()
    {
        GameObject temp = Instantiate(bullet, canonTransform);
        temp.GetComponent<Rigidbody>().velocity = canonTransform.right * bulletSpeed;
        Destroy(temp, 6f);
    }

    //problème "GetButtonDown" pour le mouvement
    void Movement(Vector2 direction)
    {
        movementInput = controls.Player.Movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(movementInput.x, 0f, 0f) * movementSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Movement(movementInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
