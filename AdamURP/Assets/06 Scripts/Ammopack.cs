using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammopack : MonoBehaviour
{

    public bool destroyafteruse = true;
    public Library lb;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detect");
        if (other.gameObject.tag == "Player")
        {
            lb = FindObjectOfType<Library>();

            switch (other.GetComponent<Player>().weapontype)
            {
                case 0:

                    break;
                case 1:

                    if (other.GetComponent<Player>().ammoleft != lb.weapon1munitions)
                    {
                        other.GetComponent<Player>().RefillAmmo();
                        if (destroyafteruse)
                        {
                            Destroy(this.gameObject);
                        }

                    }

                    break;


                case 2:

                    if (other.GetComponent<Player>().ammoleft != lb.weapon2munitions)
                    {
                        other.GetComponent<Player>().RefillAmmo();
                        if (destroyafteruse)
                        {
                            Destroy(this.gameObject);
                        }

                    }

                    break;
                case 3:

                    if (other.GetComponent<Player>().ammoleft != lb.weapon3munitions)
                    {
                        other.GetComponent<Player>().RefillAmmo();
                        if (destroyafteruse)
                        {
                            Destroy(this.gameObject);
                        }

                    }
                    break;
            }







      
        
     

        }

    }
}
