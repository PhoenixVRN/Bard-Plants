using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public eTypeCustomer TypeCustomer;
    public Orders orders;
    public int reward = 0;
    public GameObject coin;
    public GameObject Experience;
    public bool IsUsed;
    
    [Space][Header("Сoefficient for level reward %")]
    public int levelReward;
    [Space][Header("Range of values")]
    public int minRange;
    public int maxRange;


    public void Awake()
    {
        orders = GetComponentInChildren<Orders>();
        orders.gameObject.SetActive(true);
    }

    // private void OnMouseDown()
    public void OnCustomerClicked()
    {
        Debug.Log($"OnMouseDown");
        if (TypeCustomer == eTypeCustomer.GoldenForestMonster)
        {
            if (orders.GoldDeliveryOrder())
            {
                Debug.Log($"Golden Completed");
                //TODO реализовать проПарциональныое вычитание растений из кулька
                var summ = 0;
                var qantity = orders.ordersActive[0].needplant;
                foreach (var openPlant in GameManager.instance.allPlants)
                {
                    summ += openPlant.quantity.Value;
                    // Debug.Log($"openPlant {openPlant.namePlant} - {openPlant.quantity.Value}");
                }

                Debug.Log($"Summ {summ}");
                var all = 0f;
                foreach (var openPlant in GameManager.instance.allPlants)
                {
                    if (openPlant.quantity.Value > 0)
                    {
                        var z = (float)((float)openPlant.quantity.Value / (float)summ);
                        var procent = (float)((float)openPlant.quantity.Value / (float)summ) * 100f;
                        var x = (qantity * procent);
                        var e = (qantity * procent) / 100;
                        var s = (int)Math.Round(e);
                        Debug.Log(
                            $"z {z}/ x {x}openPlant {openPlant.namePlant} - {openPlant.quantity.Value}, proc {procent},need {qantity}, vzat {e}/{s}");
                        all += e;
                        openPlant.quantity.Value -= s;
                    }
                }

                Debug.Log($"All {all}");
                //----------------------------------------------------------------------------------------------------------
                GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
                    GameManager.instance.CoinPos.position, coin);
                GameManager.instance.coin.Value += reward;
                // Reference.GameModel.NumberCompletedOrders.Value++;
                // Reference.GameModel.CloseCustomersInLevel.Value++;
                GameManager.instance.getTokensVFXController.ShowGetTokensVFX(10, transform.position,
                    GameManager.instance.textLevelGame.transform.position, Experience);
                var cust2 = CustomerSystem.instance.allGoldenCustomerType.Find(c => c.TypeCustomer == TypeCustomer);
                if (cust2)
                {
                    cust2.IsUsed = false;
                }

                gameObject.SetActive(false);
                Reference.GameModel.CountCustomerInGame.Value--;
                Reference.GameModel.CloseCustomersInLevel.Value++;
                //----------------------------------------------------------------------------------------------------------

            }

            return;
        }

        if (orders.DeliveryOrder())
        {
            Debug.Log($"Add Expa {reward}");
            GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
                GameManager.instance.CoinPos.position, coin);
            GameManager.instance.coin.Value += reward;
            // Reference.GameModel.NumberCompletedOrders.Value++;
            GameManager.instance.getTokensVFXController.ShowGetTokensVFX(10, transform.position,
                GameManager.instance.textLevelGame.transform.position, Experience);
            foreach (var ord in orders.ordersActive)
            {
                if (ord.typePlant == 0)
                {
                    Debug.Log($"typePlant == 0 {ord.typePlant}");
                    break;
                }

                //TODO 
                // Debug.Log($"ord.typePlant {ord.typePlant}");
                // Debug.Log($"Plant type {ord.typePlant}/ {GameManager.instance.GetPlantToType(ord.typePlant)}");
                GameManager.instance.GetPlantToType(ord.typePlant).quantity.Value -= ord.needplant;
            }

            // GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
            //     GameManager.instance.textCoin.transform.position, coin);
            // GameManager.instance.coin.Value += reward;
            // Reference.GameModel.NumberCompletedOrders.Value++;
            var cust = CustomerSystem.instance.allWoodenCustomerType.Find(c => c.TypeCustomer == TypeCustomer);
            if (cust)
            {
                cust.IsUsed = false;
            }

            // var cust2 = CustomerSystem.instance.allGoldenCustomerType.Find(c => c.TypeCustomer == TypeCustomer);
            // if (cust2)
            // {
            //     cust2.IsUsed = false;
            // }
            gameObject.SetActive(false);
            Reference.GameModel.CountCustomerInGame.Value--;
            Reference.GameModel.CloseCustomersInLevel.Value++;
        }
    }
}