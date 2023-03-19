using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New StatusData",menuName = "PRMS/StatusData")]
public class StatusData_SO : ScriptableObject
{
    public long Exp;
    public string TagName_1;
    public long TagValue_1;
    public string TagName_2;
    public long TagValue_2;
    public string TagName_3;
    public long TagValue_3;
    public string TagName_4;
    public long TagValue_4;
    public string TagName_5;
    public long TagValue_5;
    public int DiamondCount;
}
