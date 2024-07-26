using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public List<UpgradeGrydkaCfg> upgradeGrydkaCfgs;
    public Image imageFrom;
    public TextMeshProUGUI textLevFrom;
    public TextMeshProUGUI textCountPlantsFrom;
    
    public Image imageBefore;
    public TextMeshProUGUI textLevBefore;
    public TextMeshProUGUI textCountPlantsBefore;
    public TextMeshProUGUI textCost;

    private Grydka _currentGrydka;
    
    public void Init(int lev, Grydka grydka)
    {
        _currentGrydka = grydka;
        _currentGrydka.uprgadePopUpActive = true;
        _currentGrydka.empty = true;
        var fromGrydka = upgradeGrydkaCfgs[lev - 1];
        var beforeGrydka = upgradeGrydkaCfgs[lev];
        textCost.text = upgradeGrydkaCfgs[lev].cost.ToString();
        imageFrom.sprite = fromGrydka.sprite;
        textLevFrom.text = fromGrydka.textLev;
        textCountPlantsFrom.text = fromGrydka.textCountPlants;

        imageBefore.sprite = beforeGrydka.sprite;
        textLevBefore.text = beforeGrydka.textLev;
        textCountPlantsBefore.text = beforeGrydka.textCountPlants;
    }

    public void Upgrуade()
    {
        _currentGrydka.ApplyUpgrade();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
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
    public int cost;

}