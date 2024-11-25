using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<UpgradeGrydkaCfg> upgradeGrydkaCfgs;
    public static GameManager instance; 
    public List<Plant> allPlants;
    public List<Grydka> allGrydka;
    public List<Texture> spriteTexturesPlant;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textLevelGame;
    public SubscriptionField<int> coin;
    public GameObject PoPUpUpgrade;
    public Transform ParentGrydka;
    public GameModel gameModel;
    public UpgradeLevelUp UpgradeLevelUp;
    public GetTokensVFXController getTokensVFXController;
    public GameScoreHandler gameScoreHandler;
    public Transform CoinPos;
    [Header("Assistants")]
    public GameObject musicHelpers;
    public GameObject collectorGnome;
    public Image imageLeve;
    
    [HideInInspector] public CfgLevelData _cfgLevelData;
    private int _lastCoins;
    

    public List<Plant> openPlants;
    // public List<Customer> customers;

    private void Awake()
    {
        if (instance == null) { 
            instance = this; 
        } else if(instance == this)
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _cfgLevelData = GetComponent<CfgLevelData>();
        getTokensVFXController = GetComponent<GetTokensVFXController>();
        gameScoreHandler = GetComponent<GameScoreHandler>();
        gameModel = Reference.GameModel;
        coin = new SubscriptionField<int>();
        coin.Subscribe(ChangeCoins);
        DontDestroyOnLoad(gameObject);
        CustomersSpawn();
        // ChangeLevelUp(0);
        InitializeManager();
    }

    private void InitializeManager()
    {
        openPlants.Add(GetPlantToType(_cfgLevelData.AllLevelData[0].OpenPlant));
        // Bag.instance.AddPlants(ETypePlant.MysticalMushroom, 13);
        coin.Subscribe(ChangeCoins);
        coin.Value = 121;
        gameModel.LevelGame.Subscribe(ChangeLevelGame);
        gameModel.LevelGame.Value = 1;
        gameModel.NumberCompletedOrders.Subscribe(ChangeLevelUp);
        ChangeLevelGame(gameModel.LevelGame.Value);
        InitGnome();
    }

    private void InitGnome()
    {
        gameModel.GardenGnome.Value = true;
        if (gameModel.CollectorGnome.Value)
        {
            collectorGnome.SetActive(true);
        }
        else
        {
            gameModel.CollectorGnome.Subscribe((c) => collectorGnome.SetActive(c));
        }

        if (gameModel.MusicHelpers.Value)
        {
            musicHelpers.SetActive(true);
        }
        else
        {
            gameModel.MusicHelpers.Subscribe((c) => musicHelpers.SetActive(c));
        }
    }
    
    private void CustomersSpawn()
    {
        // customers[0].gameObject.SetActive(true);
    }

    private void ChangeCoins(int newValue)
    {
        textCoin.text = newValue.ToString();
    }
    
    private void ChangeLevelGame(int newValue)
    {
        textLevelGame.text = newValue.ToString();
    }

    public void ChangeLevelUp(int number)
    {
        //TODO попап повышени уровня и пр.
        gameModel.LevelGame.Value++;
        UpgradeLevelUp.gameObject.SetActive(true);
        UpgradeLevelUp.InitPanel(_cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1]);
    }

    public void LevelUpApply()
    {
        var typePlant = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value -1].OpenPlant;
        var plant = GetPlantToType(typePlant);
        coin.Value += _cfgLevelData.AllLevelData[gameModel.LevelGame.Value-1].CoinReward;
        Debug.Log($"Add Plant {plant.typePlant}/ level {gameModel.LevelGame.Value}");
        openPlants.Add(plant);
        // gameModel.NumberCompletedOrders.Value = 0; // TODO выркзать этот рудемент
        var u = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value-1].RewardLevelDataPlants[0];
        Bag.instance.AddPlants(u.RewardPlant, u.QuantityRewardPlant);
        if (_cfgLevelData.AllLevelData[gameModel.LevelGame.Value-1].RewardLevelDataPlants.Count >1)
        {
            var p = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value -1].RewardLevelDataPlants[0];
            Bag.instance.AddPlants(p.RewardPlant, p.QuantityRewardPlant);
        }
        Time.timeScale = 1f;
    }

    public Plant GetPlantToType(ETypePlant typePlant)
    {
        return allPlants.Find(pl => pl.typePlant == typePlant);
    }

    public void ShowAmoutExp(int all, int value)
    {
        float h = (float)((float)value / (float)(all+1f));
         Debug.Log($"ShowAmoutExp {h}");
        imageLeve.DOFillAmount(h, 2);
    }
}
