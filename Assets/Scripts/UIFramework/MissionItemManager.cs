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
    public GameObject missionItemPrefab;
    public List<GameObject> MissionItemArray;
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
        GameEvents.current.EVT_MissionItemPressed += deleteMissionItem;
        addMissionItem();
    }

    private void OnDestroy()
    {
        GameEvents.current.EVT_MissionItemPressed -= deleteMissionItem;
    }


    //内置计时，用于定时切换
    private float innerTimeCounter=0;
    //用于选择一种DeadLine文本进行展示
    private bool TextDeadLineFormat;
    private void Update()
    {
        //实时更新文本内容
        innerTimeCounter+=Time.deltaTime;

        if (innerTimeCounter>4f)//4秒执行一次
        {
            TextDeadLineFormat = !TextDeadLineFormat;//切换显示的文本
            innerTimeCounter = 0;
            Debug.Log("现在有"+MissionItemSumNum+"个MissionItem");
        }

        foreach (GameObject missionItem in MissionItemArray)
        {
            missionItem.GetComponent<MissionItem>().textMissionDeadLine_Date.enabled = TextDeadLineFormat;
            missionItem.GetComponent<MissionItem>().textMissionDeadLine_Hours.enabled = !TextDeadLineFormat;
        }

    }


    public float detectDistance=600;
    //滑动触发的事件
    public void deleteMissionItem(MissionItem missionItemToDelete)
    {
        //Debug.Log("按钮按下已被ItemManager检测到");
        StartCoroutine(CoroutineDeleteMissionItem(missionItemToDelete));//启动删除的协程

    }
    //删除过程的协程
    IEnumerator CoroutineDeleteMissionItem(MissionItem missionItemToDelete)
    {
        float moveDistance = 0;//记录手指的累计移动距离
        while (Input.touchCount > 0)//当手指还在屏幕上时
        {
            Touch touch = Input.GetTouch(0);
            moveDistance += touch.deltaPosition.x;
            Debug.Log("手指当前横向移动距离为"+moveDistance);
            Debug.Log("当前手指数为"+Input.touchCount);
            if (moveDistance<detectDistance*(-1))
            {
                Debug.Log("执行删除操作");
                MissionItemArray.Remove(missionItemToDelete.gameObject);
                Destroy(missionItemToDelete.gameObject);
                MissionItemSumNum--;
                break;
            }
            if (moveDistance>detectDistance)
            {
                Debug.Log("执行完成操作");
                MissionItemArray.Remove(missionItemToDelete.gameObject);
                Destroy(missionItemToDelete.gameObject);
                MissionItemSumNum--;
                break;
            }
            yield return null;
        }
        Debug.Log("删除协程结束");
    }
}
