using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 控制MissionItem的添加，删除，以及行为
/// </summary>
public class MissionItemManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public Transform MissionArea;
    public GameObject missionItemPrefab;
    public List<GameObject> MissionItemArray;
    private int MissionItemSumNum = 0;
    /// 记录指针状态，是按下还是松开
    private bool PointerDownStatus;
    ///记录指针按下的时间，超过一定值再启用按钮
    private float PointerDownTime=0;
    ///内置计时，用于定时切换
    private float innerTimeCounter=0;
    ///用于选择一种DeadLine文本进行展示
    private bool TextDeadLineFormat;


    
    private void Start()
    {
        //监听按钮按下的事件
        GameEvents.current.EVT_MissionItemPressed += deleteMissionItem;
        addMissionItem();
        //初始化按下的时间
        PointerDownTime = 0;
    }

    private void OnDestroy()
    {
        GameEvents.current.EVT_MissionItemPressed -= deleteMissionItem;
    }
    
    //由按钮调用的添加MissionItem的方法
    public void addMissionItem()
    {
        MissionItemArray.Add(Instantiate(missionItemPrefab, MissionArea));
        MissionItemSumNum++;
        Debug.Log("添加MissionItem成功");
        
    }
    
/*/// <summary>
/// pointerDown时调用
/// </summary>
    public void PointerStatusToDown()
    {
        Debug.Log("检测到指针按下");
        PointerDownStatus = true;

    }
    /// <summary>
    /// pointerUp时调用
    /// </summary>
    public void PointerStatusToUp()
    {
        Debug.Log("检测到指针抬起");
        PointerDownStatus = false;
        PointerDownTime = 0;
        CloseButton();
    }

    private void OpenButton()
    {
        foreach (GameObject missionItem in MissionItemArray)
        {
            //激活按钮
            Debug.Log("激活按钮成功");
            missionItem.GetComponent<MissionItem>().ItemButton.SetActive(true); 
        }

        Debug.Log(eventSystem.currentSelectedGameObject);
    }

    private void CloseButton()
    {
        foreach (GameObject missionItem in MissionItemArray)
        {
            //激活按钮
            Debug.Log("去激活按钮成功");
            missionItem.GetComponent<MissionItem>().ItemButton.SetActive(false); 
        }
    }*/
    
    

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
        
        
        /*//根据PointerDownStatus来累加按钮按下的时间
        if (PointerDownStatus)
        {
            PointerDownTime += Time.deltaTime;
        }

        if (PointerDownTime>2&&PointerDownTime<3)
        {
            OpenButton();
        }*/

    }


    public float detectDistance=600;
    //滑动触发的事件
    public void deleteMissionItem(MissionItem missionItemToComplete)
    {
        //Debug.Log("按钮按下已被ItemManager检测到");
        StartCoroutine(CoroutineDeleteMissionItem(missionItemToComplete));//启动删除的协程

    }
    //删除过程的协程
    IEnumerator CoroutineDeleteMissionItem(MissionItem missionItemToComplete)
    {
        float moveDistance = 0;//记录手指的累计移动距离
        while (Input.touchCount > 0)//当手指还在屏幕上时
        {
            Touch touch = Input.GetTouch(0);
            moveDistance += touch.deltaPosition.x;
            Debug.Log("手指当前横向移动距离为"+moveDistance);
            Debug.Log("当前手指数为"+Input.touchCount);
            //删除操作暂时禁用
            /*if (moveDistance<detectDistance*(-1))
            {
                Debug.Log("执行删除操作");
                MissionItemArray.Remove(missionItemToComplete.gameObject);
                Destroy(missionItemToComplete.gameObject);
                MissionItemSumNum--;
                break;
            }*/
            if (moveDistance>detectDistance)
            {
                Debug.Log("执行完成操作");
                missionItemToComplete.missionCounter++;
                if (missionItemToComplete.UpdateMissionCounter() >=100)//达到100%才删除
                {
                    MissionItemArray.Remove(missionItemToComplete.gameObject);
                    Destroy(missionItemToComplete.gameObject);
                    MissionItemSumNum--;
                }
                break;
            }
            yield return null;
        }
        Debug.Log("完成MissionItem协程结束");
    }
}
