using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Player player;
    public Text weaponmaxammo;
    public Text ammoleft;
    public GameObject minature;
    public Animator animator;
    public Library lb;
    public Text textlife;
    public Slider sliderdash;
    public GameObject dahsloadedimage;
    public GameObject deathscreen;
    public Text firetypeinfo;
    public Image weaponicon;
    public Sprite weapon1M;
    public Sprite weapon2M;
    public Sprite weapon3M;
    public Sprite weapon4M;

    public Image newweaponicon;


    public AudioSource audioSource;
    public AudioClip healclip;
    // Start is called before the first frame update
    public void Awake()
    {
        player = FindObjectOfType<Player>();
         lb = FindObjectOfType<Library>();
    }
    void Start()
    {

        sliderdash.value = player.dashtimer;
        sliderdash.maxValue = player.dashcooldown;
    }

    // Update is called once per frame
    public void HpupdateUI()
    {
        textlife.text = player.health.ToString();
    }
    void Update()
    {
        if (player.ammoleft > 0)
        {
            switch (player.weapontype)
            {
                case 1:
                    firetypeinfo.text = lb.weapon1firetype;
                    weaponicon.sprite = weapon1M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon1munitions.ToString();
                    break;
                case 2:
                    firetypeinfo.text = lb.weapon2firetype;
                    weaponicon.sprite = weapon2M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon2munitions.ToString();
                    break;
                case 3:
                    firetypeinfo.text = lb.weapon3firetype;
                    weaponicon.sprite = weapon3M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon3munitions.ToString();
                    break;
                case 4:
                    firetypeinfo.text = lb.weapon4firetype;
                    weaponicon.sprite = weapon4M;
                    minature.SetActive(true);
                    ammoleft.text = player.ammoleft.ToString();
                    weaponmaxammo.text = lb.weapon4munitions.ToString();
                    break;
            }
        }
        else
        {
            minature.SetActive(false);
            ammoleft.text = ("0");
            weaponmaxammo.text = ("0");
        }




        sliderdash.value = player.dashtimer;
        if (player.dashtimer <=0)
        {
            dahsloadedimage.SetActive(true);
        }
        else
        {
            dahsloadedimage.SetActive(false);
        }

    }
    public void Reloadscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Menuscene()
    {
        SceneManager.LoadScene("Main_menu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void Healsound()
    {
        audioSource.PlayOneShot(healclip);
    }
    public void Playnewweaponanim()
    {

        animator.SetTrigger("newweapon");

    }

}

