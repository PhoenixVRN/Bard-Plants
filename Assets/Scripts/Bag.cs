using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public static Bag instance;
    public GameObject canvasOpenBag;
    private GameModel gameModel;

    public int starchNut;
    public int mysticalMushroom;
    public int CrystalNut;
    public int FlyEater;
    public int GutFlower;
    public int Mandrake;
    public int MiracleFruit;
    public int NeedleFlower;
    public int StaringFlower;
    public int ToxicMushroom;
    public int BushTentacles;
    public int StarFruit;

    private void Awake()
    {
        gameModel = new GameModel();
        Reference.GameModel = gameModel;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

       InitializeManager();
    }

    private void InitializeManager()
    {
        var startReward = GameManager.instance._cfgLevelData.AllLevelData[0].RewardLevelDataPlants[0];
        AddPlants(startReward.RewardPlant, startReward.QuantityRewardPlant);
    }

    public void AddPlants(ETypePlant type, int value)
    {
        // Debug.Log($"Added to Bag {type.ToString()}, count {value}");
        // var pl = GameManager.instance.allPlants.Find(pl => pl.typePlant == type);
        // Debug.Log($"pl {pl.namePlant}");
        // if (pl != null)
        // {
        //     pl.quantity.Value += value;
        //     Debug.Log($"AddPlants in bag{pl.namePlant}/{value}");
        // }
        GameManager.instance.allPlants.Find(pl => pl.typePlant == type).quantity.Value += value;
        // Debug.Log($"cristal {GameManager.instance.allPlants.Find(pl => pl.typePlant == ETypePlant.CrystalNut).quantity.Value}");
    }

    // private void OnMouseDown()
    // { Debug.Log($"OnMouseDown 1");
    //     if (Reference.GameModel.BagInteractive.Value)
    //     {
    //         Reference.GameModel.BagInteractive.Value = false;
    //         canvasOpenBag.SetActive(true);
    //     }
    // }

    public void BagInteractive()
    {
        Reference.GameModel.BagInteractive.Value = true;
    }
}