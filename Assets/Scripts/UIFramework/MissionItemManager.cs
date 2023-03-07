using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 控制MissionItem的添加，删除，以及行为
/// </summary>
public class MissionItemManager : MonoBehaviour
{
    public Transform MissionArea;
    public MissionItem missionItemPrefab;
    public List<MissionItem> MissionItemArray;
    private int MissionItemSumNum = 0;

    //由按钮调用的添加MissionItem的方法
    public void addMissionItem()
    {
        MissionItemArray.Add(Instantiate(missionItemPrefab, MissionArea));
        MissionItemSumNum++;
        Debug.Log("添加MissionItem成功");
        
    }

    private void Start()
    {
        addMissionItem();
    }

    //内置计时，用于定时切换
    private float innerTimeCounter=0;
    //用于选择一种DeadLine文本进行展示
    private bool TextDeadLineFormat;
    private void Update()
    {
        innerTimeCounter+=Time.deltaTime;

        if (innerTimeCounter>4f)//4秒执行一次
        {
            TextDeadLineFormat = !TextDeadLineFormat;//切换显示的文本
            innerTimeCounter = 0;
            Debug.Log("现在有"+MissionItemSumNum+"个MissionItem");
        }
        for (int i = 0; i < MissionItemSumNum; i++)
        {
            MissionItemArray[i].textMissionDeadLine_Date.enabled = TextDeadLineFormat;
            MissionItemArray[i].textMissionDeadLine_Hours.enabled = !TextDeadLineFormat;
        }
    }
}
