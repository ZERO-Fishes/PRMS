using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorItem : MonoBehaviour
{
    public TMP_InputField textInput;


    ///显示Mission Item中传来的string数据
    public void LoadMissionItem(string missionItemString)
    {
        textInput.text = missionItemString;
    }
    ///显示Mission Item中传来的int数据
    public void LoadMissionItem(int missionItemInt)
    {

        textInput.text = missionItemInt.ToString();
    }
    public void LoadMissionItem(long missionItemInt)
    {

        textInput.text = missionItemInt.ToString();
    }
    
}
