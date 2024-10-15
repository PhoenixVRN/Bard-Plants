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
        if (Reference.GameModel.StarchNut.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[0];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.StarchNut.Value.ToString();
            countSlot++;
        }

        if (Reference.GameModel.MysticalMushroom.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[1];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.MysticalMushroom.Value.ToString();
            countSlot++;
        }

        if (Reference.GameModel.CrystalNut.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[2];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.CrystalNut.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.FlyEater.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[3];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.FlyEater.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.GutFlower.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[4];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.GutFlower.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.Mandrake.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[5];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.Mandrake.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.MiracleFruit.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[6];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.MiracleFruit.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.NeedleFlower.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[7];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.NeedleFlower.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.StaringFlower.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[8];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.StaringFlower.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.ToxicMushroom.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[9];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.ToxicMushroom.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.BushTentacles.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[10];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.BushTentacles.Value.ToString();
            countSlot++;
        }
        
        if (Reference.GameModel.StarFruit.Value > 0)
        {
            habar[countSlot].gameObject.SetActive(true);
            habar[countSlot].GetComponent<RawImage>().texture = GameManager.instance.spriteTexturesPlant[11];
            habar[countSlot].GetComponentInChildren<TextMeshProUGUI>().text =
                Reference.GameModel.StarFruit.Value.ToString();
            countSlot++;
        }
        
        
    }

    private void OnDisable()
    {
        foreach (var h in habar)
        {
            h.SetActive(false);
        }
    }

    void Update()
    {
    }
}