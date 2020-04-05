using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapisroulant : MonoBehaviour
{
    public float speed;


    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.transform.position = new Vector3((collision.gameObject.transform.position.x+(speed*Time.deltaTime)), collision.gameObject.transform.position.y, collision.gameObject.transform.position.z) ;
    }

}
