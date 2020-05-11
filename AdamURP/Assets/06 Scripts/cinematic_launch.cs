using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematic_launch : MonoBehaviour
{
    public Animator playeranimator;
    public RuntimeAnimatorController cinematic;
    public GameObject shuushcinemaichne;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shuushcinemaichne.SetActive(false);
            playeranimator = other.GetComponent<Animator>();
            playeranimator.runtimeAnimatorController = cinematic;
        }
    }
}
