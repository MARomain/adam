using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip levelmusic;

    public void Dieeffectonmusic()
    {
        audiosource.pitch = 0.50f;
    }
    public void Startmusic()
    {
        audiosource.PlayOneShot(levelmusic);
    }
    public void Stopmusic()
    {
        audiosource.Stop();
    }
}
