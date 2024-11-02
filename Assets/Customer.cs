using UnityEngine;

public class Customer : MonoBehaviour
{
    public Orders orders;
    public int reward = 0;
    public GameObject coin;
    public GameObject Experience;
    
    

    public void Awake()
    {
        orders = GetComponentInChildren<Orders>();
        orders.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log($"OnMouseDown");
        if (orders.DeliveryOrder())
        {
             Debug.Log($"Add Expa {reward}");
            GameManager.instance.getTokensVFXController.ShowGetTokensVFX(reward / 10, transform.position,
                GameManager.instance.CoinPos.position, coin);
            GameManager.instance.coin.Value += reward;
            Reference.GameModel.NumberCompletedOrders.Value++;
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
            gameObject.SetActive(false);
            Reference.GameModel.CountCustomerInGame.Value--;
        }
    }
}