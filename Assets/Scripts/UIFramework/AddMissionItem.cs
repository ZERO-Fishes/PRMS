using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddMissionItem : MonoBehaviour
{
    private long maxMissionItemID;
    public GameObject MissionItemPrefab;
    public Transform CatalogToAdd;

    public void CreateMissionItem()
    {
        GameObject newGameObject = Instantiate(MissionItemPrefab, CatalogToAdd);
        
    }
}
