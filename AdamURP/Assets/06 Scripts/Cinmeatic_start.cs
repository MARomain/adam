using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinmeatic_start : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController cinematic;
    public bool active = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (active == true)
            {
                animator.runtimeAnimatorController = cinematic;
                active = false;
            }
          
        }
    }

}
