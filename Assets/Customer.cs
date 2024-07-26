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
            foreach (var ord in orders.ordersActive)
            {
                switch (ord.typePlant)
                {
                    case ETypePlant.Starch_Nut:
                        Reference.GameModel.StarchNut.Value -= ord.needplant;
                        break;

                    case ETypePlant.Mystical_Mushroom:
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