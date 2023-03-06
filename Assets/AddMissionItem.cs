using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMissionItem : MonoBehaviour
{
    public GameObject MissionItemPrefab;
    public Transform CatalogToAdd;

    public void CreateMissionItem()
    {
        GameObject newGameObject = Instantiate(MissionItemPrefab, CatalogToAdd);
    }
}
