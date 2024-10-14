using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public Image imageFrom;
    public TextMeshProUGUI textLevFrom;
    public TextMeshProUGUI textCountPlantsFrom;
   
    
    public Image imageBefore;
    public TextMeshProUGUI textLevBefore;
    public TextMeshProUGUI textCountPlantsBefore;
    public TextMeshProUGUI textCost;
    public Button upGradeButton;

    private Grydka _currentGrydka;
    private int _cost;

    public void Init(int lev, Grydka grydka)
    {
        Reference.GameModel.BagInteractive.Value = false;
        _currentGrydka = grydka;
        _currentGrydka.uprgadePopUpActive = true;
        _currentGrydka.empty = true;
        var fromGrydka = GameManager.instance.upgradeGrydkaCfgs[lev - 1];
        var beforeGrydka = GameManager.instance.upgradeGrydkaCfgs[lev];
        textCost.text = GameManager.instance.upgradeGrydkaCfgs[lev].cost.ToString();
        imageFrom.sprite = fromGrydka.sprite;
        textLevFrom.text = fromGrydka.textLev;
        textCountPlantsFrom.text = fromGrydka.textCountPlants;

        imageBefore.sprite = beforeGrydka.sprite;
        textLevBefore.text = beforeGrydka.textLev;
        textCountPlantsBefore.text = beforeGrydka.textCountPlants;

        if (GameManager.instance.coin.Value < GameManager.instance.upgradeGrydkaCfgs[lev].cost)
        {
            upGradeButton.interactable = false;
        }
        else
        {
            _cost = GameManager.instance.upgradeGrydkaCfgs[lev].cost;
            upGradeButton.interactable = true;
        }
    }

    public void UpgradeGrydka()
    {
        GameManager.instance.coin.Value -= _cost;
        _currentGrydka.ApplyUpgrade();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Reference.GameModel.BagInteractive.Value = true;
        _currentGrydka.uprgadePopUpActive = false;
        _currentGrydka.empty = false;
    }
}

[System.Serializable]
public class UpgradeGrydkaCfg
{
    public int Level;
    public Sprite sprite;
    public string textLev;
    public string textCountPlants;
    public int minPlants;
    public int maxPlants;
    public int cost;

}