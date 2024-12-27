using DG.Tweening;
using UnityEngine;

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
        Debug.Log($"Map Controller initialized");
    }

    public void OnLevelChanged(int level)
    {
        Debug.Log($"Map OnLevelChanged");
        if (level > 0)
        {
            _gameManager.levelGrydka[level - 1].border.ForEach(b => b.gameObject.SetActive(false));
            ;
        }

        _gameManager.levelGrydka[level].border.ForEach(b => b.gameObject.SetActive(true));


        var newGrydkas = _gameManager.levelGrydka[level].newGrydka;
        newGrydkas.ForEach(b =>
        {
            b.gameObject.SetActive(true);
            _gameManager.currentGrydka.Add(b);
        });
        _gameManager.mapForest.sprite = _gameManager.levelGrydka[level].sprite;
    }

    private void CheckLevelMap(int level)
    {
        _currentCloseOrder = level;
        // var oldIndex = _currentMapIndex > 0 ? _currentMapIndex - 1 : 0;
        if (_currentMapIndex > 0)
        {
            var need = _gameManager.levelGrydka[_currentMapIndex].numberOfOrders -
                       _gameManager.levelGrydka[_currentMapIndex - 1].numberOfOrders;
            var b = _currentCloseOrder - _currentOpenOrder;

            // Debug.Log($"CheckLevelMap1 {b}/{need}");
            float d = (float)((float)b / (float)(need));
            _gameManager.imageFoerstLevel.DOFillAmount(d, 2).OnComplete(OnLevelLoaded);
        }
        else
        {
            // Debug.Log($"CheckLevelMap2 {level}/{_gameManager.levelGrydka[_currentMapIndex].numberOfOrders}");
            float h = (float)((float)level / (float)(_gameManager.levelGrydka[_currentMapIndex].numberOfOrders));
            _gameManager.imageFoerstLevel.DOFillAmount(h, 2).OnComplete(OnLevelLoaded);
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