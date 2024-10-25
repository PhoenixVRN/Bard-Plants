using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public RawImage plant;
    public TextMeshProUGUI textOrder;
    public GameObject completeOrderTagl;
    public GameObject completeOrderFon;
    public ETypePlant typePlant;
    public int needplant;
    public int collected;
    public bool completed;

    void Start()
    {
    }


    void Update()
    {
        if (collected >= needplant)
        {
            // Debug.Log($"Completed");
            completed = true;
            completeOrderFon.SetActive(true);
            completeOrderTagl.SetActive(true);
        }
        else
        {
            completed = false;
            completeOrderFon.SetActive(false);
            completeOrderTagl.SetActive(false);
        }
    }

    public void InitOrder(ETypePlant typePlant, int needCount)
    {
        needplant = needCount;
        this.typePlant = typePlant;
        plant.texture = GameManager.instance.allPlants.Find(plant1 => plant1.typePlant == typePlant).spritePlant[4];
        // switch (typePlant)
        // {
        //     case ETypePlant.StarchNut:
        //         plant.texture = GameManager.instance.spriteTexturesPlant[0];
        //         break;
        //
        //     case ETypePlant.MysticalMushroom:
        //         plant.texture = GameManager.instance.spriteTexturesPlant[1];
        //         break;
        //     
        //     case ETypePlant.CrystalNut:
        //         plant.texture = GameManager.instance.spriteTexturesPlant[2];
        //         break;
        // }

        completeOrderFon.SetActive(false);
        completeOrderTagl.SetActive(false);
        // Reference.GameModel.StarchNut.Subscribe(ShowText);
        CountText();
    }


    public void CountText()
    {
        var plant = GameManager.instance.allPlants.Find(pl => pl.typePlant == typePlant);
        plant.quantity.Subscribe(ShowText);
        ShowText(plant.quantity.Value);
        // switch (typePlant)
        // {
        //     case ETypePlant.StarchNut:
        //         // count = Bag.instance.starchNut;
        //         Reference.GameModel.StarchNut.Subscribe(ShowText);
        //         ShowText(Reference.GameModel.StarchNut.Value);
        //         break;
        //
        //     case ETypePlant.MysticalMushroom:
        //         // count = Bag.instance.mysticalMushroom;
        //         Reference.GameModel.MysticalMushroom.Subscribe(ShowText);
        //         ShowText(Reference.GameModel.MysticalMushroom.Value);
        //         break;
        //     
        //     case ETypePlant.CrystalNut:
        //         // count = Bag.instance.mysticalMushroom;
        //         Reference.GameModel.CrystalNut.Subscribe(ShowText);
        //         ShowText(Reference.GameModel.CrystalNut.Value);
        //         break;
        // }
    }

    private void ShowText(int value)
    {
        collected = value;
        textOrder.text = needplant + " / " + value;
    }

    private void OnEnable()
    {
        Reference.GameModel.StarchNut.UnSubscribe(ShowText);
        Reference.GameModel.MysticalMushroom.UnSubscribe(ShowText);
        Reference.GameModel.CrystalNut.UnSubscribe(ShowText);
    }
}