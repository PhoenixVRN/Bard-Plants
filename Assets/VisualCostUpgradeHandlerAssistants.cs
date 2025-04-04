using UnityEngine;

public class VisualCostUpgradeHandlerAssistants : MonoBehaviour
{
    public GameObject upgradeArrow;
    public CostEndCoefficientDTO CostUpgradeGarden;
    public CostEndCoefficientDTO CostUpgradeCollector;
    public CostEndCoefficientDTO CostUpgradeMusicHelper;
    private GameManager _gameManager;
    private GameModel _gameModel;
    private bool _isfuul;


    void Start()
    {
        _gameModel = Reference.GameModel;
        _gameManager = GameManager.instance;
        _gameManager.coin.Subscribe(CheckCostToUpgrade);
        CheckCostToUpgrade(_gameManager.coin.Value);
    }


    private void CheckCostToUpgrade(int coin)
    {
        _isfuul = false;
        Debug.Log($"Cost to upgrade: {coin}");
        if (_gameModel.GardenGnome.Value)
        {
            CostCalculation(coin, _gameModel.GardenGnomeLevel.Value.lvlSpeed, CostUpgradeGarden.ElementaryCostSpeed, CostUpgradeGarden.CoefficientCostSpeed);
            CostCalculation(coin, _gameModel.GardenGnomeLevel.Value.lvlActions, CostUpgradeGarden.ElementaryCostSpeedAction, CostUpgradeGarden.CoefficientCostSpeedAction);
            CostCalculation(coin, _gameModel.GardenGnomeLevel.Value.lvlStartAction, CostUpgradeGarden.ElementaryCostAction, CostUpgradeGarden.CoefficientCostAction);
            
            CostCalculation(coin, _gameModel.CollectorGnomeLevel.Value.lvlSpeed, CostUpgradeCollector.ElementaryCostSpeed, CostUpgradeCollector.CoefficientCostSpeed);
            CostCalculation(coin, _gameModel.CollectorGnomeLevel.Value.lvlActions, CostUpgradeCollector.ElementaryCostSpeedAction, CostUpgradeCollector.CoefficientCostSpeedAction);
            CostCalculation(coin, _gameModel.CollectorGnomeLevel.Value.lvlStartAction, CostUpgradeCollector.ElementaryCostAction, CostUpgradeCollector.CoefficientCostAction);
            
            CostCalculation(coin, _gameModel.MusicHelpersLevel.Value.lvlSpeed, CostUpgradeMusicHelper.ElementaryCostSpeed, CostUpgradeMusicHelper.CoefficientCostSpeed);
            CostCalculation(coin, _gameModel.MusicHelpersLevel.Value.lvlActions, CostUpgradeMusicHelper.ElementaryCostSpeedAction, CostUpgradeMusicHelper.CoefficientCostSpeedAction);
            CostCalculation(coin, _gameModel.MusicHelpersLevel.Value.lvlStartAction, CostUpgradeMusicHelper.ElementaryCostAction, CostUpgradeMusicHelper.CoefficientCostAction);
        }
        upgradeArrow.SetActive(_isfuul);
    }

    private void CostCalculation(int coin, int level, int s, float coofficient)
    {
        for (int i = 1; i < level; i++)
        {
            s = (int)(s * coofficient);
        }

        if (coin >= s)
        {
            _isfuul = true;
        }
    }
}