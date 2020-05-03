using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnhotstuff : MonoBehaviour
{
    public GameObject spawnobject;
    public float spawnrate;
    public Animator animator;
    public GameObject spawnsource;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("spawnrate", spawnrate);
    }


public void SpawnObject()
    {
        GameObject Hotobject = Instantiate(spawnobject, spawnsource.transform.position, new Quaternion());
        Hotobject.transform.SetParent(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<pike>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
