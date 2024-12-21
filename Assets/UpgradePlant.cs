using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePlant : MonoBehaviour
{
   public TextMeshProUGUI namePlants;
   public TextMeshProUGUI countPlants;
   public TextMeshProUGUI count;
   public TextMeshProUGUI cost;
   public Image upgradeIcon;
   
   public void Init(Plant plant)
   {
      // upgradeIcon.sprite = plant.spritePlant[4];
      namePlants.text = plant.namePlant;
      countPlants.text = "X" + plant.quantity.Value;
      count.text = plant.Level +1.ToString();
      cost.text = ((plant.Level + 1) * 200).ToString();
   }
}
