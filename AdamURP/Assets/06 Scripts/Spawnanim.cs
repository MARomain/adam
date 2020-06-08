using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnanim : MonoBehaviour
{
    public GameObject spawnobject;
    public GameObject spawnsource;


    public void Spawn()
    {
        GameObject appeared = Instantiate(spawnobject, spawnsource.transform.position, new Quaternion());
    }

}
