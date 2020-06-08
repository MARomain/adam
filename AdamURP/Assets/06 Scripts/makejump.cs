using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makejump : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float jumpForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
