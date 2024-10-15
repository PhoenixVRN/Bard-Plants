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

        // InitializeManager();
    }

    public void AddPlants(ETypePlant type, int value)
    {
        Debug.Log($"Added to Bag {type.ToString()}, count {value}");
        switch (type)
        {
            case ETypePlant.Starch_Nut:
                Reference.GameModel.StarchNut.Value += value;
                break;

            case ETypePlant.Mystical_Mushroom:
                Reference.GameModel.MysticalMushroom.Value += value;
                break;

            case ETypePlant.CrystalNut:
                Reference.GameModel.CrystalNut.Value += value;
                break;
            
            case ETypePlant.FlyEater:
                Reference.GameModel.FlyEater.Value += value;
                break;
            
            case ETypePlant.GutFlower:
                Reference.GameModel.GutFlower.Value += value;
                break;
            
            case ETypePlant.Mandrake:
                Reference.GameModel.Mandrake.Value += value;
                break;
            
            case ETypePlant.MiracleFruit:
                Reference.GameModel.MiracleFruit.Value += value;
                break;
            
            case ETypePlant.NeedleFlower:
                Reference.GameModel.NeedleFlower.Value += value;
                break;
            
            case ETypePlant.StaringFlower:
                Reference.GameModel.StaringFlower.Value += value;
                break;
            
            case ETypePlant.ToxicMushroom:
                Reference.GameModel.ToxicMushroom.Value += value;
                break;
            
            case ETypePlant.BushTentacles:
                Reference.GameModel.BushTentacles.Value += value;
                break;
            
            case ETypePlant.StarFruit:
                Reference.GameModel.StarFruit.Value += value;
                break;
            
            
        }
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