using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class debugcheat : MonoBehaviour
{
    public Camera camera;
    public Player player;
    public GameObject Chargeur;
    public GameObject Range;
    public GameObject Sniper;
    public GameObject shotgun;
    public GameObject bloc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && (Input.GetKey(KeyCode.U)))
        {
            SpawnCharger();
            Debug.Log("inputed spawn");
        }
    }
    public void Fullammo()
    {
        player.ammoleft = player.weaponmaxammo;
    }
    public void SpawnCharger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Vector3 pose = hit.point;
            GameObject Chargeurclone = Instantiate(Chargeur, new Vector3(pose.x,pose.y,0f), new Quaternion());
            Debug.Log("spawncharger");
        }


    }


}
