using UnityEngine;
using UnityEngine.Serialization;

public class Customer : MonoBehaviour
{
    public Orders orders;
  public int reward;

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
            GameManager.instance.coin.Value += reward;
            Reference.GameModel.NumberCompletedOrders.Value++;
            foreach (var ord in orders.ordersActive)
            {
                //TODO 
                switch (ord.typePlant)
                {
                    case ETypePlant.StarchNut:
                        Reference.GameModel.StarchNut.Value -= ord.needplant;
                        break;

                    case ETypePlant.MysticalMushroom:
                        Reference.GameModel.MysticalMushroom.Value -= ord.needplant;
                        break;
                    
                    case ETypePlant.CrystalNut:
                        Reference.GameModel.CrystalNut.Value -= ord.needplant;
                        break;
                }
            }
            gameObject.SetActive(false);
            Reference.GameModel.CountCustomerInGame.Value--;
        }
    }
}