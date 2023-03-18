using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
/// <summary>
/// 控制MissionItem的添加，删除，以及行为
/// </summary>
public class MissionItemManager : MonoBehaviour
{
    
    public static MissionItemManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }
    
    public EventSystem eventSystem;
    public Transform MissionArea;
    public GameObject missionItemPrefab;
    
    public List<GameObject> MissionItemArray;
    //存储所有MissionItem的数据,需要额外存储

    public List<long> MissionItemDataIDs;
    public long MissionItemSumNum = 0;
    public long MissionItemNowID = 0;
    
    
    /// 记录指针状态，是按下还是松开
    private bool PointerDownStatus;
    ///记录指针按下的时间，超过一定值再启用按钮
    private float PointerDownTime=0;
    ///内置计时，用于定时切换
    private float innerTimeCounter=0;
    ///用于选择一种DeadLine文本进行展示
    private bool TextDeadLineFormat=false;

    private bool TextCounterFormat=false;


    
    private void Start()
    {
        //监听按钮按下的事件
        GameEvents.current.EVT_MissionItemPressed += deleteMissionItem;
        //addMissionItem();
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
        MissionItemArray.Add(Instantiate(missionItemPrefab, MissionArea));//实例化一个对象,保存在manager中的list中
        MissionItemArray[MissionItemArray.Count-1].GetComponent<MissionItem>().missionItemData.missionItemID = MissionItemNowID;//为其SO赋ID
        //保存SO到磁盘
        SaveManager.Instance.Save(this.MissionItemArray[MissionItemArray.Count-1].GetComponent<MissionItem>().missionItemData,this.MissionItemNowID.ToString());

        MissionItemDataIDs.Add(MissionItemNowID);
        MissionItemNowID++;//ID+1
        MissionItemSumNum++;
        SaveItemDataBank();
        
        Debug.Log("添加MissionItem成功");
    }

    
    private void SaveItemDataBank()
    {
        PlayerPrefs.SetString("ID", MissionItemNowID.ToString());//存储现在发到的ID
        PlayerPrefs.SetString("SumNum", MissionItemSumNum.ToString());//存储总Item数
        string IDsString = string.Join(",", MissionItemDataIDs);//连接成string
        PlayerPrefs.SetString("IDs",IDsString);//存储所有ID
        PlayerPrefs.Save();
        Debug.Log("保存元数据成功");
    }

    public void LoadItemDataBank()
    {
        string tempstring;
        tempstring=PlayerPrefs.GetString("IDs");
        string[] stringArray = tempstring.Split(",");//提取IDs
        MissionItemDataIDs.Clear();
        foreach (var str in stringArray)
        {
            MissionItemDataIDs.Add(long.Parse(str));
        }
        tempstring=PlayerPrefs.GetString("SumNum");
        long.TryParse(tempstring,out MissionItemSumNum);
        tempstring=PlayerPrefs.GetString("ID");
        long.TryParse(tempstring,out MissionItemNowID);
    }

    public MissionItemData_SO missionItemDataSo;
    public void InitializeMissionArea()//初始化MissionArea
    {
        foreach (var missionItem in MissionItemArray)
        {
            Destroy(missionItem.gameObject);//删除game object
        }
        MissionItemArray.Clear();//list中删除
        Debug.Log("清空MissionArea");

        int i = 0;
        foreach (var missionItemID in MissionItemDataIDs)
        {
            //实例化一个对象,保存在manager中的list中
            MissionItemArray.Add(Instantiate(missionItemPrefab, MissionArea));
            Debug.Log("添加"+i+"个实例");
            //加载SO数据
            //JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(missionItemID.ToString()), MissionItemArray[i].GetComponent<MissionItem>().missionItemData);
            
            //SaveManager.Instance.Load(Instance.missionItemDataSo,missionItemID.ToString());
            SaveManager.Instance.Load(Instance.MissionItemArray[i].GetComponent<MissionItem>().missionItemData,missionItemID.ToString());
            Instance.MissionItemArray[i].GetComponent<MissionItem>().missionItemData.NeedToUpdate = true;//刷新显示
            i++;
        }

    }
    

    private void Update()
    {
        //实时更新文本内容
        innerTimeCounter+=Time.deltaTime;

        if (innerTimeCounter>4f)//4秒执行一次
        {
            TextDeadLineFormat = !TextDeadLineFormat;//切换显示的文本
            TextCounterFormat = !TextCounterFormat;//切换显示的文本
            innerTimeCounter = 0;
            Debug.Log("现在有"+MissionItemSumNum+"个MissionItem");
            ToggleMissionDeadlineText();
            ToggleMissionCounterText();
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

    private void ToggleMissionDeadlineText()
    {
        foreach (GameObject missionItem in MissionItemArray)
        {
            missionItem.GetComponent<MissionItem>().textMissionDeadLine_Date.enabled = TextDeadLineFormat;
            missionItem.GetComponent<MissionItem>().textMissionDeadLine_Hours.enabled = !TextDeadLineFormat;
        }
    }

    private void ToggleMissionCounterText()
    {
        foreach (GameObject missionItem in MissionItemArray)
        {
            missionItem.GetComponent<MissionItem>().textMissionCounter_Percent.enabled = TextCounterFormat;
            missionItem.GetComponent<MissionItem>().textMissionCounter_Rate.enabled = !TextCounterFormat;
        }

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
            if (moveDistance>detectDistance)
            {
                Debug.Log("执行完成操作");
                missionItemToComplete.MissionCounter++;//达到100%才删除
                if (missionItemToComplete.UpdateMissionCounterText() >=100)
                {
                    DeleteMissionItem(missionItemToComplete);
                }
                else//否则更新进度并保存
                {
                    SaveManager.Instance.Save(missionItemToComplete.missionItemData,missionItemToComplete.missionItemData.missionItemID.ToString());
                    missionItemToComplete.missionItemData.NeedToUpdate = true;//刷新
                }
                break;
            }
            yield return null;
        }
        Debug.Log("完成MissionItem协程结束");
    }

    private void DeleteMissionItem(MissionItem missionItemToComplete)
    {
        MissionItemArray.Remove(missionItemToComplete.gameObject);//list中删除
        Destroy(missionItemToComplete.gameObject);//删除game object
        //磁盘中删除
        SaveManager.Instance.Delete(missionItemToComplete.missionItemData.missionItemID.ToString());
        MissionItemSumNum--;//Item总数-1
        MissionItemDataIDs.Remove(missionItemToComplete.missionItemData.missionItemID);//从ID列表中移除
        SaveItemDataBank();//保存元数据
        Debug.Log("删除MissionItem");
    }
    
    
    
    
}