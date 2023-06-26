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
    public static MissionEditorManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }
    
    public GameObject missionEditor;
    private MissionItemData_SO editorItemData;
    private MissionItem missionItemObj;

    


    //接受ItemManager数据，打开并刷新Editor，供Item Manager调用
    public void ReceiveItemAndOpenEditor(MissionItemData_SO missionItemDataSo,MissionItem missionItemObj)
    {
        Instance.editorItemData = missionItemDataSo;//接受数据
        Instance.missionItemObj = missionItemObj;//接受Item对象(可行性未知)
        //用数据初始化Editor窗口
        OpenMissionEditor(Instance.editorItemData);

    }

    public void CloseEditorAndSaveItem()//供按钮按下调用
    {
        SaveMissionEditor(Instance.editorItemData);//读取窗口数据并保存
        missionEditor.gameObject.SetActive(false);//关闭窗口
        SaveManager.Instance.Save(Instance.editorItemData,Instance.editorItemData.missionItemID.ToString());
        //刷新Item实例
        Instance.editorItemData.NeedToUpdate = true;
    }

    public void CloseEditorAndDeleteItem()//供按下按钮调用
    {
        MissionItemManager.Instance.DestroyMissionItem(missionItemObj);
        AppriseManager.Instance.NTC_DeleteItem();//通知
        missionEditor.gameObject.SetActive(false);//关闭窗口
        
    }
    
    
    
    
    private void OpenMissionEditor(MissionItemData_SO editorItemData)//用来操作数据
    {
        //更新编辑器显示数据
        missionEditor.GetComponent<MissionEditor>().updateMissionEditor(editorItemData);
        missionEditor.SetActive(true);//开启editor
        
    }

    private void SaveMissionEditor(MissionItemData_SO editorItemData)//用来操作数据
    {
        missionEditor.GetComponent<MissionEditor>().updateEditorItemData(editorItemData);
    }
    


}
