using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NowTime : MonoBehaviour
{

    public TextMeshProUGUI textNowTime;

    private void Update()
    {
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        textNowTime.text=nowTime.ToString("yyyy/MM/dd HH:mm");
    }
}
