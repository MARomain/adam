using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pike : MonoBehaviour
{
    public float damageammout;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bump");

        if (collision.gameObject.GetComponent<Player>().faceright == false)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(2650,3,0);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-2650, 3, 0);
        }
        collision.gameObject.GetComponent<Player>().TakeDamage(damageammout);
       // Vector3 pushdirection = transform.position - collision.transform.position;
        //collision.gameObject.GetComponent<Rigidbody>().AddForce(pushdirection.normalized * -900f);
    }
}
