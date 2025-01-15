using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using N.Fridman.FormatNums.Scripts.Helpers;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GrydkaPrefab;
    public Transform ParentGrydka;
    public List<UpgradeGrydkaCfg> upgradeGrydkaCfgs;
    public static GameManager instance;
    public List<Plant> allPlants;
    public List<Grydka> currentGrydka;
    public List<LevelGrydka> levelGrydka;
    public List<Texture> spriteTexturesPlant;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textLevelGame;
    public SubscriptionField<int> coin;
    public GameObject PoPUpUpgrade;
    public GameModel gameModel;
    public UpgradeLevelUp UpgradeLevelUp;
    public GetTokensVFXController getTokensVFXController;
    public GameScoreHandler gameScoreHandler;
    public Transform CoinPos;
    [Header("Assistants")] public GameObject musicHelpers;
    public GameObject collectorGnome;
    public GameObject GardenGnome;
    public Image imageLeve;
    public float timeToPlant;
    private float _timer = 0;
    [HideInInspector] public CfgLevelData _cfgLevelData;
    private int _lastCoins;
    private MapController _mapController;
    public Image imageFoerstLevel;
    public TextMeshProUGUI MapUprgadeText;


    public List<Plant> openPlants;
    // public List<Customer> customers;

    private void Awake()
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
    }

    void Start()
    {
        _mapController = new MapController();
        _mapController.OnLevelChanged(0);
        _mapController.Init();
        // _mapController.CheckLevelMap(0);
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
        coin.Value = 50000;
        gameModel.LevelGame.Subscribe(ChangeLevelGame);
        gameModel.LevelGame.Value = 1;
        gameModel.NumberCompletedOrders.Subscribe(ChangeLevelUp);
        ChangeLevelGame(gameModel.LevelGame.Value);
        InitGnome();
        _timer = Time.time + timeToPlant;
    }

    private void Update()
    {
        // && 2 < Reference.GameModel.MaxNumberPlants.Value
        // Debug.Log($"currentGrydka {currentGrydka.Count}");
            if (_timer < Time.time && currentGrydka.Count < Reference.GameModel.MaxNumberPlants.Value)
            {
                _timer = Time.time + timeToPlant;
                PlantAplant(SpawnPositionPlant());
            }
    }

    private void InitGnome()
    {
        // gameModel.GardenGnome.Value = true;
        if (gameModel.GardenGnome.Value)
        {
            GardenGnome.SetActive(true);
        }
        else
        {
            gameModel.GardenGnome.Subscribe((c) =>
            {
                // Debug.Log($"GardenGnome on {c}");
                GardenGnome.SetActive(c);
            });
        }

        if (gameModel.CollectorGnome.Value)
        {
            collectorGnome.SetActive(true);
        }
        else
        {
            gameModel.CollectorGnome.Subscribe((c) => { collectorGnome.SetActive(c); });
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
        textCoin.text = FormatNumsHelper.FormatNum((float) newValue);
    }

    private void ChangeLevelGame(int newValue)
    {
        textLevelGame.text = newValue.ToString();
    }

    public void ChangeLevelUp(int number)
    {
        //TODO попап повышени уровня и пр.
        gameModel.LevelGame.Value++;
        gameModel.LevelMap.Value++;
        // Debug.Log($"Level Map: {gameModel.LevelMap.Value}");
        //TODO  сделать систему повышающую уровень карты
        // _mapController.OnLevelChanged(gameModel.LevelMap.Value);
        UpgradeLevelUp.gameObject.SetActive(true);
        UpgradeLevelUp.InitPanel(_cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1]);
    }

    public void LevelUpApply()
    {
        var typePlant = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1].OpenPlant;
        var plant = GetPlantToType(typePlant);
        coin.Value += _cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1].CoinReward;
        // Debug.Log($"Add Plant {plant.typePlant}/ level {gameModel.LevelGame.Value}");
        openPlants.Add(plant);
        // gameModel.NumberCompletedOrders.Value = 0; // TODO выркзать этот рудемент
        var u = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1].RewardLevelDataPlants[0];
        Bag.instance.AddPlants(u.RewardPlant, u.QuantityRewardPlant);
        if (_cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1].RewardLevelDataPlants.Count > 1)
        {
            var p = _cfgLevelData.AllLevelData[gameModel.LevelGame.Value - 1].RewardLevelDataPlants[0];
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
        float h = (float) ((float) value / (float) (all + 1f));
        // Debug.Log($"ShowAmoutExp {h}");
        imageLeve.DOFillAmount(h, 2);
    }

    public void PlantAplant(Vector2 pos)
    {
        var grydka = Instantiate(GrydkaPrefab, pos, quaternion.identity, ParentGrydka)
            .GetComponent<Grydka>();
        currentGrydka.Add(grydka);
        grydka.PlantaPlant();
    }

    public Vector2 SpawnPositionPlant()
    {
        Vector2 origin = new Vector2(0, -1);
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        float maxDistance = 100f;
        int targetLayerMask = LayerMask.GetMask("Bush");
        RaycastHit2D hit = Physics2D.Raycast(origin, randomDirection, maxDistance, targetLayerMask);
        if (hit != null)
        {
            float hitDistance = hit.distance;
            float randomDistance = Random.Range(0, hitDistance - 0.1f);
            return origin + randomDirection * randomDistance;
        }
        return Vector2.zero;
    }

    public void DestroyForest(Transform bush)
    {
        Destroy(bush.gameObject);
    }
}