using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }
    public TextMeshProUGUI textLevel;
    
    public TextMeshProUGUI textTagName1;
    public TextMeshProUGUI textTagValue1;
    
    public TextMeshProUGUI textTagName2;
    public TextMeshProUGUI textTagValue2;
    
    public TextMeshProUGUI textTagName3;
    public TextMeshProUGUI textTagValue3;
    
    public TextMeshProUGUI textTagName4;
    public TextMeshProUGUI textTagValue4;
    
    public TextMeshProUGUI textTagName5;
    public TextMeshProUGUI textTagValue5;
    
    public TextMeshProUGUI textDiomond;
    
    public StatusData_SO statusDataSo;
    public StatusData_SO statusDataSo_Temp;

    private void Start()
    {
        LoadStatusData();
        UpdateTexts();
    }

    public void ClearStatusData()
    {
        var json = JsonUtility.ToJson(Instance.statusDataSo_Temp);
        JsonUtility.FromJsonOverwrite(json,Instance.statusDataSo);
        UpdateTexts();
        SaveStatusData();
        AppriseManager.Instance.NTC_ResetProperty();
    }

    public void LoadStatusData()
    {
        SaveManager.Instance.Load(Instance.statusDataSo,"StatusData");
        UpdateTexts();
    }
    public void SaveStatusData()
    {
        SaveManager.Instance.Save(Instance.statusDataSo,"StatusData");
    }

    public void GetMissionItemAwards(MissionItemData_SO missionItemDataSo)//用于计算任务完成的奖励
    {
        statusDataSo.Exp += missionItemDataSo.missionExp;
        switch (missionItemDataSo.missionAwardType)
        {
            case 1:
            {
                statusDataSo.TagValue_1 += missionItemDataSo.missionAwardNum;
                AppriseManager.Instance.NTC_AddProperty(statusDataSo.TagName_1,missionItemDataSo.missionAwardNum,statusDataSo.TagValue_1);
                break;
            }
            case 2:
            {
                statusDataSo.TagValue_2 += missionItemDataSo.missionAwardNum;
                AppriseManager.Instance.NTC_AddProperty(statusDataSo.TagName_2,missionItemDataSo.missionAwardNum,statusDataSo.TagValue_2);
                break;
            }
            case 3:
            {
                statusDataSo.TagValue_3 += missionItemDataSo.missionAwardNum;
                AppriseManager.Instance.NTC_AddProperty(statusDataSo.TagName_3,missionItemDataSo.missionAwardNum,statusDataSo.TagValue_3);
                break;
            }
            case 4:
            {
                statusDataSo.TagValue_4 += missionItemDataSo.missionAwardNum;
                AppriseManager.Instance.NTC_AddProperty(statusDataSo.TagName_4,missionItemDataSo.missionAwardNum,statusDataSo.TagValue_4);
                break;
            }
            case 5:
            {
                statusDataSo.TagValue_5 += missionItemDataSo.missionAwardNum;
                AppriseManager.Instance.NTC_AddProperty(statusDataSo.TagName_5,missionItemDataSo.missionAwardNum,statusDataSo.TagValue_5);
                break;
            }
        }
        statusDataSo.DiamondCount += missionItemDataSo.missionDiamond;
        SaveStatusData();
        UpdateTexts();//更新文本
    }

    public void UpdateTexts()
    {
        long LV = statusDataSo.Exp / 100;
        long nowEXP = statusDataSo.Exp % 100;
        textLevel.text = "LV" + LV + "   " + nowEXP + "/100";
        textTagName1.text=statusDataSo.TagName_1;
        textTagValue1.text = statusDataSo.TagValue_1.ToString();
        textTagName2.text=statusDataSo.TagName_2;
        textTagValue2.text = statusDataSo.TagValue_2.ToString();
        textTagName3.text=statusDataSo.TagName_3;
        textTagValue3.text = statusDataSo.TagValue_3.ToString();
        textTagName4.text=statusDataSo.TagName_4;
        textTagValue4.text = statusDataSo.TagValue_4.ToString();
        textTagName5.text=statusDataSo.TagName_5;
        textTagValue5.text = statusDataSo.TagValue_5.ToString();
        textDiomond.text=statusDataSo.DiamondCount.ToString();
    }
}
