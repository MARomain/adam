using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looktarget : MonoBehaviour
{
    public GameObject cible;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = cible.transform.position - transform.position;
      
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);
    }
}
