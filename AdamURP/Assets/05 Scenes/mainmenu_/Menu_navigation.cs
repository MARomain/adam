using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_navigation : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip validClip;
    public AudioClip InvalidClip;

    public GameObject Homescreen;
    public GameObject levelselect;
    public GameObject Level1menu;
    public Animator animator;


    public void Startbutton()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        levelselect.SetActive(true);
    }
    public void LevelSelectmenu()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        levelselect.SetActive(true);
        animator.SetBool("level1", false);
    }

    public void Level1Screen()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        animator.SetBool("level1", true);
        Level1menu.SetActive(true);
    }
    public void Notreadybutton()
    {
        audioSource.PlayOneShot(InvalidClip);
    }
    public void Level1start()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
        
    }


    public void Clearmenu()
    {
        Homescreen.SetActive(false);
        levelselect.SetActive(false);
        Level1menu.SetActive(false);
    }
}

