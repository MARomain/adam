using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class chrono : MonoBehaviour
{
    public bool chronoON = true;
    public float seconds = 0;
    public int minutes = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (chronoON)
        {
            seconds = seconds + Time.deltaTime;
        }
        if (seconds >= 60)
        {
            seconds = seconds - 60;
            minutes++;
        }
    }

    public void Chronostop()
    {
        chronoON = false;
        
    }
    public void Chronocontinue()
    {
        chronoON = true;

    }
    public void ChronoRestart()
    {
        seconds = 0;
        minutes = 0;
    }
}
