using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombExplosionRadius;
    public float bombDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().TakeDamage(bombDamage);
            Destroy(this.gameObject);
        }


        //Tentative AOE pas éprouver à retoucher

        //Collider[] colliders = Physics.OverlapSphere(transform.position, bombExplosionRadius);
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    if (GetComponent<Player>() != null)
        //    {
        //        colliders[i].GetComponent<Player>().TakeDamage(bombDamage);
        //    }
        //}

        Destroy(this.gameObject, 5f);
    }

   
}
