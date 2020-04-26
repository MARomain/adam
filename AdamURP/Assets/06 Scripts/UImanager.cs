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
    public Library lb;

    public Image weaponicon;
    public Sprite weapon1M;
    public Sprite weapon2M;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ammoleft > 0)
        {
            switch (player.weapontype)
            {
                case 1:
                    weaponicon.sprite = weapon1M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon1munitions.ToString();
                    break;
                case 2:
                    weaponicon.sprite = weapon2M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon2munitions.ToString();
                    break;
            }
        }
        else
        {
            minature.SetActive(false);
            ammoleft.text = ("0");
            weaponmaxammo.text = ("0");
        }

       
    }
}
