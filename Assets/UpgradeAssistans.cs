using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAssistans : MonoBehaviour
{
    public List<Sprite> allAssistanse;
    public Image pers;

    public GameObject panelSpeed;
    public GameObject panelSpeedAction;
    public GameObject panelStartAction;
    public GameObject panelBuyButton;
    public GameObject panelLock;

    public Sprite sprireOpenUpgrade;
    
    
    private int _numAssistance;
    private GameModel _gameModel;
    private void OnEnable()
    {
        _numAssistance = 0;
        pers.sprite = allAssistanse[_numAssistance];
    }

    private void Start()
    {
        _gameModel = Reference.GameModel;
        CheckActiveAssistance();
    }

    public void Right()
    {
        // Debug.Log($"Right{_numAssistance}");
        _numAssistance++;
        _numAssistance = _numAssistance > 2 ? 0 : _numAssistance;
        pers.sprite = allAssistanse[_numAssistance];
    }
    public void Left()
    {
        // Debug.Log($"Left {_numAssistance}");
        _numAssistance--;
        _numAssistance = _numAssistance < 0 ? 2 : _numAssistance;
        pers.sprite = allAssistanse[_numAssistance];
    }

    private void CheckActiveAssistance()
    {
        switch (_numAssistance)
        {
            case 0 :
                if (_gameModel.GardenGnome.Value)
                {
                    
                }
                break;
            
            case 1 :
                if (_gameModel.CollectorGnome.Value)
                {
                    
                }
                break;
            
            case 2 :
                if (_gameModel.MusicHelpers.Value)
                {
                    
                }
                break;
        }
    }
    
}
