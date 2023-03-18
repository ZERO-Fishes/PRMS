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


    public void updateMissionEditor(MissionItemData_SO missionItemDataSo)
    {
        editorItemName.LoadMissionItem(missionItemDataSo.missionName);
        editorItemProfile.LoadMissionItem(missionItemDataSo.missionProfile);
        editorItemDeadLine.LoadMissionItem(missionItemDataSo.missionDeadLine);
        editorItemCounter.LoadMissionItem(missionItemDataSo.missionCounter);
        editorItemCountNum.LoadMissionItem(missionItemDataSo.missionCountNum);
    }

    public void updateEditorItemData(MissionItemData_SO editorItemData)
    {
        editorItemData.missionName = editorItemName.textInput.text;
        editorItemData.missionProfile = editorItemProfile.textInput.text;
        editorItemData.missionDeadLine = editorItemDeadLine.textInput.text;
        int temp;
        int.TryParse(editorItemCounter.textInput.text,out temp);
        editorItemData.missionCounter = temp;
        int.TryParse(editorItemCountNum.textInput.text,out temp);
        editorItemData.missionCountNum = temp;
    }


    /*public void ConfirmAndSave()
    {
        //根据文本内容更新临时的MissionItem数据
        MissionItemData.MissionName = editorItemName.textInput.text;
        MissionItemData.MissionProfile = editorItemProfile.textInput.text;
        MissionItemData.MissionDeadLine = editorItemDeadLine.textInput.text;
        int temp;
        int.TryParse(editorItemCounter.textInput.text,out temp);
        MissionItemData.MissionCounter = temp;
        int.TryParse(editorItemCountNum.textInput.text,out temp);
        MissionItemData.MissionCountNum = temp;
        GameEvents.current.BC_MissionEditorConfirm(MissionItemData);
        
    }*/
    /*public void CancelAndNoSave()
    {
        GameEvents.current.BC_MissionEditorCancel();
    }*/
}
