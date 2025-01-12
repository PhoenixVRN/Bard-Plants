using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Object = System.Object;

public class MapController
{
    private GameModel _gameModel;
    private GameManager _gameManager;
    private int _currentMapIndex;
    private int _currentCloseOrder;
    private int _currentOpenOrder;

    public MapController()
    {
        _currentMapIndex = 0;
        _gameModel = new GameModel();
        _gameManager = GameManager.instance;
        Reference.GameModel.NumberClosedOrders.Subscribe(CheckLevelMap);
        // _gameModel.LevelMap.Subscribe(OnLevelChanged);
        // Debug.Log($"Map Controller initialized");
    }

    public void Init()
    {
        _gameManager.MapUprgadeText.text = 0 + " / " + _gameManager.levelGrydka[0].numberOfOrders;
    }

    public void OnLevelChanged(int level)
    {
        // Debug.Log($"Map OnLevelChanged");
        if (level > 0)
        {
            _gameManager.MapUprgadeText.text = 0 + " / " + (_gameManager.levelGrydka[_currentMapIndex].numberOfOrders -
                                                            _gameManager.levelGrydka[_currentMapIndex - 1]
                                                                .numberOfOrders);
        }

        // if (level > 0)
        // {
        //     _gameManager.levelGrydka[level - 1].border.ForEach(b => b.gameObject.SetActive(false));
        //     ;
        // }
        //
        // _gameManager.levelGrydka[level].border.ForEach(b => b.gameObject.SetActive(true));

        if (level > 0)
        {
            var allgroup = _gameManager.levelGrydka[level].forestGroup;
            foreach (Transform child in allgroup.transform)
            {
                // Debug.Log("Дочерний объект (первый уровень): " + child.name);
                child.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => _gameManager.DestroyForest(child));
            }
        }

        Reference.GameModel.MaxNumberPlants.Value = _gameManager.levelGrydka[level].numberPlantsLevel;
        Debug.Log($">>> {Reference.GameModel.MaxNumberPlants.Value}");
        // var newGrydkas = _gameManager.levelGrydka[level].newGrydka;
        // newGrydkas.ForEach(b =>
        // {
        //     b.gameObject.SetActive(true);
        //     _gameManager.currentGrydka.Add(b);
        // });
        // // _gameManager.mapForest.sprite = _gameManager.levelGrydka[level].sprite;
    }

    public void CheckLevelMap(int level)
    {
        // Debug.Log($"Contrl currentMapIndex {_currentMapIndex}, levelGrydka {_gameManager.levelGrydka.Count}");
        if (_currentMapIndex >= _gameManager.levelGrydka.Count) return;
        _currentCloseOrder = level;
        // var oldIndex = _currentMapIndex > 0 ? _currentMapIndex - 1 : 0;
        if (_currentMapIndex > 0)
        {
            var need = _gameManager.levelGrydka[_currentMapIndex].numberOfOrders -
                       _gameManager.levelGrydka[_currentMapIndex - 1].numberOfOrders;
            var b = _currentCloseOrder - _currentOpenOrder;

            // Debug.Log($"CheckLevelMap1 {b}/{need}");
            _gameManager.MapUprgadeText.text = b + " / " + need;
            float d = (float) ((float) b / (float) (need));
            _gameManager.imageFoerstLevel.DOFillAmount(d, 2).OnComplete(OnLevelLoaded);
        }
        else
        {
            // Debug.Log($"CheckLevelMap2 {level}/{_gameManager.levelGrydka[_currentMapIndex].numberOfOrders}");
            _gameManager.MapUprgadeText.text =
                level + " / " + _gameManager.levelGrydka[_currentMapIndex].numberOfOrders;
            float h = (float) ((float) level / (float) (_gameManager.levelGrydka[_currentMapIndex].numberOfOrders));
            _gameManager.imageFoerstLevel.DOFillAmount(h, 1).OnComplete(OnLevelLoaded);
        }
    }

    private void OnLevelLoaded()
    {
        if (_currentCloseOrder >= _gameManager.levelGrydka[_currentMapIndex].numberOfOrders)
        {
            _gameManager.imageFoerstLevel.fillAmount = 0f;

            _currentMapIndex++;
            _currentOpenOrder = Reference.GameModel.NumberClosedOrders.Value;
            OnLevelChanged(_currentMapIndex);
        }
    }
}