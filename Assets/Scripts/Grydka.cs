using System;
using System.Collections.Generic;
using AudioSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Grydka : MonoBehaviour
{
    public SpriteRenderer plantunGrydka;
    public GameObject needPlayMusic;
    public Sprite playMusic;
    public Sprite noplayMusic;
    public Plant plant;
    public int StateOfGrowth;
    public float GrowthAccelerator;
    private bool Growth;
    public bool needMusic;
    private float timeGrowthInStage;
    public bool empty;
    public bool ripe;
    public int levelGrydka;
    public bool uprgadePopUpActive;
    private GameManager _gameManager;
    private bool _playerOn;
    public GameObject fetus;
    public ESound DropFrutsSound;
    public event Action DestroyGrydka;
    public event Action MusicOff;

    private void Start()
    {
        _gameManager = GameManager.instance;
        levelGrydka = 1;
    }

    private void OnDestroy()
    {
        DestroyGrydka?.Invoke();
    }

    void Update()
    {
        if (empty && needMusic && _playerOn)
        {
            PlayMusic();
        }

        if (empty && !Growth && _playerOn)
        {
            Harvesting();
        }

        if (Growth && !needMusic)
        {
            if (StateOfGrowth == 2)
            {
                needPlayMusic.SetActive(true);
                // needPlayMusic.texture = playMusic.texture;
                needMusic = true;
            }

            if (StateOfGrowth == 4)
            {
                Growth = false;
                ripe = true;
                //TODO евент что созрели
                return;
            }

            if (timeGrowthInStage + plant.timeGrowth < Time.time)
            {
                StateOfGrowth++;
                timeGrowthInStage = Time.time;
                // if (StateOfGrowth == 4 ) return;
                // if (StateOfGrowth == 5) Debug.Log($"StateOfGrowth 5");
                plantunGrydka.sprite = StateOfGrowth == 4
                    ? plantunGrydka.sprite = ConvertTextureToSprite(plant.spritePlant[3])
                    : plantunGrydka.sprite = ConvertTextureToSprite(plant.spritePlant[StateOfGrowth]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Player"))
        {
            _playerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Player"))
        {
            _playerOn = false;
        }
    }


    public void PlantaPlant()
    {
        if (uprgadePopUpActive) return;
        empty = true;
        StateOfGrowth = 0;
        timeGrowthInStage = Time.time;
        Growth = true;
        plant = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)];
        // Debug.Log($"PlantaPlant {GameManager.instance.openPlants.Count}/{plant.namePlant}");
        // plantunGrydka.GetComponent<CircleCollider2D>().enabled = true;
        plantunGrydka.sprite = ConvertTextureToSprite(plant.spritePlant[0]);
        plantunGrydka.gameObject.SetActive(true);
    }

    public void PlantaPlantToLoad(Plant plantLoad, int level)
    {
        empty = true;
        StateOfGrowth = level;
        timeGrowthInStage = Time.time;
        Growth = true;
        plant = plantLoad;
        plantunGrydka.sprite = StateOfGrowth == 4
            ? ConvertTextureToSprite(plant.spritePlant[3])
            : ConvertTextureToSprite(plant.spritePlant[StateOfGrowth]);
        plantunGrydka.gameObject.SetActive(true);
    }

    private Sprite ConvertTextureToSprite(Texture2D texture)
    {
        // Создаем спрайт на основе текстуры
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height), // Полный размер текстуры
            new Vector2(0.5f, 0.5f), // Точка привязки (pivot), по умолчанию центр
            100.0f // Пиксели на единицу (PPU)
        );
    }

    public void TapGrydka()
    {
        Debug.Log($"TapGrydka");
    }

    public void Harvesting()
    {
        if (StateOfGrowth == 4)
        {
            StateOfGrowth = 0;
            ripe = false;
            plantunGrydka.gameObject.SetActive(false);
            var count = (plant.Level+2); // for test *3

            for (int i = 0; i < count; i++)
            {
                GameObject fet = Instantiate(fetus, transform.position, Quaternion.identity);
                fet.GetComponent<Fetus>().typePlant = plant.typePlant;
                fet.GetComponent<SpriteRenderer>().sprite = Texture2DToSprite(plant.spritePlant[4]);

                fet.transform
                    .DOMove(transform.position + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0),
                        0.5f).OnComplete(() => { fet.GetComponent<Fetus>().NonInteractive = true; });
                Reference.AllFetus.Add(fet.GetComponent<Fetus>());
                empty = false;
            }

            AudioManagerView.Instance.PlaySound(DropFrutsSound);
        }

        _gameManager.currentGrydka.Remove(this);
        Destroy(gameObject);
    }

    Sprite Texture2DToSprite(Texture2D texture)
    {
        // Создание спрайта из Texture2D
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height), // Размеры спрайта
            new Vector2(0.5f, 0.5f) // Точка привязки (pivot), по умолчанию в центре
        );
    }

    public void PlayMusic()
    {
        MusicOff?.Invoke();
        needPlayMusic.SetActive(false);
        // needPlayMusic.texture = noplayMusic.texture;
        StateOfGrowth = 3;
        // plantunGrydka.texture = plant.spritePlant[StateOfGrowth];
        timeGrowthInStage = Time.time;
        needMusic = false;
    }

    private void AddedPlodToBag()
    {
        empty = false;
        plantunGrydka.gameObject.SetActive(false);
        plantunGrydka.transform.SetParent(transform);
        plantunGrydka.transform.localPosition = new Vector3(0, 40, 0);
        // plantunGrydka.GetComponent<CircleCollider2D>().enabled = false; // хз
        // var count = Random.Range(GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].minPlants,
        //     GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].maxPlants);
        var count = plant.Level + 10;
        Bag.instance.AddPlants(plant.typePlant, count);
        // Debug.Log(
        //     $"min {GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].minPlants}/max {GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].maxPlants}/{count}");
    }

    public void UpgradeGrydka()
    {
        // if (levelGrydka > 4 || empty)
        // {
        //     //TODO max level
        //     return;
        // }
        //
        // GameManager.instance.PoPUpUpgrade.SetActive(true);
        // GameManager.instance.PoPUpUpgrade.GetComponent<UpgradePanel>().Init(levelGrydka, this);
        // Debug.Log($"UptgadeGrydka");
    }
}

public enum ETypePlant
{
    None = 0,
    StarchNut = 1,
    MysticalMushroom = 2,
    CrystalNut = 3,
    FlyEater = 4,
    GutFlower = 5,
    Mandrake = 6,
    MiracleFruit = 7,
    NeedleFlower = 8,
    StaringFlower = 9,
    ToxicMushroom = 10,
    BushTentacles = 11,
    StarFruit = 12
}