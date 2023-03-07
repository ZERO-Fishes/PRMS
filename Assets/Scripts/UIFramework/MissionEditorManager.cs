using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MissionEditorManager : MonoBehaviour
{
    public GameObject missionEditor;
    

    private void Start()
    {
        //将该方法添加到事件动作的列表中，表示事件触发时执行该方法，也就是订阅了该事件
        GameEvents.current.EVT_MissionItemClick += ActiveMissionEditor;
    }

    private void OnDestroy()
    {
        //销毁物体时必须将这个方法从事件动作列表中删除，否则就会因为没法调用这个方法而出错
        GameEvents.current.EVT_MissionItemClick -= ActiveMissionEditor;
    }



    /// <summary>
    /// 根据MissionItem的数据初始化MissionEditor的窗口数据，并激活窗口
    /// </summary>
    /// <param name="missionItem"></param>
    private void ActiveMissionEditor(MissionItem missionItem)
    {
        //传入数据到missionEditor，里面会自动处理
        missionEditor.GetComponent<MissionEditor>().GetMissionItemData(missionItem);
        //然后激活missionEditor
        missionEditor.SetActive(true);
    }


    
    
}
