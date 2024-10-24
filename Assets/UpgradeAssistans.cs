using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAssistans : MonoBehaviour
{
    public List<Sprite> allAssistanse;
    public Image pers;
    public int costAssistance;
    public int costUpgrade;

    public GameObject panelSpeed;
    public GameObject panelSpeedAction;
    public GameObject panelStartAction;
    public GameObject panelBuyButton;
    public GameObject panelLock;
    public GameObject panelBuyUpgrades;

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

    private bool _currentAssistanseUp;
    private LvlAssistance _currentLvlAssistance;
    private int _currentLevSpeed;
    private int _currentLevSpeedAction;
    private int _currentLevStartAction;


    private void OnEnable()
    {
        _numAssistance = 0;
        pers.sprite = allAssistanse[_numAssistance];
    }

    private void Start()
    {
        _gameModel = Reference.GameModel;
        UpDateData();
        CheckActiveAssistance();
    }

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
                _gameModel.GardenGnome.Value = true;
                break;
            case 1:
                _gameModel.CollectorGnome.Value = true;
                break;
            case 2:
                _gameModel.MusicHelpers.Value = true;
                break;
        }
        UpDateData();
        CheckActiveAssistance();
    }

    public void BuyLevelSpeed()
    {
        _currentLvlAssistance.lvlSpeed++;
        // switch (_numAssistance)
        // {
        //     case 0:
        //         _gameModel.GardenGnomeLevel.Value.lvlSpeed++;
        //         break;
        //
        //     case 1:
        //         _gameModel.CollectorGnomeLevel.Value.lvlSpeed++;
        //         break;
        //
        //     case 2:
        //         _gameModel.MusicHelpersLevel.Value.lvlSpeed++;
        //         break;
        // }

        UpDateData();
        SetLevelButton();
    }
    public void BuySpeedAction()
    {
        _currentLvlAssistance.lvlActions++;

        UpDateData();
        SetLevelButton();
    }

    public void BuyStartAction()
    {
        _currentLvlAssistance.lvlStartAction++;
        UpDateData();
        SetLevelButton();
    }

    private void SetLevelButton()
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
            textBuySpeed.text = costUpgrade.ToString();
            sprireBuyArrowSpeed.gameObject.SetActive(true);
            //TODO check coins
            sprireBuyArrowSpeed.transform.parent.gameObject.GetComponent<Button>().interactable = true;
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
            textBuySpeedAction.text = costUpgrade.ToString();
            sprireBuyArrowSpeedAction.gameObject.SetActive(true);
            sprireBuyArrowSpeedAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
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
            textBuyStartAction.text = costUpgrade.ToString();
            sprireBuyArrowStartAction.gameObject.SetActive(true);
            sprireBuyArrowStartAction.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private void UpDateData()
    {
        switch (_numAssistance)
        {
            case 0:
                _currentAssistanseUp = _gameModel.GardenGnome.Value;
                _currentLvlAssistance = _gameModel.GardenGnomeLevel.Value;
                _currentLevSpeed = _gameModel.GardenGnomeLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.GardenGnomeLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.GardenGnomeLevel.Value.lvlStartAction;
                break;

            case 1:
                _currentAssistanseUp = _gameModel.CollectorGnome.Value;
                _currentLvlAssistance = _gameModel.CollectorGnomeLevel.Value;
                _currentLevSpeed = _gameModel.CollectorGnomeLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.CollectorGnomeLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.CollectorGnomeLevel.Value.lvlStartAction;
                break;

            case 2:
                _currentAssistanseUp = _gameModel.MusicHelpers.Value;
                _currentLvlAssistance = _gameModel.MusicHelpersLevel.Value;
                _currentLevSpeed = _gameModel.MusicHelpersLevel.Value.lvlSpeed;
                _currentLevSpeedAction = _gameModel.MusicHelpersLevel.Value.lvlActions;
                _currentLevStartAction = _gameModel.MusicHelpersLevel.Value.lvlStartAction;
                break;
        }
    }
}