using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class AppriseManager : MonoBehaviour
{
    public GameObject AppriseCanvas;
    public TextMeshProUGUI textApprise;
    private bool AppriseON;
    
    public static AppriseManager Instance;
    void Awake(){
        if(Instance!=null)
            Destroy(gameObject);
        Instance=this;
    }

    private void Update()
    {
    }

    private void EnableApprise()
    {
        AppriseCanvas.SetActive(true);
        Invoke(nameof(DisableApprise),3);
    }
    private void DisableApprise()
    {
        AppriseCanvas.SetActive(false);
    }

    /// <summary>
    /// 通知：获得属性值
    /// </summary>
    /// <param name="PropertyName">要添加的属性名</param>
    /// <param name="PropertyAddValue">要添加的值</param>
    /// <param name="PropertyValue">属性添加后值</param>
    public void NTC_AddProperty(string PropertyName, int PropertyAddValue,long PropertyValue)
    {
        textApprise.text = PropertyName+":"+PropertyValue.ToString() + "(+" +PropertyAddValue.ToString()+")";
        EnableApprise();
    }

    public void NTC_DeleteItem()
    {
        textApprise.text = "删除任务成功";
        EnableApprise();
    }

    public void NTC_NewDay()
    {
        textApprise.text = "新的一天，数据已更新";
        EnableApprise();
    }

    public void NTC_ResetProperty()
    {
        textApprise.text = "已重置属性值";
        EnableApprise();
    }
    
}
