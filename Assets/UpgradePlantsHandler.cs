using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlantsHandler : MonoBehaviour
{
    public GameObject contener;
    public UpgradePlant upgradePlant;
    private List<GameObject> upgradePlants;

    private void Awake()
    {
        upgradePlants = new List<GameObject>();
    }

    private void OnEnable()
    {
        InitPlant();
    }

    private void InitPlant()
    {
        int countSlot = 0;
        foreach (var plant in GameManager.instance.openPlants)
        {
            var plantInit = Instantiate(upgradePlant, transform.position, Quaternion.identity, contener.transform);
            plantInit.GetComponent<UpgradePlant>().Init(plant);
            upgradePlants.Add(plantInit.gameObject);
        }
    }

    private void OnDisable()
    {
        upgradePlants.Clear();
    }
}
