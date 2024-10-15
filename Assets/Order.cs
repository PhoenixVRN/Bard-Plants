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
        switch (typePlant)
        {
            case ETypePlant.Starch_Nut:
                plant.texture = GameManager.instance.spriteTexturesPlant[0];
                break;

            case ETypePlant.Mystical_Mushroom:
                plant.texture = GameManager.instance.spriteTexturesPlant[1];
                break;
            
            case ETypePlant.CrystalNut:
                plant.texture = GameManager.instance.spriteTexturesPlant[2];
                break;
        }

        completeOrderFon.SetActive(false);
        completeOrderTagl.SetActive(false);
        // Reference.GameModel.StarchNut.Subscribe(ShowText);
        CountText();
    }


    public void CountText()
    {
        switch (typePlant)
        {
            case ETypePlant.Starch_Nut:
                // count = Bag.instance.starchNut;
                Reference.GameModel.StarchNut.Subscribe(ShowText);
                ShowText(Reference.GameModel.StarchNut.Value);
                break;

            case ETypePlant.Mystical_Mushroom:
                // count = Bag.instance.mysticalMushroom;
                Reference.GameModel.MysticalMushroom.Subscribe(ShowText);
                ShowText(Reference.GameModel.MysticalMushroom.Value);
                break;
            
            case ETypePlant.CrystalNut:
                // count = Bag.instance.mysticalMushroom;
                Reference.GameModel.CrystalNut.Subscribe(ShowText);
                ShowText(Reference.GameModel.CrystalNut.Value);
                break;
        }
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