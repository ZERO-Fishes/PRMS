using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }
    //保存已有的MissionItem的数量
    public long MissionItemCount;
    public List<long> MissionItemIDArray;

    public void Save(Object data,string key)
    {
        var jsonData = JsonUtility.ToJson(data);//转换成json字符串
        PlayerPrefs.SetString(key,jsonData);//以键值对形式存储
        PlayerPrefs.Save();
    }

    public void Load(Object data,string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
             JsonUtility.FromJsonOverwrite( PlayerPrefs.GetString(key),data);//从json中读取数据并写入
        }
    }

    public void Delete(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public void DeleteAllItem()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("清除所有数据");
        StatusManager.Instance.ClearStatusData();
    }

    public void LoadLocalDatas()
    {

        MissionItemManager.Instance.LoadItemDataBank();
        MissionItemManager.Instance.InitializeMissionArea();
    }
}
