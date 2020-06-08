using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destroyaftertime : MonoBehaviour
{
    public float lifetime;
    private float timeleft;
    // Start is called before the first frame update
    void Start()
    {
        timeleft = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeleft > 0)
        {
            timeleft = timeleft - Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
