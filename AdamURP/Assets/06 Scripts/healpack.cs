using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healpack : MonoBehaviour
{
    public float healvalue;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detect");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("tu passe");
            if (other.GetComponent<Player>().health < other.GetComponent<Player>().maxhealth)
            {
                other.GetComponent<Player>().Heal(healvalue);
                Destroy(this.gameObject);
                Debug.Log("heal");
            }
            else
            {
                Debug.Log("toute ta vie deja");
            }
          
        }
 
    }
}
