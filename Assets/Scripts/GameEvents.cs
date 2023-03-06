using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    
    private void Awake()
    {
        //创建一个这个类的全局唯一实例
        current = this;
    }

    //创建一个事件动作（列表）
    //点击MissionItem的事件
    public event Action<MissionItem> EVT_MissionItemClick;
    //用来触发事件的函数
    public void BC_MissionItemClick(MissionItem missionItem)
    {
        if (EVT_MissionItemClick!=null)
        {
            //该行为调用这个事件动作
            EVT_MissionItemClick(missionItem);
        }
        
    }
}
