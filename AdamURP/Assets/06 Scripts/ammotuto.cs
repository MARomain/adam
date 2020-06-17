using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammotuto : MonoBehaviour
{
    public Player player;
    public GameObject ammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ammoleft <= 0)
        {
            ammo.gameObject.SetActive(true);
        }
        else
        {
            ammo.gameObject.SetActive(false);
        }
    }
}
