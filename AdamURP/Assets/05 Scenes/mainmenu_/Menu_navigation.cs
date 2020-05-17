using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_navigation : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip validClip;
    public AudioClip InvalidClip;

    public GameObject playButton;
    public GameObject levelselect;
    public GameObject Level1menu;
    public GameObject Leveltutomenu;
    public Animator animator;

    public GameObject startTutoButton;
    public GameObject startLevel1Button;
    public GameObject levelSelectLevel1Button;

    EventSystem eventSystem;

    public void OnEnable()
    {
        eventSystem = EventSystem.current;
    }


    public void LevelSelectmenu()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        levelselect.SetActive(true);
        animator.SetBool("level1", false);
        eventSystem.SetSelectedGameObject(levelSelectLevel1Button);
    }

    public void Startbutton()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        levelselect.SetActive(true);
        animator.SetBool("leveltuto", false);
    }
    

    public void Level1Screen()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        animator.SetBool("level1", true);
        Level1menu.SetActive(true);
        eventSystem.SetSelectedGameObject(startLevel1Button);
    }
    public void LeveltutoScreen()
    {
        Clearmenu();
        audioSource.PlayOneShot(validClip);
        animator.SetBool("leveltuto", true);
        Leveltutomenu.SetActive(true);
        eventSystem.SetSelectedGameObject(startTutoButton);
    }
    public void Notreadybutton()
    {
        audioSource.PlayOneShot(InvalidClip);
    }
    public void Level1start()
    {
        SceneManager.LoadScene("level_1", LoadSceneMode.Single);
        
    }
    public void LevelTutoStart()
    {
        SceneManager.LoadScene("level_tuto", LoadSceneMode.Single);

    }


    public void Clearmenu()
    {
        eventSystem.SetSelectedGameObject(playButton);
        levelselect.SetActive(false);
        Level1menu.SetActive(false);
        Leveltutomenu.SetActive(false);
    }
}

