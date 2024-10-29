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


    public void CountText()
    {
        plant = GameManager.instance.GetPlantToType(typePlant);
        plant.quantity.Subscribe(ShowText);
        ShowText(plant.quantity.Value);
        // Debug.Log($"plant.quantity {plant.typePlant}/{plant.quantity.Value}");
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