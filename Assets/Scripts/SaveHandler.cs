using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

public class SaveHandler : MonoBehaviour
{
    private const string KEY_AUTOSAVE = "GAMESAVE.gd";
    private GameModel _gameModel;
    public GameManager gameManager;


    private void Start()
    {
        _gameModel = Reference.GameModel;
        gameManager = GameManager.instance;
        _gameModel.LoadGame.Subscribe(LoadGame);
        _gameModel.SaveGame.Subscribe(ApplySaveData);
        // gameManager.coin.Subscribe(AddSaveCoin);
    }

    private void AddSaveCoin(int amount)
    {
        ApplySaveData(true);
    }
    // private MetaUprgadeModel _metaUprgadeModel;
    // internal SaveGameController(GameModel gameModel)
    // {
    //     // Debug.Log($"SaveGameController init");
    //     _gameModel = gameModel;
    //     _gameModel.SaveGame.Subscribe(ApplySaveData);
    //     _gameModel.LoadGame.Subscribe(LoadGame);
    //     _gameModel.ClearLoadGame.Subscribe(ClearLoadGames);
// #if !All_CHEAT
//             LoadGame(true);
//             #else
//             Debug.Log($"AllCheats");
//             Reference.GameModel.Resources.Value = 50000;
//             Reference.GameModel.Coins.Value = 50000; 
//             Reference.GameModel.PlayerAllExperienceMeta.Value = 100000;
//             Reference.GameModel.NumberOpenPlanets.Value = 5;
//             Reference.GameModel.CheatAllOpenPlanets.ValueForce = true;
// #endif
    // }

    public void LoadGame(bool init)
    {
        Debug.Log($"Load Game {Application.persistentDataPath}");
        foreach (string filePath in Directory.EnumerateFiles(Application.persistentDataPath, KEY_AUTOSAVE))
        {
            // Debug.Log("Game save found");
            // string filePath = Application.persistentDataPath + KEY_AUTOSAVE;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            try
            {
                using (fileStream)
                {
                    ApplyLoadData((SaveData)bf.Deserialize(fileStream));
                    Debug.Log("Load Game Completed");
                }
            }
            catch
            {
                Debug.Log("ERROR LOADING SAVE, IGNORING");
            }
        }
    }


    private void SaveGame(SaveData data)
    {
        Debug.Log("Save Game");
        string filePath = Application.persistentDataPath + "/" + KEY_AUTOSAVE;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);
        bf.Serialize(fileStream, data);
        fileStream.Close();
        // Debug.Log($"Save Game Completed {filePath}");
        // callback?.Invoke();
    }

    private void ApplySaveData(bool value)
    {
        Debug.Log($"Save Subscribe");
        SaveData data = new SaveData()
        {
            Coin = gameManager.coin.Value,
            // PlantDatas = new PlantData("111", 5)
            // TestList = new List<Plant>(){new Plant()}
            // Grydkas = new List<Grydka>(gameManager.currentGrydka)
            // Level = _gameModel.PlayerLevelMeta.Value,
            // NextLevelExperience = _gameModel.PlayerExperienceMeta.Value,
            // NumberOpenTanks = _gameModel.NumberOpenTanks.Value,
            // QuantityKillMobs = _gameModel.QuantityKillMobs.Value,
            // NumberOpenPlanets = _gameModel.NumberOpenPlanets.Value,
            // PlayerAllExperienceMeta = _gameModel.PlayerAllExperienceMeta.Value,
            // MetaUprgadeModel = Reference.MetaUprgadeModel,
            // NameSelectedTank = Reference.NameTankPrefab

            // Resources_1 = _gameModel.Resources.Value
        };
        // Debug.Log($"AddMetaUpgradeModel save {data.MetaUprgadeModel.AllWeaponDamage.lvl}");
        SaveGame(data);
    }

    private void ClearLoadGames(bool value)
    {
        SaveData data = new SaveData()
        {
            Coin = gameManager.coin.Value,
            // Level = 0,
            // NextLevelExperience = 0,
            // NumberOpenTanks = 1,
            // QuantityKillMobs = 0,
            // NumberOpenPlanets = 0,
            // PlayerAllExperienceMeta = 0,
            // MetaUprgadeModel = new MetaUprgadeModel(),
            // NameSelectedTank = "Player.prefab"
            // Resources_1 = _gameModel.Resources.Value
        };
        SaveGame(data);
    }

    private void ApplyLoadData(SaveData data)
    {
        // Debug.Log($"Load exp {data.NextLevelExperience} / lev {data.Level}");
        gameManager.coin.Value = data.Coin;
        // Debug.Log($"Plant test {data.PlantDatas.Name}");
         // if (data.Grydkas != null)
         // {
         //     gameManager.currentGrydka = new List<Grydka>(data.Grydkas);
         // }
         

        Debug.Log($"Load Game currentGrydka {gameManager.currentGrydka.Count}");
        // _gameModel.PlayerLevelMeta.Value = data.Level;
        // _gameModel.PlayerExperienceMeta.Value = data.NextLevelExperience;
        // _gameModel.NumberOpenTanks.Value = data.NumberOpenTanks;
        // _gameModel.QuantityKillMobs.Value = data.QuantityKillMobs;
        // _gameModel.NumberOpenPlanets.Value = data.NumberOpenPlanets;
        // _gameModel.PlayerAllExperienceMeta.Value = data.PlayerAllExperienceMeta;
        // Reference.NameTankPrefab = data.NameSelectedTank;
        // Debug.Log($"NameSelectedTank {data.NameSelectedTank}");
        // Debug.Log($"AddMetaUpgradeModel load {data.MetaUprgadeModel.AllWeaponDamage.lvl}");
        // if (data.MetaUprgadeModel != null)
        // {
        //     // Reference.AddMetaUpgradeModel(data.MetaUprgadeModel);
        // }
        // _gameModel.Resources.Value = data.Resources_1;
    }
}

[Serializable]
public class SaveData
{
    public int Coin;

    // public PlantData PlantDatas;
    // public List<Grydka> Grydkas = new List<Grydka>(){new Grydka(),new Grydka()};
    // public List<Plant> TestList;
    public int HardCoin;
    public int Level;
    public int NextLevelExperience;
    public int NumberOpenTanks;
    public int NumberOpenPlanets;
    public int QuantityKillMobs;
    public int PlayerAllExperienceMeta;
    public int Resources_1;
    public int Resources_2;
    public int Resources_3;
    public int Resources_4;

    public int Resources_5;

    // public MetaUprgadeModel MetaUprgadeModel;
    public string NameSelectedTank;
}

[Serializable]
public class PlantData
{
    public string Name;
    public int CountPlants;

    public PlantData(string name, int amount)
    {
        Name = name;
        CountPlants = amount;
    }
}