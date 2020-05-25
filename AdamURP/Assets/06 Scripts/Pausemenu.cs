using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pausemenu : MonoBehaviour
{
    public Player player;
    public UImanager uImanager;
    public GameObject pausemenu;
    public bool paused;
    public InputMaster controls;

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);
        uImanager = FindObjectOfType<UImanager>();
        player = FindObjectOfType<Player>();



        
    }
    private void Awake()
    {
        controls = new InputMaster();
          controls.Player.Pause.performed += ctx => Pausebutton();

    }



    private void Update()
    {
        if (Keyboard.current[Key.Space].wasReleasedThisFrame)
        {

        }
    }

    // Update is called once per frame
    void Pausebutton()
    {
        Debug.Log("pauseinput");
        if (paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        if (!player.isdead)
        {

           
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = player.slowmodievalue;
        }

        paused = false;
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
