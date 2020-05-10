using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startmusicondeath : MonoBehaviour
{
    public musicmanager musicmanager;
    public Ennemy ennemy;
    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((ennemy.health == 0)&&(active))
        {
            musicmanager.Startmusic();
            active = false; 
        }
    }
}
