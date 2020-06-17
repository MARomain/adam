using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    public GameObject player;
    public GameObject cibleplayer;

    public bool weapon1picked = false;

    public string weapon1firetype = "auto";
    public int weapon1munitions = 20;
    public GameObject bulletplayertype1;



    public string weapon2firetype = "railgun";
    public float weapon2chargetime = 1;
    public float weapon2chargevalue = 0;
    public bool weapon2charged=false;
    public int weapon2munitions = 20;
    public GameObject bulletplayertype2;

    public bool weapon3picked = false;
    public string weapon3firetype = "semi-auto";
    public int weapon3munitions = 50;
    public GameObject bulletplayertype3;


    public string weapon4firetype = "semi-auto";
    public int weapon4munitions = 50;
    public GameObject bulletplayertype4;

}
