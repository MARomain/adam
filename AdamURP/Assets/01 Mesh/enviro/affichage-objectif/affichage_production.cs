using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class affichage_production : MonoBehaviour
{
    public Text score1;
    public Text name1;
    public Text score2;
    public Text name2;
    public Text score3;
    public Text name3;
    public Text score4;
    public Text name4;
    public Text score5;
    public Text name5;
    public Text score6;
    public Text name6;



    public List<Ascor_employee> employeeList;




    // Start is called before the first frame update





    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < employeeList.Count; i++)
        {
            for (int j = i + 1; j < employeeList.Count; j++)
            {
                if (employeeList[j].score > employeeList[i].score)
                {
                    Ascor_employee tmp = employeeList[i];
                    employeeList[i] = employeeList[j];
                    employeeList[j] = tmp;
                }
            }
        }
        Showscreen();

    }

    public void Showscreen()
    {
        score1.text = employeeList [0].score.ToString();
        score2.text = employeeList[1].score.ToString();
        score3.text = employeeList[2].score.ToString();
          score4.text = employeeList[3].score.ToString();
          score5.text = employeeList[4].score.ToString();
         score6.text = employeeList[5].score.ToString();



        name1.text = employeeList[0].nameemployee;
        name2.text = employeeList[1].nameemployee;
        name3.text = employeeList[2].nameemployee;
        name4.text = employeeList[3].nameemployee;
           name5.text = employeeList[4].nameemployee;
           name6.text = employeeList[5].nameemployee;

    }








}
