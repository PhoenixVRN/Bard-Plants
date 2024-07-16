using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Grydka : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spriteGrydka;
    [SerializeField] private List<Plant> _allPlants;
    public RawImage plantunGrydka;
    public GameObject needPlayMusic;
    public Sprite playMusic;
    public Sprite noplayMusic;
    public Plant plant;
    private int StateOfGrowth;
    public float GrowthAccelerator;
    private bool Growth;
    public bool needMusic;
    private float timeGrowthInStage;
    public bool empty;
    public bool ripe;
    public int levelGrydka;

    private void Start()
    {
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
                plantunGrydka.texture = plant.spritePlant[StateOfGrowth];
            }
        }
    }

    public void PlantaPlant()
    {
        empty = true;
        StateOfGrowth = 0;
        timeGrowthInStage = Time.time;
        Growth = true;
        plant = _allPlants[Random.Range(0, _allPlants.Count)];
        plantunGrydka.texture = plant.spritePlant[0];
        plantunGrydka.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (StateOfGrowth == 4)
        {
            StateOfGrowth = 0;
            plantunGrydka.transform.SetParent(Bag.instance.transform);
            plantunGrydka.transform.DOMove(Bag.instance.transform.position, 1).SetEase(Ease.Linear).OnComplete(Test);
        }

        Debug.Log($"Tap in {gameObject}");
    }

    public void Harvesting()
    {
        if (StateOfGrowth == 4)
        {
            StateOfGrowth = 0;
            ripe = false;
            plantunGrydka.transform.SetParent(Bag.instance.transform);
            plantunGrydka.transform.DOMove(Bag.instance.transform.position, 1).SetEase(Ease.Linear).OnComplete(Test);
        }

        Debug.Log($"Harvesting {gameObject}");
    }

    public void PlayMusic()
    {
        needPlayMusic.SetActive(false);
        // needPlayMusic.texture = noplayMusic.texture;
        StateOfGrowth = 3;
        timeGrowthInStage = Time.time;
        needMusic = false;
    }
    private void Test()
    {
        empty = false;
        plantunGrydka.gameObject.SetActive(false);
        plantunGrydka.transform.SetParent(transform);
        plantunGrydka.transform.localPosition = Vector3.zero;
        Bag.instance.AddPlants(plant.typePlant);
    }

    public void UpgradeGrydka()
    {
        if (levelGrydka > 4)
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
    Starch_Nut = 1,
    Mystical_Mushroom = 2,
    CrystalNut = 3
}

[Serializable]
public class Plant
{
    public string namePlant;
    public ETypePlant typePlant;
    public int timeGrowth;
    public List<Texture> spritePlant;
}