using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject btnDestroy;
    public GameObject btnMenu;
    public GameObject btnSwap;
    public GameObject btnTitle;
    public GameObject btnButtom;
    public static CanvasController instance;
    public Text txtScore;
    public float score = 0;
    public Text txtTime;
    int time = 3610;
    int a = 0;
    int temp;
    public void Awake()
    {
        instance = this;
    }
    public void SetScore(float x )
    {
        score += x;
        txtScore.text = "Score:  "+ score.ToString();
    }
   
    public void SetTime()
    {
        string strMinute, strSecond,strHour;
        temp = (time / 3600)* time;
        int hour = time / 3600;
        int minute = time/60;
        int second = time % 60;
        strHour = hour.ToString();
        strMinute = minute.ToString();
        strSecond = second.ToString();

       if(a < Time.time)
        {
            time = time - 1;
            a = a + 1;

        }
       if(minute == 60)
        {
            strMinute = "00";
        }
       if(second <10)
        {
            strSecond = "0" + second;
        }
       if(minute<10)
        {
            strMinute = "0" + minute;
        }
        txtTime.text = strHour + ":" + strMinute + ":" + strSecond;
    }
}

