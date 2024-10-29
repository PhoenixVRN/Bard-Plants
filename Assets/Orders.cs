using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Orders : MonoBehaviour
{
    public List<Order> ordersActive;
    public TextMeshProUGUI rewardText;
    

    public void InitOrders(OrderCfg orderCfg)
    {
        rewardText.text = orderCfg.reward.ToString();
        ordersActive[0].gameObject.SetActive(true);
        ordersActive[0].InitOrder(orderCfg.firstPlant, orderCfg.firstCount);
        
        if (orderCfg.countOrders > 1)
        {
            ordersActive[1].gameObject.SetActive(true);
            ordersActive[1].InitOrder(orderCfg.secondPlant, orderCfg.secondCount);
        }
        if (orderCfg.countOrders > 2)
        {
            ordersActive[2].gameObject.SetActive(true);
            ordersActive[2].InitOrder(orderCfg.thirdPlant, orderCfg.thirdCount);
        }
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