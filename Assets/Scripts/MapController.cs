using UnityEngine;

public class MapController
{
    private GameModel _gameModel;
    private GameManager _gameManager;

    public MapController()
    {
        _gameModel = new GameModel();
        _gameManager = GameManager.instance;
        // _gameModel.LevelMap.Subscribe(OnLevelChanged);
        Debug.Log($"Map Controller initialized");
    }

    public void OnLevelChanged(int level)
    {
        Debug.Log($"Map OnLevelChanged");
        var borders = _gameManager.levelGrydka[level - 1].border;
        borders.ForEach(b => b.gameObject.SetActive(false));

        var newGrydkas = _gameManager.levelGrydka[level].newGrydka;
        newGrydkas.ForEach(b =>
        {
            b.gameObject.SetActive(true);
            _gameManager.currentGrydka.Add(b);
        });
    }
}