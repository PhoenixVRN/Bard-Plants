using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
    private CfgLevelData _cfgLevelData;

    public List<Plant> openPlants;
    // public List<Customer> customers;

    void Start()
    {
        _cfgLevelData = GetComponent<CfgLevelData>();
        gameModel = Reference.GameModel;
        coin = new SubscriptionField<int>();
        coin.Value = 0;
        coin.Subscribe(ChangeCoins);
        gameModel.LevelGame.Subscribe(ChangeLevelGame);
        gameModel.NumberCompletedOrders.Subscribe(ChangeLevelUp);
        ChangeLevelGame(gameModel.LevelGame.Value);
        if (instance == null) { 
            instance = this; 
        } else if(instance == this)
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        CustomersSpawn();
        ChangeLevelUp(0);
        // InitializeManager();
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
        if (_cfgLevelData.AllLevelData[gameModel.LevelGame.Value].NumberCompletedOrders <= number)
        {
            var enamPlant = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value].OpenPlant;
            var plant = allPlants.Find(p => p.typePlant == enamPlant);
            coin.Value += _cfgLevelData.AllLevelData[gameModel.LevelGame.Value].CoinReward;
            Debug.Log($"Add Plant {plant.typePlant}");
            openPlants.Add(plant);
            gameModel.LevelGame.Value++;
            gameModel.NumberCompletedOrders.Value = 0;
            //TODO попап повышени уровня и пр.
        }
    }
    public void OpenPlants()
    {
        
    }

    public Plant GetPlantToType(ETypePlant typePlant)
    {
        return allPlants.Find(pl => pl.typePlant == typePlant);
    }
}
