using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Grydka : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spriteGrydka;
    public RawImage plantunGrydka;
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

    private void Start()
    {
        _gameManager = GameManager.instance;
        levelGrydka = 1;
    }

    void Update()
    {
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
                if (StateOfGrowth == 4 ) return;
                // if (StateOfGrowth == 5) Debug.Log($"StateOfGrowth 5");
                plantunGrydka.texture = plant.spritePlant[StateOfGrowth];
            }
        }
    }

    public void PlantaPlant()
    {
        if (uprgadePopUpActive) return;
        empty = true;
        StateOfGrowth = 0;
        timeGrowthInStage = Time.time;
        Growth = true;
        plant = GameManager.instance.allPlants[Random.Range(0, GameManager.instance.openPlants.Count)];
        Debug.Log($"PlantaPlant {GameManager.instance.openPlants.Count}/{plant.namePlant}");
        plantunGrydka.texture = plant.spritePlant[0];
        plantunGrydka.gameObject.SetActive(true);
    }

    public void TapGrydka()
    {
        Debug.Log($"TapGrydka");
    }

    private void OnMouseDown()
    {
        // if (StateOfGrowth == 4)
        // {
        //     StateOfGrowth = 0;
        //     plantunGrydka.transform.SetParent(Bag.instance.transform);
        //     plantunGrydka.transform.DOMove(Bag.instance.transform.position, 1).SetEase(Ease.Linear).OnComplete(AddedPlodToBag);
        // }

        // Debug.Log($"Tap in {gameObject}");
    }

    public void Harvesting()
    {
        if (StateOfGrowth == 4)
        {
            StateOfGrowth = 0;
            ripe = false;
            plantunGrydka.texture = plant.spritePlant[4];
            plantunGrydka.transform.SetParent(Bag.instance.transform);
            plantunGrydka.transform.DOMove(Bag.instance.transform.position, 1).SetEase(Ease.Linear).OnComplete(AddedPlodToBag);
            // AddedPlodToBag();
        }

        Debug.Log($"Harvesting {gameObject}");
    }

    public void PlayMusic()
    {
        needPlayMusic.SetActive(false);
        // needPlayMusic.texture = noplayMusic.texture;
        StateOfGrowth = 3;
        plantunGrydka.texture = plant.spritePlant[StateOfGrowth];
        timeGrowthInStage = Time.time;
        needMusic = false;
    }
    private void AddedPlodToBag()
    {
        empty = false;
        plantunGrydka.gameObject.SetActive(false);
        plantunGrydka.transform.SetParent(transform);
        plantunGrydka.transform.localPosition = new Vector3(0, 40, 0);
        var count = Random.Range(GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].minPlants,
            GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].maxPlants);
        Bag.instance.AddPlants(plant.typePlant, count);
        Debug.Log($"min {GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].minPlants}/max {GameManager.instance.upgradeGrydkaCfgs[levelGrydka - 1].maxPlants}/{count}");
    }

    public void UpgradeGrydka()
    {
        if (levelGrydka > 4 || empty)
        {
            //TODO max level
            return;
        }
        
        GameManager.instance.PoPUpUpgrade.SetActive(true);
        GameManager.instance.PoPUpUpgrade.GetComponent<UpgradePanel>().Init(levelGrydka, this);
        Debug.Log($"UptgadeGrydka");
    }

    public void ApplyUpgrade()
    {
        levelGrydka++;
        gameObject.GetComponent<RawImage>().texture = _spriteGrydka[levelGrydka - 1].texture;
    }
    
}

public enum ETypePlant
{
    None = 0,
    StarchNut = 1,
    MysticalMushroom = 2,
    CrystalNut = 3,
    FlyEater =4,
    GutFlower = 5,
    Mandrake = 6,
    MiracleFruit = 7,
    NeedleFlower = 8,
    StaringFlower = 9,
    ToxicMushroom = 10,
    BushTentacles = 11,
    StarFruit = 12
}

[Serializable]
public class Plant
{
    public string namePlant;
    public ETypePlant typePlant;
    public int timeGrowth;
    public int defaultValueDelivery;
    public List<Texture> spritePlant;

    public int QE;
   
    public SubscriptionField<int> quantity;
    public Plant()
    {
        quantity = new SubscriptionField<int>() {Value = 0};
    }
   
}