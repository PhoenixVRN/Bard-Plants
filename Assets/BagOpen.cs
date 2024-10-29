using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagOpen : MonoBehaviour
{
    public List<GameObject> habar;

    private void OnEnable()
    {
        InitBag();
    }
    

    private void InitBag()
    {
        int countSlot = 0;
        foreach (var plant in GameManager.instance.allPlants)
        {
            if (plant.quantity.Value > 0)
            {
                habar[countSlot].gameObject.SetActive(true);
                habar[countSlot].GetComponent<RawImage>().texture = plant.spritePlant[4];
                habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                    plant.quantity.Value.ToString();
                countSlot++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var h in habar)
        {
            h.SetActive(false);
        }
    }
}