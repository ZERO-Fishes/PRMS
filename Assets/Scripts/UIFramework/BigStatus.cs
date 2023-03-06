using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigStatus : MonoBehaviour
{

    //public float timeLeft = 60f;
    public string sleepTimeString;
    public TextMeshProUGUI sleepTimeRemainText;
    private 

    // Update is called once per frame
    void Update()
    {
        
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        DateTime sleepTime = nowTime;
        if (!DateTime.TryParse(sleepTimeString,out sleepTime))
        {
            Debug.Log("无效的会叫时间");
        }
        
        TimeSpan sleeptimeRemain = sleepTime.Subtract(nowTime);
        //Debug.Log(timeRemain.ToString());
        //nowtimeText.text = nowTime.ToString("yyyy/MM/dd HH:mm");
        
        sleepTimeRemainText.text = sleeptimeRemain.ToString(@"hh\:mm\:ss");
    }
}
