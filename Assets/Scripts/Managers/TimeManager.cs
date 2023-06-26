using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }

    public bool IsNewDay;

    // Start is called before the first frame update
    void Start()
    {
        IsNewDay = NewDayDetect();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        if (nowTime.Second%15==0)//每15s执行一次
        {
            IsNewDay = NewDayDetect();
        }

        if (IsNewDay)
        {
            NewDayProperty();
            NewDayMissionItem();
            AppriseManager.Instance.NTC_NewDay();
            IsNewDay = false;
        }

    }

    /// <summary>
    /// 用于更新本地用于比较的时间，并检测是否是新的一天
    /// </summary>
    /// <returns>是否是新一天</returns>
    private bool NewDayDetect()
    {
        String S_SavedTime = PlayerPrefs.GetString("SavedTime","2000/1/1 00:00:00");
        DateTime savedTime;
        DateTime.TryParse(S_SavedTime, out savedTime);
        
        DateTime nowTime = System.DateTime.Now.ToLocalTime();
        
        PlayerPrefs.SetString("SavedTime",nowTime.ToString());//保存时间
        
        //两种情况，一种是日期相差超过一天，一种是在同一天，但是保存的是4点前，当前是4点后
        if (nowTime.Year>savedTime.Year||nowTime.DayOfYear>savedTime.DayOfYear)
        {
            return true;
        }
        else if (savedTime.Hour<4 && nowTime.Hour>=4)
        {
            return true;
        }

        return false;
    }

    
    /// <summary>
    /// 新的一天更新属性值
    /// </summary>
    private void NewDayProperty()
    {
        StatusManager.Instance.statusDataSo.TagValue_1 -= 1;
        StatusManager.Instance.statusDataSo.TagValue_2 -= 1;
        StatusManager.Instance.statusDataSo.TagValue_3 -= 1;
        StatusManager.Instance.statusDataSo.TagValue_4 -= 1;
        StatusManager.Instance.statusDataSo.TagValue_5 -= 1;
        StatusManager.Instance.SaveStatusData();
        StatusManager.Instance.UpdateTexts();
    }

    private void NewDayMissionItem()
    {
        //遍历所有Mission Item
        for (int i = 0; i < MissionItemManager.Instance.MissionItemSumNum; i++)
        {
            //将MissionItem标记为今日未完成
            MissionItemManager.Instance.MissionItemArray[i].GetComponent<MissionItem>().missionItemData
                .missionCompletedToday = false;
            //保存并刷新MissionItem
            MissionItemManager.Instance.UpdateMissionItem(MissionItemManager.Instance.MissionItemArray[i].GetComponent<MissionItem>());
        }
    }
}
