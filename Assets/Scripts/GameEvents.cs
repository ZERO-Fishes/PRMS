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
    
    //按下MissionItem的事件
    public event Action<MissionItem> EVT_MissionItemPressed;

    public void BC_MissionItemPressed(MissionItem missionItem)
    {
        if (EVT_MissionItemPressed!=null)
        {
            //该行为调用这个事件动作
            EVT_MissionItemPressed(missionItem);
        }
    }
    
    
    //关闭MissionEditor并保存数据的事件
    public event Action<MissionItem> EVT_MissionEditorConfirm;

    public void BC_MissionEditorConfirm(MissionItem missionItem)
    {
        if (EVT_MissionEditorConfirm!=null)
        {
            Debug.Log("成功调用确认事件（未执行）");
            //该行为调用这个事件动作
            EVT_MissionEditorConfirm(missionItem);
        }
    }
    
    //关闭MissionEditor并不保存数据的事件
    public event Action EVT_MissionEditorCancel;

    public void BC_MissionEditorCancel()
    {
        if (EVT_MissionEditorCancel!=null)
        {
            Debug.Log("成功调用取消事件（未执行）");
            //该行为调用这个事件动作
            EVT_MissionEditorCancel();
        }
    }
}
