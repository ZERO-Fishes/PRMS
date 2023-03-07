using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MissionEditor : MonoBehaviour
{
    public EditorItem editorItemName;
    public EditorItem editorItemProfile;
    public EditorItem editorItemDeadLine;
    public EditorItem editorItemCounter;
    public EditorItem editorItemCountNum;
    
    public GameObject buttonCancel;
    public GameObject buttonConfirm;

    public MissionItem MissionItemData;

    ///加载Mission Item的数据后会显示在editor中
    public void GetMissionItemData(MissionItem missionItem)
    {
        MissionItemData = missionItem;
        editorItemName.LoadMissionItem(MissionItemData.missionName);
        editorItemProfile.LoadMissionItem(MissionItemData.missionProfile);
        editorItemDeadLine.LoadMissionItem(MissionItemData.missionDeadLine);
        editorItemCounter.LoadMissionItem(MissionItemData.missionCounter);
        editorItemCountNum.LoadMissionItem(MissionItemData.missionCountNum);
    }


    public void ConfirmAndSave()
    {
        //根据文本内容更新临时的MissionItem数据
        MissionItemData.missionName = editorItemName.textInput.text;
        MissionItemData.missionProfile = editorItemProfile.textInput.text;
        MissionItemData.missionDeadLine = editorItemDeadLine.textInput.text;
        int.TryParse(editorItemCounter.textInput.text,out MissionItemData.missionCounter);
        int.TryParse(editorItemCountNum.textInput.text,out MissionItemData.missionCountNum);
        GameEvents.current.BC_MissionEditorConfirm(MissionItemData);
        
    }
    public void CancelAndNoSave()
    {
        GameEvents.current.BC_MissionEditorCancel();
        
    }
}
