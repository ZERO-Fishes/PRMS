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
        //计算并显示剩余时间
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        DateTime deadLineTime=nowTime;
        if (!DateTime.TryParse(missionDeadLine,out deadLineTime))
        {
            Debug.Log("无效的截止时间");
        }
        TimeSpan timeRemain = deadLineTime.Subtract(nowTime);
        textTimeCounter.text= timeRemain.ToString(@"hh\:mm\:ss");
    }
    
    public void SendClickEvent()//由按钮触发事件，以打开编辑器
    {
        //监听编辑完成的事件，并调用方法获得编辑结果，覆盖本地数据
        GameEvents.current.EVT_MissionEditorConfirm += ReceiveData;
        
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
        GameEvents.current.EVT_MissionEditorConfirm -= ReceiveData;//传完之后关闭监听，不知道行不行
    }
    
}
