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
    public int plant3;
    public int plant4;
    public int plant5;
    public int plant6;
    public int plant7;
    public int plant8;
    public int plant9;

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

    public void AddPlants(ETypePlant type)
    {
        switch (type)
        {
            case ETypePlant.Starch_Nut:
                Reference.GameModel.StarchNut.Value++;
                break;

            case ETypePlant.Mystical_Mushroom:
                Reference.GameModel.MysticalMushroom.Value++;
                break;

            case ETypePlant.CrystalNut:
                Reference.GameModel.CrystalNut.Value++;
                break;
        }
    }

    private void OnMouseDown()
    {
        if (Reference.GameModel.BagInteractive.Value)
        {
            canvasOpenBag.SetActive(true);
        }
    }

    void Update()
    {
    }
}