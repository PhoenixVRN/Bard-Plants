using TMPro;
using UnityEngine;
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


        completeOrderFon.SetActive(false);
        completeOrderTagl.SetActive(false);
        // Reference.GameModel.StarchNut.Subscribe(ShowText);
        CountText();
    }

    public void InitOrderGold(int needCount)
    {
        needplant = needCount;
        // this.typePlant = typePlant;
        // plantImage.texture = GameManager.instance.GetPlantToType(typePlant).spritePlant[4];


        completeOrderFon.SetActive(false);
        completeOrderTagl.SetActive(false);
        // Reference.GameModel.StarchNut.Subscribe(ShowText);

        foreach (var plant in GameManager.instance.allPlants)
        {
            plant.quantity.Subscribe(AllFruit);
        }

        AllFruit(0);
    }

    public void CountText()
    {
        plant = GameManager.instance.GetPlantToType(typePlant);
        plant.quantity.Subscribe(ShowText);
        Debug.Log($"CountText {plant.namePlant}/{plant.quantity.Value}");
        ShowText(plant.quantity.Value);
        // Debug.Log($"plant.quantity {plant.typePlant}/{plant.quantity.Value}");
    }

    private void AllFruit(int value)
    {
        int currentfruit = 0;
        foreach (var plant in GameManager.instance.allPlants)
        {
            currentfruit += plant.quantity.Value;
        }

        ShowText(currentfruit);
        // Debug.Log($"plant.quantity {plant.typePlant}/{plant.quantity.Value}");
    }

    private void ShowText(int value)
    {
        // Debug.Log($"CountText {plant.namePlant}/{plant.quantity.Value}");
        collected = value;
        textOrder.text = needplant + " / " + value;
    }

    private void OnDisable()
    {
        if (plant != null)
        {
            plant.quantity.UnSubscribe(ShowText);
        }
        else
        {
            foreach (var plant in GameManager.instance.allPlants)
            {
                plant.quantity.UnSubscribe(AllFruit);
            }
        }
        // plant.quantity.UnSubscribe(ShowText);
    }
}