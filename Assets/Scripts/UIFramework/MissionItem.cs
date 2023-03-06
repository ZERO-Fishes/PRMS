using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class MissionItem : MonoBehaviour
{
    private long MissionItemID;
    public Transform parentObject;
    public TextMeshProUGUI textTimeCounter;
    
    public TextMeshProUGUI textMissionCounter;
    public TextMeshProUGUI textMissionName;
    public TextMeshProUGUI textMissionProfile;
    public TextMeshProUGUI textMissionDeadLine;

    public string missionName="None";
    public string missionProfile="None";
    public string missionDeadLine="00:00:00";
    public int missionCounter=0;
    public int missionCountNum=1;


    
    private void Update()
    {
        //DeadLine文本
        //创建一个临时的date time用来计算时间；初始化时间到今天（防止Item中只填了小时数，没有日期）
        DateTime tempTime = System.DateTime.Now.ToLocalTime();
        //提取出Item中保存的时间，并尝试计算剩余时间
        if (DateTime.TryParse(missionDeadLine,out tempTime))
        {
            //DeadLine显示方法1：截止日期
            textMissionDeadLine.text = tempTime.Month.ToString()+'/'+tempTime.Day.ToString();


        }
        else
        {
            textMissionDeadLine.text = "INF";
        }
        
        //DeadLine显示方法2：剩余小时分钟数



        //TimeSpan timeRemain = deadLineTime.Subtract(nowTime);
        //MissionItem自身的显示
        textMissionName.text = missionName;
        textMissionProfile.text = missionProfile;
    }
    
    public void SendClickEvent()//由按钮触发事件，以打开编辑器
    {
        //监听编辑窗口关闭的事件，并调用方法获得编辑结果，覆盖本地数据
        GameEvents.current.EVT_MissionEditorConfirm += ReceiveData;
        GameEvents.current.EVT_MissionEditorCancel += NotReceiveData;
        
        //执行事件动作
        GameEvents.current.BC_MissionItemClick(this);
    }

    private void ReceiveData(MissionItem missionItem)
    {
        this.missionName = missionItem.missionName;
        this.missionProfile = missionItem.missionProfile;
        this.missionDeadLine = missionItem.missionDeadLine;
        this.missionCounter = missionItem.missionCounter;
        this.missionCountNum = missionItem.missionCountNum;
        Debug.Log("成功执行确认事件");
        Debug.Log("现在的name是"+missionName);
        GameEvents.current.EVT_MissionEditorConfirm -= ReceiveData;//传完之后关闭监听，不知道行不行
        GameEvents.current.EVT_MissionEditorCancel -= NotReceiveData;//没确认也要关闭监听
    }

    private void NotReceiveData()
    {
        Debug.Log("成功执行取消事件");
        GameEvents.current.EVT_MissionEditorConfirm -= ReceiveData;//传完之后关闭监听，不知道行不行
        GameEvents.current.EVT_MissionEditorCancel -= NotReceiveData;//没确认也要关闭监听
    }
}
