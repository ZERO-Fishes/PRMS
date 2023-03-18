using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class MissionItem : MonoBehaviour
{
    private long _missionItemID;
    public Transform parentObject;
    public GameObject ItemButton;
    public TextMeshProUGUI textTimeCounter;
    public MissionItemData_SO missionItemDataTemplate;
    public MissionItemData_SO missionItemData;

    public TextMeshProUGUI textMissionName;
    public TextMeshProUGUI textMissionProfile;
    public TextMeshProUGUI textMissionDeadLine_Date;
    public TextMeshProUGUI textMissionDeadLine_Hours;
    public TextMeshProUGUI textMissionCounter_Percent;
    public TextMeshProUGUI textMissionCounter_Rate;
    

    #region Read from MissionItemData_SO
    public string MissionName
    {
        get =>missionItemData != null ? missionItemData.missionName : "未初始化任务名";
        set => missionItemData.missionName = value;
    }
    public string MissionProfile
    {
        get =>missionItemData != null ? missionItemData.missionProfile : "未初始化任务简介";
        set => missionItemData.missionProfile = value;
    }
    public string MissionDeadLine
    {
        get =>missionItemData != null ? missionItemData.missionDeadLine : "00：00：00";
        set => missionItemData.missionDeadLine = value;
    }
    public int MissionRepeatMode
    {
        get =>missionItemData != null ? missionItemData.missionRepeatMode : 0;
        set => missionItemData.missionRepeatMode = value;
    }
    public int MissionTag
    {
        get =>missionItemData != null ? missionItemData.missionTag : 0;
        set => missionItemData.missionTag = value;
    }
    public int MissionCounter
    {
        get =>missionItemData != null ? missionItemData.missionCounter : 0;
        set => missionItemData.missionCounter = value;
    }
    public int MissionCountNum
    {
        get =>missionItemData != null ? missionItemData.missionCountNum : 1;
        set => missionItemData.missionCountNum = value;
    }
    public int MissionDiamond
    {
        get =>missionItemData != null ? missionItemData.missionDiamond : 1;
        set => missionItemData.missionDiamond = value;
    }
    public int MissionAwardType
    {
        get =>missionItemData != null ? missionItemData.missionAwardType : 0;
        set => missionItemData.missionAwardType = value;
    }
    public int MissionAwardNum
    {
        get =>missionItemData != null ? missionItemData.missionAwardNum : 1;
        set => missionItemData.missionAwardNum = value;
    }
    #endregion

    private void Awake()
    {
        if (missionItemDataTemplate!=null)
        {
            missionItemData = Instantiate(missionItemDataTemplate);
        }
    }

    private void Start()
    {
        //MissionItem自身的显示
        UpdateMainText();
        UpdateDeadLineText_Date();
        UpdateDeadLineText_Hours();
        UpdateMissionCounterText();
    }


    private void Update()
    {
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        if (nowTime.Second==60)
        {
            //MissionItem自身的显示
            UpdateMainText();
            UpdateDeadLineText_Date();
            UpdateDeadLineText_Hours();
            UpdateMissionCounterText();
        }

        if (missionItemData.NeedToUpdate==true)
        {
            UpdateMainText();
            UpdateDeadLineText_Date();
            UpdateDeadLineText_Hours();
            UpdateMissionCounterText();
            missionItemData.NeedToUpdate = false;
        }

        

    }
    private void UpdateMainText()
    {
        textMissionName.text = MissionName;
        textMissionProfile.text = MissionProfile;
    }

    private void UpdateDeadLineText_Date()
    {
        //DeadLine文本
        //创建一个临时的date time用来计算时间；初始化时间到今天（防止Item中只填了小时数，没有日期）
        DateTime tempTime = System.DateTime.Now.ToLocalTime();
        //提取出Item中保存的时间，并尝试计算剩余时间
        if (DateTime.TryParse(MissionDeadLine,out tempTime))
        {
            TimeSpan timeRemained = tempTime.Subtract(System.DateTime.Now.ToLocalTime());
            if (timeRemained.Hours<0)//计算出时间为负数表示超时
            {
                textMissionDeadLine_Date.text = "Overtime";
            }
            else
            {
                //DeadLine显示方法1：截止日期
                textMissionDeadLine_Date.text = "Date  "+tempTime.Month.ToString()+'/'+tempTime.Day.ToString();
            }
        }
        else//无效时间则默认设成无限
        {
            textMissionDeadLine_Date.text = "Infinite";
        }
    }
    private void UpdateDeadLineText_Hours()
    {
        //DeadLine文本
        //创建一个临时的date time用来计算时间；初始化时间到今天（防止Item中只填了小时数，没有日期）
        DateTime tempTime = System.DateTime.Now.ToLocalTime();
        //提取出Item中保存的时间，并尝试计算剩余时间
        if (DateTime.TryParse(MissionDeadLine,out tempTime))
        {
            TimeSpan timeRemained = tempTime.Subtract(System.DateTime.Now.ToLocalTime());
            if (timeRemained.Hours<0)//计算出时间为负数表示超时
            {
                textMissionDeadLine_Hours.text = "Overtime";
            }
            else
            {
                //DeadLine显示方法2：剩余小时分钟数
                textMissionDeadLine_Hours.text = "Remain  "+Convert.ToInt32(timeRemained.TotalHours).ToString() + ":" +
                                                 timeRemained.Minutes.ToString();
            }
        }
        else//无效时间则默认设成无限
        {
            textMissionDeadLine_Hours.text = "Infinite";
        }
    }
    
    public void ItemClickedEvent()//由按钮点击触发事件，以打开编辑器
    {
        /*//监听编辑窗口关闭的事件，并调用方法获得编辑结果，覆盖本地数据
        GameEvents.current.EVT_MissionEditorConfirm += ReceiveData;
        GameEvents.current.EVT_MissionEditorCancel += NotReceiveData;
        
        //执行事件动作
        GameEvents.current.BC_MissionItemClick(this);*/
        //打开编辑器
        MissionEditorManager.Instance.ReceiveItemAndOpenEditor(missionItemData);
    }


    /// <summary>
    /// 编辑器中的数据传入时更新MissionCounter显示内容
    /// </summary>

    public int UpdateMissionCounterText()
    {
        int percent = 0;
        if (this.MissionCountNum<1)
        {
            this.MissionCountNum = 1;
        }
        percent= this.MissionCounter*100 / this.MissionCountNum;

         
        textMissionCounter_Percent.text = percent.ToString()+"%";
        return percent;
    }

    public void ItemPressedEvent()//按钮按下后触发ItemManager中的方法
    {
        MissionItemManager.Instance.deleteMissionItem(this);
    }

    /*private void ReceiveData(MissionItem missionItem)
    {
        this.MissionName = missionItem.MissionName;
        this.MissionProfile = missionItem.MissionProfile;
        this.MissionDeadLine = missionItem.MissionDeadLine;
        this.MissionCounter = missionItem.MissionCounter;
        this.MissionCountNum = missionItem.MissionCountNum;
        UpdateMainText();
        UpdateDeadLineText_Date();
        UpdateDeadLineText_Hours();
        UpdateMissionCounterText();//更新MissionCounter文本
        
        Debug.Log("成功执行确认事件");
        Debug.Log("现在的name是"+MissionName);
        GameEvents.current.EVT_MissionEditorConfirm -= ReceiveData;//传完之后关闭监听，不知道行不行
        GameEvents.current.EVT_MissionEditorCancel -= NotReceiveData;//没确认也要关闭监听
    }

    private void NotReceiveData()
    {
        Debug.Log("成功执行取消事件");
        GameEvents.current.EVT_MissionEditorConfirm -= ReceiveData;//传完之后关闭监听，不知道行不行
        GameEvents.current.EVT_MissionEditorCancel -= NotReceiveData;//没确认也要关闭监听
    }*/
}
