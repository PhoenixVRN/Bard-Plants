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
        if (level > 0)
        {
            _gameManager.levelGrydka[level - 1].border.ForEach(b => b.gameObject.SetActive(false));;
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
}