using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transition2;
    public float transitionTime = 1f;

    public InputMaster controls;

    
    private void Awake()
    {
        controls = new InputMaster();

        controls.InMenu.StartToPlay.performed += ctx => LoadNextLevel();
    }


    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        if (transition2 != null)
            transition2.SetTrigger("Start");
        controls.InMenu.Disable();
        yield return new WaitForSeconds(transitionTime);
        //SceneManager.LoadScene(levelIndex);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }



}
