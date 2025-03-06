using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    private const string KEY_AUTOSAVE = "GAMESAVE.gd";
    private GameModel _gameModel;
    public GameManager gameManager;
    public GameObject Fetus;


    private void Start()
    {
        _gameModel = Reference.GameModel;
        gameManager = GameManager.instance;
        // _gameModel.LoadGame.Subscribe(LoadGame);
        _gameModel.SaveGame.Subscribe(ApplySaveData);
        // gameManager.coin.Subscribe(AddSaveCoin);
        LoadGame(true);
    }

    public void LoadGame(bool init)
    {
        Debug.Log($"Load Game {Application.persistentDataPath}");
        var saveFilePath = Path.Combine(Application.persistentDataPath, KEY_AUTOSAVE);
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("Файл сохранения отсутствует.");
            // gameManager.coin.Value = 50000;
            // CustomerSystem.instance.InitStart(true);
            gameManager._mapController.InitStart(true);
            _gameModel.StageTutorial.ValueForce = 1;
            return;
        }

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
                    ApplyLoadData((SaveData) bf.Deserialize(fileStream));
                    Debug.Log("Load Game Completed");
                }
            }
            catch
            {
                Debug.Log("ERROR LOADING SAVE, IGNORING");
                // _gameModel.StageTutorial.ValueForce = 1;
            }
        }
        // gameManager._mapController.InitStart(false);
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
        List<PlantData> plantDatas = new List<PlantData>();
        foreach (var plantInAll in gameManager.allPlants)
        {
            plantDatas.Add(new PlantData(plantInAll.namePlant, plantInAll.Level, plantInAll.quantity.Value));
        }

        List<GrydkaData> rgydkaDatas = new List<GrydkaData>();

        foreach (var gradlaInCurrent in gameManager.currentGrydka)
        {
            rgydkaDatas.Add(new GrydkaData(gradlaInCurrent.plant.namePlant, gradlaInCurrent.StateOfGrowth,
                gradlaInCurrent.gameObject.transform.position.x, gradlaInCurrent.gameObject.transform.position.y));
        }

        List<FetusToSave> fetusToSaves = new List<FetusToSave>();

        foreach (var fet in Reference.AllFetus)
        {
            fetusToSaves.Add(new FetusToSave(fet.typePlant, fet.gameObject.transform.position.x,
                fet.gameObject.transform.position.y));
        }

        SaveData data = new SaveData()
        {
            Coin = gameManager.coin.Value,
            LevelGame = _gameModel.LevelGame.Value,
            // quantityCustomers = CustomerSystem.instance._quantityCustomersInLevel,
            // qq =  CustomerSystem.instance._qq,

            // currentCloseOrder = 4,
            // currentOpenOrder = 5,
            // currentMapIndex = 6,

            numberClosedOrders = Reference.GameModel.NumberClosedOrders.Value,
            currentCloseOrder = gameManager._mapController._currentCloseOrder,
            currentOpenOrder = gameManager._mapController._currentOpenOrder,
            currentMapIndex = gameManager._mapController._currentMapIndex,
            maxNumberPlants = Reference.GameModel.MaxNumberPlants.Value,

            plantDatasSave = new List<PlantData>(plantDatas),
            grydkaDatasSave = new List<GrydkaData>(rgydkaDatas),
            fetusToSaves = new List<FetusToSave>(fetusToSaves),
            GardenGnomePurchased = _gameModel.GardenGnome.Value,
            GardenGnomeLevel = Reference.GameModel.GardenGnomeLevel.Value,
            CollectorGnomePurchased = _gameModel.CollectorGnome.Value,
            CollectorGnomeLevel = Reference.GameModel.GardenGnomeLevel.Value,
            MusicHelpersPurchased = _gameModel.MusicHelpers.Value,
            MusicHelpersLevel = Reference.GameModel.MusicHelpersLevel.Value,
            StageTutorial = Reference.GameModel.StageTutorial.Value
        };
        // Debug.Log($"1-{data.numberClosedOrders}," +
        //           $"2-{data.currentOpenOrder}," +
        //           $"3-{data.currentCloseOrder}," +
        //           $"4-{data.currentMapIndex}," +
        //           $"5-{data.maxNumberPlants}");
        SaveGame(data);
    }

    private void ApplyLoadData(SaveData data)
    {
        // Debug.Log($"Load exp {data.NextLevelExperience} / lev {data.Level}");
        // gameManager.coin.Value = data.Coin;
        gameManager.coin.Value = data.Coin == 0 ? 0 : data.Coin;
        _gameModel.LevelGame.Value = data.LevelGame;
        // CustomerSystem.instance.InitStart(false, data.quantityCustomers, data.qq);

        Reference.GameModel.NumberClosedOrders.Value = data.numberClosedOrders;
        //
        gameManager._mapController._currentOpenOrder = data.currentOpenOrder;
        gameManager._mapController._currentCloseOrder = data.currentCloseOrder;
        gameManager._mapController._currentMapIndex = data.currentMapIndex;
        //
        Reference.GameModel.MaxNumberPlants.Value = data.maxNumberPlants;
        // Reference.GameModel.MaxNumberPlants.Value = 5;


        Debug.Log($"Plant test {data.plantDatasSave.Count}");
        foreach (var plantData in data.plantDatasSave)
        {
            var n = gameManager.allPlants.Find((plant => plant.namePlant.Contains(plantData.Name)));
            if (n != null)
            {
                n.Level = plantData.Level;
                n.quantity.Value = plantData.CountPlants;
            }
        }

        Debug.Log($"Grygka test {data.grydkaDatasSave.Count}");
        foreach (var grydkaLoad in data.grydkaDatasSave)
        {
            var p = gameManager.allPlants.Find((plant => plant.namePlant.Contains(grydkaLoad.NamePlant)));
            if (p != null)
            {
                gameManager.PlantAplantToLoad(new Vector2(grydkaLoad.x, grydkaLoad.y), p, grydkaLoad.LevelStage);
            }
        }

        foreach (var feti in data.fetusToSaves)
        {
            var f = gameManager.allPlants.Find((plant => plant.typePlant == feti.typePlant));
            if (f != null)
            {
                GameObject fet = Instantiate(Fetus, new Vector3(feti.Xpos, feti.Ypos, 0), Quaternion.identity);
                fet.GetComponent<Fetus>().typePlant = f.typePlant;
                fet.GetComponent<SpriteRenderer>().sprite = Texture2DToSprite(f.spritePlant[4]);
                fet.GetComponent<Fetus>().NonInteractive = true;
                Reference.AllFetus.Add(fet.GetComponent<Fetus>());
                // Reference.AllFetus.Add(fet.GetComponent<Fetus>());
            }
        }

        // Debug.Log($"Load Game currentGrydka {gameManager.currentGrydka.Count}");
        //For Test----------------------------------------------------
        _gameModel.GardenGnome.Value = data.GardenGnomePurchased;
        // _gameModel.GardenGnome.Value = false;
        //------------------------------------------------------------
        _gameModel.GardenGnomeLevel.Value.lvlSpeed = data.GardenGnomeLevel.lvlSpeed;
        _gameModel.GardenGnomeLevel.Value.lvlActions = data.GardenGnomeLevel.lvlActions;
        _gameModel.GardenGnomeLevel.Value.lvlStartAction = data.GardenGnomeLevel.lvlStartAction;

        _gameModel.CollectorGnome.Value = data.CollectorGnomePurchased;
        _gameModel.CollectorGnomeLevel.Value.lvlSpeed = data.CollectorGnomeLevel.lvlSpeed;
        _gameModel.CollectorGnomeLevel.Value.lvlActions = data.CollectorGnomeLevel.lvlActions;
        _gameModel.CollectorGnomeLevel.Value.lvlStartAction = data.CollectorGnomeLevel.lvlStartAction;

        _gameModel.MusicHelpers.Value = data.MusicHelpersPurchased;
        _gameModel.MusicHelpersLevel.Value.lvlSpeed = data.MusicHelpersLevel.lvlSpeed;
        _gameModel.MusicHelpersLevel.Value.lvlActions = data.MusicHelpersLevel.lvlActions;
        _gameModel.MusicHelpersLevel.Value.lvlStartAction = data.MusicHelpersLevel.lvlStartAction;
        _gameModel.StageTutorial.Value = CurrentStageTutorial(data.StageTutorial);
        // _gameModel.StageTutorial.Value = 0;
        // _gameModel.LoadGameComplited.ValueForce = false;
        gameManager._mapController.InitStart(false);
        // gameManager._mapController.InitStart(false);
    }

    // private void ClearLoadGames(bool value)
    // {
    //     SaveData data = new SaveData()
    //     {
    //         Coin = gameManager.coin.Value,
    //         // Level = 0,
    //         // NextLevelExperience = 0,
    //         // NumberOpenTanks = 1,
    //         // QuantityKillMobs = 0,
    //         // NumberOpenPlanets = 0,
    //         // PlayerAllExperienceMeta = 0,
    //         // MetaUprgadeModel = new MetaUprgadeModel(),
    //         // NameSelectedTank = "Player.prefab"
    //         // Resources_1 = _gameModel.Resources.Value
    //     };
    //     SaveGame(data);
    // }

    Sprite Texture2DToSprite(Texture2D texture)
    {
        // Создание спрайта из Texture2D
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height), // Размеры спрайта
            new Vector2(0.5f, 0.5f) // Точка привязки (pivot), по умолчанию в центре
        );
    }

    private int CurrentStageTutorial(int stage)
    {
        if (stage < 5) return 1;
        // if (stage == 2 || stage == 3)
        if (stage > 4 && stage < 11) return 6;
        // if (stage > 10 && stage < 15) return 11;
        return stage;
    }
}

[Serializable]
public class SaveData
{
    public int Coin;

    public int LevelGame;
    // public int quantityCustomers;
    // public int qq;

    public int numberClosedOrders;
    public int currentCloseOrder;
    public int currentOpenOrder;
    public int currentMapIndex;
    public int maxNumberPlants;

    public List<PlantData> plantDatasSave;
    public List<GrydkaData> grydkaDatasSave;
    public List<FetusToSave> fetusToSaves;
    public bool GardenGnomePurchased;
    public LvlAssistance GardenGnomeLevel;
    public bool CollectorGnomePurchased;
    public LvlAssistance CollectorGnomeLevel;
    public bool MusicHelpersPurchased;
    public LvlAssistance MusicHelpersLevel;

    public int StageTutorial;
}

[Serializable]
public class PlantData
{
    public string Name;
    public int Level;
    public int CountPlants;

    public PlantData(string name, int level, int amount)
    {
        Name = name;
        Level = level;
        CountPlants = amount;
    }
}

[Serializable]
public class GrydkaData
{
    public string NamePlant;
    public int LevelStage;
    public float x;
    public float y;

    public GrydkaData(string name, int level, float X, float Y)
    {
        NamePlant = name;
        LevelStage = level;
        x = X;
        y = Y;
    }
}

[Serializable]
public class FetusToSave
{
    public ETypePlant typePlant;
    public float Xpos;
    public float Ypos;

    public FetusToSave(ETypePlant type, float x, float y)
    {
        typePlant = type;
        Xpos = x;
        Ypos = y;
    }
}