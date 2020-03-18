using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Player player;
    public Text weaponmaxammo;
    public Text ammoleft;
    public GameObject minature;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.weapontype == 1)&&(player.ammoleft>=0))
        {
           minature.SetActive(true);
            ammoleft.text = player.ammoleft.ToString();
            weaponmaxammo.text = player.weaponmaxammo.ToString();
        }
        else
        {
             minature.SetActive(false);
            ammoleft.text = ("0");
            weaponmaxammo.text = ("0");
        }
       
    }
}
