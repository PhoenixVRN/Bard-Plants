using UnityEngine;

public class Customer : MonoBehaviour
{
    public eTypeCustomer TypeCustomer;
    public Orders orders;
    public int reward = 0;
    public GameObject coin;
    public GameObject Experience;
    public bool IsUsed;
    
    

    public void Awake()
    {
        orders = GetComponentInChildren<Orders>();
        orders.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log($"OnMouseDown");
        if (TypeCustomer == eTypeCustomer.GoldenForestMonster && orders.GoldDeliveryOrder())
        {
            Debug.Log($"Golden Completed");
            //TODO реализовать протарциональныое вычитание растений из кулька
            //----------------------------------------------------------------------------------------------------------
            // GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
            //     GameManager.instance.CoinPos.position, coin);
            // GameManager.instance.coin.Value += reward;
            // // Reference.GameModel.NumberCompletedOrders.Value++;
            // Reference.GameModel.CloseCustomersInLevel.Value++;
            // GameManager.instance.getTokensVFXController.ShowGetTokensVFX(10,transform.position,
            //     GameManager.instance.textLevelGame.transform.position, Experience);
            var cust2 = CustomerSystem.instance.allGoldenCustomerType.Find(c => c.TypeCustomer == TypeCustomer);
            if (cust2)
            {
                cust2.IsUsed = false;
            }
            gameObject.SetActive(false);
            Reference.GameModel.CountCustomerInGame.Value--;
            Reference.GameModel.CloseCustomersInLevel.Value++;
            //----------------------------------------------------------------------------------------------------------
            
            
            return;
        }
        
        if (orders.DeliveryOrder())
        {
             Debug.Log($"Add Expa {reward}");
            GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
                GameManager.instance.CoinPos.position, coin);
            GameManager.instance.coin.Value += reward;
            // Reference.GameModel.NumberCompletedOrders.Value++;
            GameManager.instance.getTokensVFXController.ShowGetTokensVFX(10,transform.position,
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