using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputMaster controls;



    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += ctx => Shoot();
    }


    void Shoot()
    {
        Debug.Log("we shot the sheriff");
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    //sss

}
