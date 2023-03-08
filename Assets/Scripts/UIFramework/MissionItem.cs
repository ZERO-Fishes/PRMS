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
    public TextMeshProUGUI textMissionDeadLine_Date;
    public TextMeshProUGUI textMissionDeadLine_Hours;

    public string missionName="None";
    public string missionProfile="None";
    public string missionDeadLine="00:00:00";
    public int missionCounter=0;
    public int missionCountNum=1;
    



    
    private void Update()
    {
        //MissionItem自身的显示
        textMissionName.text = missionName;
        textMissionProfile.text = missionProfile;
        
        //DeadLine文本
        //创建一个临时的date time用来计算时间；初始化时间到今天（防止Item中只填了小时数，没有日期）
        DateTime tempTime = System.DateTime.Now.ToLocalTime();
        //提取出Item中保存的时间，并尝试计算剩余时间
        if (DateTime.TryParse(missionDeadLine,out tempTime))
        {
            TimeSpan timeRemained = tempTime.Subtract(System.DateTime.Now.ToLocalTime());
            if (timeRemained.Hours<=0)//计算出时间为负数表示超时
            {
                textMissionDeadLine_Date.text = "Overtime";
                textMissionDeadLine_Hours.text = "Overtime";
            }
            else
            {
                //DeadLine显示方法1：截止日期
                textMissionDeadLine_Date.text = "Date  "+tempTime.Month.ToString()+'/'+tempTime.Day.ToString();
                //DeadLine显示方法2：剩余小时分钟数
                textMissionDeadLine_Hours.text = "Remain  "+Convert.ToInt32(timeRemained.TotalHours).ToString() + ":" +
                                                 timeRemained.Minutes.ToString();
            }
        }
        else//无效时间则默认设成无限
        {
            textMissionDeadLine_Date.text = "Infinite";
            textMissionDeadLine_Hours.text = "Infinite";
        }
    }
    
    public void ItemClickedEvent()//由按钮点击触发事件，以打开编辑器
    {
        //监听编辑窗口关闭的事件，并调用方法获得编辑结果，覆盖本地数据
        GameEvents.current.EVT_MissionEditorConfirm += ReceiveData;
        GameEvents.current.EVT_MissionEditorCancel += NotReceiveData;
        
        //执行事件动作
        GameEvents.current.BC_MissionItemClick(this);
    }

    public void ItemPressedEvent()
    {
        GameEvents.current.BC_MissionItemPressed(this);
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
