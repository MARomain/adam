using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossdeath : MonoBehaviour
{
    public Ennemy boss;
    public Animator animator;
    public Animator dronepath;
    public Animator spawner1;
    public Animator spawner2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.health <= 0)
        {
            animator.SetTrigger("action");
            dronepath.enabled = false;
            spawner1.enabled = false;
            spawner2.enabled = false;
        }
    }
}
