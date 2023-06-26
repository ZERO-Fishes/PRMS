using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject StatusUI;
    public GameObject MissionUI;
    public GameObject SettingsUI;
    public static UIManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }

    public void EnableStatusUI()
    {
        StatusUI.SetActive(true);
        MissionUI.SetActive(false);
        SettingsUI.SetActive(false);
    }
    public void EnableMissionUI()
    {
        StatusUI.SetActive(false);
        MissionUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
    public void EnableSettingsUI()
    {
        StatusUI.SetActive(false);
        MissionUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
}
