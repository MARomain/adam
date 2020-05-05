using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnhotstuff : MonoBehaviour
{
    public GameObject spawnobject;
    public float spawnrate;
    public Animator animator;
    public GameObject spawnsource;
    public int charge = 3;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("spawnrate", spawnrate);
    }


public void SpawnObject()
    {
        if (charge > 0)
        {
            GameObject Hotobject = Instantiate(spawnobject, spawnsource.transform.position, new Quaternion());
            Hotobject.transform.SetParent(null);
            charge--;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<pike>() != null)
        {
            Destroy(other.gameObject);
            charge++;
        }
    }
}
