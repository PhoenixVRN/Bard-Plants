using DG.Tweening;
using UnityEngine;

public class MapController
{
    private GameModel _gameModel;
    private GameManager _gameManager;
    public int _currentMapIndex;
    public int _currentCloseOrder;
    public int _currentOpenOrder;

    public MapController()
    {
        _currentMapIndex = 0;
        _gameModel = new GameModel();
        _gameManager = GameManager.instance;
        Reference.GameModel.NumberClosedOrders.Subscribe(CheckLevelMap);
        // Reference.GameModel.LoadGameComplited.Subscribe(InitStart);
        // _gameModel.LevelMap.Subscribe(OnLevelChanged);
        // Debug.Log($"Map Controller initialized");
    }

    public void InitStart(bool starDefault)
    {
        float fil = 0;
        Debug.Log($"Init Start MapController {starDefault}");
        if (starDefault)
        {
            _gameManager.MapUprgadeText.text = 0 + " / " + _gameManager.levelGrydka[0].numberOfOrders;
            Reference.GameModel.MaxNumberPlants.Value = _gameManager.levelGrydka[0].numberPlantsLevel;
        }
        else
        {
            Debug.Log($"{_gameManager.levelGrydka.Count}/{_currentMapIndex}");
            if (_currentMapIndex == 0)
            {
                _gameManager.MapUprgadeText.text = _currentCloseOrder + " / " +
                                                   _gameManager.levelGrydka[_currentMapIndex].numberOfOrders;
                fil = (float)_currentCloseOrder / (float)_gameManager.levelGrydka[_currentMapIndex].numberOfOrders;
            }
            else
            {
                var need = _gameManager.levelGrydka[_currentMapIndex].numberOfOrders -
                           _gameManager.levelGrydka[_currentMapIndex - 1].numberOfOrders;
                var b = _currentCloseOrder - _currentOpenOrder;

                _gameManager.MapUprgadeText.text = b + " / " + need;
                fil = (float) ((float) b / (float) (need));
            }
            FildAmmout(fil);
            // CheckLevelMap(Reference.GameModel.NumberClosedOrders.Value);
        }
        // Debug.Log($"NumberClosedOrders {_gameManager.levelGrydka.Count}/{Reference.GameModel.NumberClosedOrders.Value}");
        // Reference.GameModel.MaxNumberPlants.Value = _gameManager.levelGrydka[Reference.GameModel.NumberClosedOrders.Value].numberPlantsLevel;
    }

    public void OnLevelChanged(int level)
    {
        if (level > 0)
        {
            _gameManager.MapUprgadeText.text = 0 + " / " + (_gameManager.levelGrydka[_currentMapIndex].numberOfOrders -
                                                            _gameManager.levelGrydka[_currentMapIndex - 1]
                                                                .numberOfOrders);
            var allgroup = _gameManager.levelGrydka[level].forestGroup;
            foreach (Transform child in allgroup.transform)
            {
                // Debug.Log("Дочерний объект (первый уровень): " + child.name);
                child.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => _gameManager.DestroyForest(child));
            }
        }

        Reference.GameModel.MaxNumberPlants.Value = _gameManager.levelGrydka[level].numberPlantsLevel;
    }

    public void CheckLevelMap(int level)
    {
        // Debug.Log($"CheckLevelMap");
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
            FildAmmout(d);
            // _gameManager.imageFoerstLevel.DOFillAmount(d, 1).OnComplete(OnLevelLoaded);
        }
        else
        {
            // Debug.Log($"CheckLevelMap2 {level}/{_gameManager.levelGrydka[_currentMapIndex].numberOfOrders}");
            _gameManager.MapUprgadeText.text =
                level + " / " + _gameManager.levelGrydka[_currentMapIndex].numberOfOrders;
            float h = (float) ((float) level / (float) (_gameManager.levelGrydka[_currentMapIndex].numberOfOrders));
            FildAmmout(h);
            // _gameManager.imageFoerstLevel.DOFillAmount(h, 1).OnComplete(OnLevelLoaded);
        }
    }

    private void FildAmmout(float value)
    {
        _gameManager.imageFoerstLevel.DOFillAmount(value, 1).OnComplete(OnLevelLoaded);
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

    private void OnApplicationQuit()
    {
        Debug.Log("Приложение закрывается2.");
        Debug.Log(
            $"currentMapIndex {_currentMapIndex}, currentCloseOrder {_currentCloseOrder}, currentOpenOrder {_currentOpenOrder}");
    }
    // async Task ExecuteAfterDelay()
    // {
    //     await Task.Delay(2000);
    //     _gameManager.ShowUpgradeLevelPanel();
    // }
}