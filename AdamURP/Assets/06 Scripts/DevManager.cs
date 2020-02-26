using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevManager : MonoBehaviour
{
    public InputMaster controls;
    public GameObject EnnemyToSpawn;
    // Start is called before the first frame update

    private void Awake()
    {
        controls = new InputMaster();


        controls.Dev.SpawnEnnemyatlocation.performed += ctx => SpawnEnnemyAtLocation();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnnemyAtLocation()
    {
        Debug.Log("spawn ennemy");
        Vector3 point = new Vector3();
        point = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, -Camera.main.transform.position.z));
        Instantiate(EnnemyToSpawn, point, Quaternion.identity);
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
