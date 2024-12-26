using DG.Tweening;
using UnityEngine;

public class MapController
{
    private GameModel _gameModel;
    private GameManager _gameManager;
    private int _currentMapIndex;
    private int _currentPlayerIndex;

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
        _currentPlayerIndex = level;
        Debug.Log($"CheckLevelMap {level}/{_gameManager.levelGrydka[_currentMapIndex].numberOfOrders}");
        float h = (float)((float)level / (float)(_gameManager.levelGrydka[_currentMapIndex].numberOfOrders));
        _gameManager.imageFoerstLevel.DOFillAmount(h, 2).OnComplete(OnLevelLoaded);
       
    }

    private void OnLevelLoaded()
    {
        if (_currentPlayerIndex >= _gameManager.levelGrydka[_currentMapIndex].numberOfOrders)
        {
            _gameManager.imageFoerstLevel.fillAmount = 0f;
            _currentMapIndex++;
            OnLevelChanged(_currentMapIndex);
        }
    }
}