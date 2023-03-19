using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAwake : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Application.targetFrameRate = 120;
    }
}
