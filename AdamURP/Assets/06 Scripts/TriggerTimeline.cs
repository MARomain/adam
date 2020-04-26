using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timeline;

    //DEBUG UI
    public GameObject debugCanvas;
    public Text debugTimelineText;
    public Text debugCameraText;
    private double countDown;
    private bool startCountDown = false;
    private bool trackActiveCam = false;
    // Start is called before the first frame update
    void Start()
    {
            StartCameraDebugTest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timeline.Play();
            //disable player inputs

            //Debug
            EnableDebugUI();
            StartTimelineDebugTimer();
        }
    }

    void EnableDebugUI()
    {
        debugCanvas.SetActive(true);
    }

    void StartTimelineDebugTimer()
    {
        if (debugTimelineText != null)
        {
            countDown = timeline.duration;
            debugTimelineText.text = timeline.duration.ToString("F1");
            startCountDown = true;
        }
    }

    void StartCameraDebugTest()
    {
        trackActiveCam = true;
    }

    void Update()
    {
        //DEBUG UI Debug Timer
        if(startCountDown == true)
        {
            countDown -= Time.deltaTime;
            debugTimelineText.text = countDown.ToString("F1");

            
        }
        if (countDown <= 0)
        {
            startCountDown = false;
            debugTimelineText.text = "0";
        }
        //DEBUG UI Debug Timer



        //DEBUG UI Active Camera
        if(trackActiveCam == true)
        {
            debugCameraText.text = FindObjectOfType<CinemachineBrain>().GetComponent<CinemachineBrain>().ActiveVirtualCamera.ToString();
            //erreur console connu, ne semble pas poser de probleme
        }
        //DEBUG UI Active Camera
    }
}
