using UnityEngine;

public class VisualCostUpgradeHandler : MonoBehaviour
{
    public GameObject arrowUpgrade;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameManager.instance;
        _gameManager.coin.Subscribe(CheckCostToUpgrade);
        CheckCostToUpgrade(_gameManager.coin.Value);
    }

    private void CheckCostToUpgrade(int coin)
    {
        Debug.Log($"Cost to upgrade: {coin}");
       var costToPlant =  _gameManager.openPlants.Find(pl => (pl.Level + 1) * 200  <= coin);
       if (costToPlant != null)
       {
           arrowUpgrade.SetActive(true);
       }
       else
       {
           arrowUpgrade.SetActive(false);
       }
    }
}
