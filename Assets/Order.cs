using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public RawImage plantImage;
    public TextMeshProUGUI textOrder;
    public GameObject completeOrderTagl;
    public GameObject completeOrderFon;
    public ETypePlant typePlant;
    public int needplant;
    public int collected;
    public bool completed;
    private Plant plant;

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
        plantImage.texture = GameManager.instance.GetPlantToType(typePlant).spritePlant[4];
        // switch (typePlant)
        // {
        //     case ETypePlant.StarchNut:
        //         plantImage.texture = GameManager.instance.spriteTexturesPlant[0];
        //         break;
        //
        //     case ETypePlant.MysticalMushroom:
        //         plantImage.texture = GameManager.instance.spriteTexturesPlant[1];
        //         break;
        //     
        //     case ETypePlant.CrystalNut:
        //         plantImage.texture = GameManager.instance.spriteTexturesPlant[2];
        //         break;
        // }

        completeOrderFon.SetActive(false);
        completeOrderTagl.SetActive(false);
        // Reference.GameModel.StarchNut.Subscribe(ShowText);
        CountText();
    }


    public void CountText()
    {
        plant = GameManager.instance.GetPlantToType(typePlant);
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

    // private void OnDisable()
    // {
    //     plant.quantity.UnSubscribe(ShowText);
    // }
}