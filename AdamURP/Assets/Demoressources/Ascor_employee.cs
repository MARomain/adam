using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascor_employee : MonoBehaviour
{


    public string nameemployee;
    public int score;

    public int startvalue1;

    public float speedvalue1 = 1;
    public float workingtime1 = 1;
    public float progress1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progress1 < workingtime1)
        {
            //en train de travailler sur ca piece
            progress1 = progress1 + (Time.deltaTime * speedvalue1);
        }
        else
        {
            score++;
            progress1 = 0;
        }


    }
}
