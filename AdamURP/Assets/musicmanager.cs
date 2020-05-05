using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
    public AudioSource audiosource;


    public void Dieeffectonmusic()
    {
        audiosource.pitch = 0.50f;
    }
}
