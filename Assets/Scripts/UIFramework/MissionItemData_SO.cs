using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New MissionItemData",menuName = "PRMS/MissionItemData")]
public class MissionItemData_SO : ScriptableObject
{
    public bool NeedToUpdate = false;
    public long missionItemID;
    [Header("Main Info")] 
    public string missionName;
    public string missionProfile;
    public string missionDeadLine;
    public int missionRepeatMode;
    /// <summary>
    /// 0:一次性
    /// 1:重复
    /// </summary>
    public int missionTag;
    
    /// <summary>
    /// 重复次数计数器
    /// </summary>
    [Header("Processing")]
    public int missionCounter;
    /// <summary>
    ///重复次数总数
    /// </summary>
    public int missionCountNum;

    public bool missionCompletedToday;
    [Header("Award")] 
    public long missionExp;
    public int missionDiamond;
    public int missionAwardType;
    public int missionAwardNum;

}
