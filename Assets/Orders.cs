using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Orders : MonoBehaviour
{
    public List<Order> ordersActive;
    public TextMeshProUGUI rewardText;
    public Customer customer;
    

    public void InitOrders(int quantityOrders)
    {

        var plant1 = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)].typePlant;
        var needQuantity1 = Random.Range(10, 20);
        customer.reward = needQuantity1 * GameManager.instance.GetPlantToType(plant1).defaultValueDelivery;
        ordersActive[0].gameObject.SetActive(true);
        ordersActive[0].InitOrder(plant1, needQuantity1);
        var plant2 = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)].typePlant;
        if (quantityOrders > 1)
        {
           
            while (plant1 == plant2)
            {
                plant2 = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)].typePlant;
            }
           
            var needQuantity2 = Random.Range(10, 20);
            ordersActive[1].gameObject.SetActive(true);
            ordersActive[1].InitOrder(plant2, needQuantity2);
            customer.reward += needQuantity2 * GameManager.instance.GetPlantToType(plant2).defaultValueDelivery;
        }
        if (quantityOrders > 2)
        {
            var plant3 = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)].typePlant;
            
            while (plant1 == plant3 || plant2 == plant3)
            {
                plant3 = GameManager.instance.openPlants[Random.Range(0, GameManager.instance.openPlants.Count)].typePlant;
            }
           
            var needQuantity3 = Random.Range(10, 20);
            
            ordersActive[2].gameObject.SetActive(true);
            ordersActive[2].InitOrder(plant3, needQuantity3);
            customer.reward += needQuantity3 * GameManager.instance.GetPlantToType(plant3).defaultValueDelivery;
        }
        rewardText.text = customer.reward.ToString();
    }


    public void GoldOrdersInit()
    {
      //  customer.reward = пердать сумму награды
        ordersActive[0].gameObject.SetActive(true);
        // TODO  метод расчета количества растений на золотого
        var needQuantity = Random.Range(10, 20);
        ordersActive[0].InitOrderGold(needQuantity);
    }

    public bool GoldDeliveryOrder()
    {
        return ordersActive[0].completed;
    }
    public bool DeliveryOrder()
    {
        var allActive = ordersActive.FindAll(c => c.gameObject.activeSelf);
        foreach (var ord in allActive)
        {
            if (!ord.completed)
            {
                return false;
            }
        }

        return true;
    }
}

public class OrderData
{
    public int countOrders;
    public ETypePlant firstPlant;
    public int firstCount;
    public ETypePlant secondPlant;
    public int secondCount;
    public ETypePlant thirdPlant;
    public int thirdCount;
    public int reward;

    public OrderData()
    {
       
    }
}

[Serializable]
public struct OrderCfg
{
    public int typeCustomer;
    public int countOrders;
    public ETypePlant firstPlant;
    public int firstCount;
    public ETypePlant secondPlant;
    public int secondCount;
    public ETypePlant thirdPlant;
    public int thirdCount;
    public int reward;
}