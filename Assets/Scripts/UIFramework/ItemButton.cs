using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemButton : MonoBehaviour
{
    public MissionItem _missionItem;
    public void SendEvent()
    {
        //执行事件动作
        GameEvents.current.BC_MissionItemClick(_missionItem);
    }
}
