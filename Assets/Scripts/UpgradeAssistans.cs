using System;
using System.Collections;
using System.Collections.Generic;
using N.Fridman.FormatNums.Scripts.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UpgradeAssistans : MonoBehaviour
{
    public List<Sprite> allAssistanse;
    public Image pers;
    public int costAssistanceGarden;
    public int costAssistanceCollecroir;
    public int costAssistanceMusic;
    private int costAssistance;
    public int costUpgrade;

    public GameObject panelSpeed;
    public GameObject panelSpeedAction;
    public GameObject panelStartAction;
    public GameObject panelBuyButton;
    public GameObject panelLock;
    public GameObject panelBuyUpgrades;

    public CostEndCoefficientDTO CostUpgradeGardenNew;
    public CostEndCoefficientDTO CostUpgradeCollectorNew;
    public CostEndCoefficientDTO CostUpgradeMusicHelperNew;
    
    public TextMeshProUGUI textNameAssistance;
    public TextMeshProUGUI textCostAssistance;

    public TextMeshProUGUI textPanelSpeed;
    public TextMeshProUGUI textPanelSpeedAction;
    public TextMeshProUGUI textPanelStartAction;

    public TextMeshProUGUI textBuySpeed;
    public TextMeshProUGUI textBuySpeedAction;
    public TextMeshProUGUI textBuyStartAction;

    public Image sprireBuyArrowSpeed;
    public Image sprireBuyArrowSpeedAction;
    public Image sprireBuyArrowStartAction;

    public Sprite sprireOpenUpgrade;
    public Sprite sprireCloseUpgrade;


    private int _numAssistance;
    private GameModel _gameModel;
    private GameManager _gameManager;

    private bool _currentAssistanseUp;
    private LvlAssistance _currentLvlAssistance;
    private int _currentLevSpeed = 1;
    private int _currentLevSpeedAction = 1;
    private int _currentLevStartAction = 1;


    private void OnEnable()
    {
        // Time.timeScale = 0f;
        _gameManager = GameManager.instance;
        _gameModel = Reference.GameModel;
        _numAssistance = 0;
        pers.sprite = allAssistanse[_numAssistance];
        GameManager.instance.coin.Subscribe(SetLevelButton);
        UpDateData();
        CheckActiveAssistance();
    }
    private void OnDisable()
    {
        GameManager.instance.coin.UnSubscribe(SetLevelButton);
        // Time.timeScale = 1f;
    }
    

    // private void Start()
    // {
    //     _gameManager = GameManager.instance;
    //     _gameModel = Reference.GameModel;
    //     UpDateData();
    //     CheckActiveAssistance();
    // }

    public void Right()
    {
        // Debug.Log($"Right{_numAssistance}");
        _numAssistance++;
        _numAssistance = _numAssistance > 2 ? 0 : _numAssistance;
        pers.sprite = allAssistanse[_numAssistance];
        UpDateData();
        CheckActiveAssistance();
    }

    public void Left()
    {
        // Debug.Log($"Left {_numAssistance}");
        _numAssistance--;
        _numAssistance = _numAssistance < 0 ? 2 : _numAssistance;
        pers.sprite = allAssistanse[_numAssistance];
        UpDateData();
        CheckActiveAssistance();
    }

    private void CheckActiveAssistance()
    {
        if (_currentAssistanseUp)
        {
            SetLvL();
        }
        else
        {
            HideLvL();
        }
    }

    private void SetLvL()
    {
        panelSpeed.GetComponent<Image>().sprite = sprireOpenUpgrade;
        panelSpeedAction.GetComponent<Image>().sprite = sprireOpenUpgrade;
        panelStartAction.GetComponent<Image>().sprite = sprireOpenUpgrade;
        panelLock.SetActive(false);
        panelBuyButton.SetActive(false);
        panelBuyUpgrades.SetActive(true);
        SetLevelButton();
    }

    private void HideLvL()
    {
        panelSpeed.GetComponent<Image>().sprite = sprireCloseUpgrade;
        panelSpeedAction.GetComponent<Image>().sprite = sprireCloseUpgrade;
        panelStartAction.GetComponent<Image>().sprite = sprireCloseUpgrade;
        textCostAssistance.text = FormatNumsHelper.FormatNum((float)costAssistance);
        if (CheckMony(_gameManager.coin.Value, costAssistance))
        {
            textCostAssistance.color = Color.black;
            panelBuyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            textCostAssistance.color = Color.red;
            panelBuyButton.GetComponent<Button>().interactable = false;
        }
        panelBuyButton.SetActive(true);
        panelLock.SetActive(true);
        panelBuyUpgrades.SetActive(false);
        SetLevelButton();
    }

    
    public void BuyAssistance()
    {
        GameManager.instance.coin.Value -= costAssistance;
        switch (_numAssistance)
        {
            case 0:
                // Debug.Log($"You bought {_numAssistance} upgrades.");
                _gameModel.GardenGnome.Value = true;
                AnalyticsManager.instance.AnalyticsEvent("count_buy_gnoms");
                break;
            case 1:
                _gameModel.CollectorGnome.Value = true;
                AnalyticsManager.instance.AnalyticsEvent("count_buy_collector");
                break;
            case 2:
                _gameModel.MusicHelpers.Value = true;
                AnalyticsManager.instance.AnalyticsEvent("count_buy_musicant");
                break;
        }
        UpDateData();
        CheckActiveAssistance();
    }

    public void BuyLevelSpeed()
    {
        GameManager.instance.coin.Value -= SetCostUpgradeSpeed(_currentLevSpeed);
        _currentLvlAssistance.lvlSpeed++;
        AnalyticsManager.instance.AnalyticsEvent("count_upgrade_movespeed_helpers");
        UpDateData();
        SetLevelButton();
    }
    public void BuySpeedAction()
    {
        GameManager.instance.coin.Value -= SetCostUpgradeSpeedAction(_currentLevSpeedAction);
        _currentLvlAssistance.lvlActions++;
        AnalyticsManager.instance.AnalyticsEvent("count_upgrade_speedworking_helpers");
        UpDateData();
        SetLevelButton();
    }

    public void BuyStartAction()
    {
        GameManager.instance.coin.Value -= SetCostUpgradeAction(_currentLevStartAction);
        _currentLvlAssistance.lvlStartAction++;
        AnalyticsManager.instance.AnalyticsEvent("count_upgrade_chiil_before_work_helpers");
        UpDateData();
        SetLevelButton();
    }

    private void SetLevelButton(int value = 0)
    {
        if (_currentLevSpeed >= 15)
        {
            textPanelSpeed.text = "15 LvL";
            textBuySpeed.text = "MAX";
            sprireBuyArrowSpeed.gameObject.SetActive(false);
            sprireBuyArrowSpeed.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            textPanelSpeed.text = _currentLevSpeed + " LvL";
            // textBuySpeed.text = costUpgrade.ToString();
            // Debug.Log($"textBuySpeed {SetCostUpgradeSpeed(_currentLevSpeed).ToString()}");
            int costUprgade = SetCostUpgradeSpeed(_currentLevSpeed);
            textBuySpeed.text = FormatNumsHelper.FormatNum((float)costUprgade);
            sprireBuyArrowSpeed.gameObject.SetActive(true);
           if (CheckMony(_gameManager.coin.Value, costUprgade))
           {
            sprireBuyArrowSpeed.transform.parent.gameObject.GetComponent<Button>().interactable = true;
           }
           else
           {
               sprireBuyArrowSpeed.transform.parent.gameObject.GetComponent<Button>().interactable = false;
           }
        }

        if (_currentLevSpeedAction >= 15)
        {
            textPanelSpeedAction.text = "15 LvL";
            textBuySpeedAction.text = "MAX";
            sprireBuyArrowSpeedAction.gameObject.SetActive(false);
            sprireBuyArrowSpeedAction.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            textPanelSpeedAction.text = _currentLevSpeedAction + " LvL";
            // textBuySpeedAction.text = costUpgrade.ToString();
            int costUprgade = SetCostUpgradeSpeedAction(_currentLevSpeedAction);
            textBuySpeedAction.text = FormatNumsHelper.FormatNum((float)costUprgade);
            sprireBuyArrowSpeedAction.gameObject.SetActive(true);
            if (CheckMony(_gameManager.coin.Value, costUprgade))
            {
                sprireBuyArrowSpeedAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                sprireBuyArrowSpeedAction.transform.parent.gameObject.GetComponent<Button>().interactable = false;
            }
            // sprireBuyArrowSpeedAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }

        if (_currentLevStartAction >= 15)
        {
            textPanelStartAction.text = "15 LvL";
            textBuyStartAction.text = "MAX";
            sprireBuyArrowStartAction.gameObject.SetActive(false);
            sprireBuyArrowStartAction.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            textPanelStartAction.text = _currentLevStartAction + " LvL";
            // textBuyStartAction.text = costUpgrade.ToString();
            int costUprgade = SetCostUpgradeAction(_currentLevStartAction);
            textBuyStartAction.text = FormatNumsHelper.FormatNum((float)costUprgade);
            sprireBuyArrowStartAction.gameObject.SetActive(true);
            if (CheckMony(_gameManager.coin.Value, costUprgade))
            {
                sprireBuyArrowStartAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                sprireBuyArrowStartAction.transform.parent.gameObject.GetComponent<Button>().interactable = false;
            }
            // sprireBuyArrowStartAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private void UpDateData()
    {
        switch (_numAssistance)
        {
            case 0:
                costAssistance = costAssistanceGarden;
                var localizString = LocalizationSettings.StringDatabase.GetLocalizedString("Game","gnoms");
                textNameAssistance.text = localizString;
                _currentAssistanseUp = _gameModel.GardenGnome.Value;
                _currentLvlAssistance = _gameModel.GardenGnomeLevel.Value;
                _currentLevSpeed = _gameModel.GardenGnomeLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.GardenGnomeLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.GardenGnomeLevel.Value.lvlStartAction;
                break;

            case 1:
                costAssistance = costAssistanceCollecroir;
                var localizString2 = LocalizationSettings.StringDatabase.GetLocalizedString("Game","taker");
                textNameAssistance.text =localizString2;
                _currentAssistanseUp = _gameModel.CollectorGnome.Value;
                _currentLvlAssistance = _gameModel.CollectorGnomeLevel.Value;
                _currentLevSpeed = _gameModel.CollectorGnomeLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.CollectorGnomeLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.CollectorGnomeLevel.Value.lvlStartAction;
                break;

            case 2:
                costAssistance = costAssistanceMusic;
                var localizString3 = LocalizationSettings.StringDatabase.GetLocalizedString("Game","musicant");
                textNameAssistance.text = localizString3;
                _currentAssistanseUp = _gameModel.MusicHelpers.Value;
                _currentLvlAssistance = _gameModel.MusicHelpersLevel.Value;
                _currentLevSpeed = _gameModel.MusicHelpersLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.MusicHelpersLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.MusicHelpersLevel.Value.lvlStartAction;
                break;
        }
    }
    private bool CheckMony(int mony, int cost)
    {
        return mony >= cost;
    }

    private int SetCostUpgradeSpeed(int level)
    {
        switch (_numAssistance)
        {
            case 0 :
                int s = CostUpgradeGardenNew.ElementaryCostSpeed;
                for (int i = 1; i < level; i++)
                {
                    s = (int)(s * CostUpgradeGardenNew.CoefficientCostSpeed);
                }
                return s;
            
            case 1 :
                int d = CostUpgradeCollectorNew.ElementaryCostSpeed;
                for (int i = 1; i < level; i++)
                {
                    d = (int)(d * CostUpgradeCollectorNew.CoefficientCostSpeed);
                }
                return d;
            case 2 :
                int f = CostUpgradeMusicHelperNew.ElementaryCostSpeed;
                for (int i = 1; i < level; i++)
                {
                    f = (int)(f * CostUpgradeMusicHelperNew.CoefficientCostSpeed);
                }
                return f;
        }
        return 100;
    }
    
    private int SetCostUpgradeSpeedAction(int level)
    {
        switch (_numAssistance)
        {
            case 0 :
                int s = CostUpgradeGardenNew.ElementaryCostSpeedAction;
                for (int i = 1; i < level; i++)
                {
                    s = (int)(s * CostUpgradeGardenNew.CoefficientCostSpeedAction);
                }
                return s;
            
            case 1 :
                int d = CostUpgradeCollectorNew.ElementaryCostSpeedAction;
                for (int i = 1; i < level; i++)
                {
                    d = (int)(d * CostUpgradeCollectorNew.CoefficientCostSpeedAction);
                }
                return d;
            case 2 :
                int f = CostUpgradeMusicHelperNew.ElementaryCostSpeedAction;
                for (int i = 1; i < level; i++)
                {
                    f = (int)(f * CostUpgradeMusicHelperNew.CoefficientCostAction);
                }
                return f;
        }
        return 100;
    }
    
    private int SetCostUpgradeAction(int level)
    {
        switch (_numAssistance)
        {
            case 0 :
                int s = CostUpgradeGardenNew.ElementaryCostAction;
                for (int i = 1; i < level; i++)
                {
                    s = (int)(s * CostUpgradeGardenNew.CoefficientCostAction);
                }
                return s;
            
            case 1 :
                int d = CostUpgradeCollectorNew.ElementaryCostAction;
                for (int i = 1; i < level; i++)
                {
                    d = (int)(d * CostUpgradeCollectorNew.CoefficientCostAction);
                }
                return d;
            case 2 :
                int f = CostUpgradeMusicHelperNew.ElementaryCostAction;
                for (int i = 1; i < level; i++)
                {
                    f = (int)(f * CostUpgradeMusicHelperNew.CoefficientCostAction);
                }
                return f;
        }
        return 100;
    }
   
   
    
    private int SetCostUpgrade(int level, eTypeUpgradeAssistanse type)
    {
        switch (type)
        {
          case  eTypeUpgradeAssistanse.SpeedGarden:
              
              break;
          case  eTypeUpgradeAssistanse.SpeedActionGarden:
              
              break;
          case  eTypeUpgradeAssistanse.StartActionGarden:
              
              break;
          case  eTypeUpgradeAssistanse.SpeedCollector:
              
              break;
          case  eTypeUpgradeAssistanse.SpeedActionCollector:
              
              break;
          case  eTypeUpgradeAssistanse.StartActionCollector:
              
              break;
          case  eTypeUpgradeAssistanse.SpeedMusicHelper:
              
              break;
          case  eTypeUpgradeAssistanse.SpeedActionMusicHelper:
              
              break;
          case  eTypeUpgradeAssistanse.StartActionMusicHelper:
              
              break;
          
        }
        return 1;
    }
}

public enum eTypeUpgradeAssistanse
{
    SpeedGarden,
    SpeedActionGarden,
    StartActionGarden,
    SpeedCollector,
    SpeedActionCollector,
    StartActionCollector,
    SpeedMusicHelper,
    SpeedActionMusicHelper,
    StartActionMusicHelper
}

[Serializable]
public class CostLevelUpgrade
{
    public int Level;
    public int CostSpeed;
    public int CostSpeedAction;
    public int CostAction;
}

[Serializable]
public class CostEndCoefficient
{
    public int ElementaryCostSpeed;
    public float CoefficientCostSpeed;
    public int ElementaryCostSpeedAction;
    public float CoefficientCostSpeedAction;
    public int ElementaryCostAction;
    public float CoefficientCostAction;
}