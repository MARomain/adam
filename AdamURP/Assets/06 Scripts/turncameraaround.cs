using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turncameraaround : MonoBehaviour
{
   
    public float valueofturn = 0.774f;
    public CinemachineVirtualCamera CVM;
    public CinemachineFramingTransposer CFT;
    // Start is called before the first frame update
    void Start()
    {
        CFT = CVM.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent <Player>() != null)
        {
            if (CVM.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX!=valueofturn)
            {
                CFT.m_ScreenX = valueofturn;
            }
          
        }
    }
}
