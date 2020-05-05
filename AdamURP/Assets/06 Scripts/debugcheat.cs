using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class debugcheat : MonoBehaviour
{
    public Camera cammain;
    public Camera cambonus;
    public bool bonuscam = false;
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
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Bonuscam();
        }
    }
    public void Fullammo()
    {
        player.ammoleft = player.weaponmaxammo;
    }
    public void SpawnCharger()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Instantiate(Chargeur, new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0), Quaternion.identity);
        Debug.Log("spawncharger");
    }
    public void Bonuscam()
    {
        if (!bonuscam)
        {
            cammain.targetDisplay =3;
            cambonus.targetDisplay = 0;
            bonuscam = true;
            Debug.Log("camnbonus");
        }
        else
        {
            cammain.targetDisplay = 0;
            cambonus.targetDisplay = 3;
            bonuscam = false;
            Debug.Log("camnormal");
        }
    }


}
