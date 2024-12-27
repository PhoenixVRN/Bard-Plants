using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePlant : MonoBehaviour
{
    public TextMeshProUGUI namePlants;
    public TextMeshProUGUI countPlants;
    public TextMeshProUGUI count;
    public TextMeshProUGUI cost;
    public Image upgradeIcon;
    private Plant _plant;
    private GameManager _gameManager;

    public void Init(Plant plant)
    {
        _gameManager = GameManager.instance;
        _plant = plant;
        var texture = plant.spritePlant[4];
        upgradeIcon.sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        namePlants.text = plant.namePlant;
        countPlants.text = "X" + plant.quantity.Value;
        count.text = (plant.Level + 1).ToString();
        CheckMony(_gameManager.coin.Value);
        _gameManager.coin.Subscribe(CheckMony);
        cost.text = ((plant.Level + 1) * 200).ToString();
    }

    // TODO реализовать проверку на достаточности средсв на улучшение

    public void ButtonClick()
    {
        if (_gameManager.coin.Value >= ((_plant.Level + 1) * 200) && _plant.Level < 11)
        {
            _gameManager.coin.Value -= ((_plant.Level + 1) * 200);
            _plant.Level++;
            countPlants.text = "X" + _plant.quantity.Value;
            count.text = (_plant.Level + 1).ToString();
            cost.text = ((_plant.Level + 1) * 200).ToString();
            Debug.Log($"Button clicked");
        }
    }

    private void CheckMony(int mony)
    {
        if (mony >= (_plant.Level + 1) * 200)
        {
            cost.color = Color.white;
        }
        else
        {
            cost.color = Color.red;
        }
    }
}