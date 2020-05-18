using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOnTheGround : MonoBehaviour
{

    public int weapontype = 1;
    public int ammo = 10;
    public Animator animator;
    public GameObject marqueur;
    private float distancefromplayer;
    public float distancetoshow;


    public bool startwithfullammo = true;
    private Library lb;

    // Start is called before the first frame update
    void Start()
    {
        if (startwithfullammo)
        {
            lb = FindObjectOfType<Library>();
            if (weapontype == 1)
            {
                ammo = lb.weapon1munitions;
            }
            if (weapontype == 2)
            {
                ammo = lb.weapon2munitions;
            }
            if (weapontype == 3)
            {
                ammo = lb.weapon3munitions;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        distancefromplayer = Vector3.Distance(lb.cibleplayer.transform.position, transform.position);
        if (distancefromplayer < distancetoshow)
        {
            marqueur.SetActive(true);
        }
        else
        {
            marqueur.SetActive(false);
        }
    }
}
