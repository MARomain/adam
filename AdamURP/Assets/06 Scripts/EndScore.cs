using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Player player;
    public chrono chrono;
    public Text Scorehit;
    public Text secondsaffiche;
    public Text minutesaffiche;
    private float tempsround;
    public GameObject notperfect;
    public GameObject perfect;
   
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            chrono.Chronostop();

           Scoreupdate();
        }
    }
    private void Scoreupdate()
    {
        if (player.hittedtimes == 0)
        {
            notperfect.gameObject.SetActive(false);//double tap
            perfect.gameObject.SetActive(true);
        }
        else
        {
            perfect.gameObject.SetActive(false);
            notperfect.gameObject.SetActive(true);
            Scorehit.text = player.hittedtimes.ToString();
        }


        tempsround = Mathf.Round(chrono.seconds);
        secondsaffiche.text = tempsround.ToString();

        if (chrono.minutes <10)
        {
            minutesaffiche.text =("0"+ chrono.minutes.ToString());
        }
        else
        {
            minutesaffiche.text = chrono.minutes.ToString();
        }

        







    }
}
