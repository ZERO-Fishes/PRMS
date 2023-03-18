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
    public int missionTag;
    [Header("Processing")]
    public int missionCounter;
    public int missionCountNum;
    [Header("Award")]
    public int missionDiamond;
    public int missionAwardType;
    public int missionAwardNum;

}
