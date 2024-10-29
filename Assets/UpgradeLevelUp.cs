using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLevelUp : MonoBehaviour
{
  public TextMeshProUGUI textLevel;
  public TextMeshProUGUI nameNewPlant;
  public RawImage spriteNewPlant;
[Header("Rewards")]
  public TextMeshProUGUI coins;
  public RawImage SpriteReward1;
  public TextMeshProUGUI countReward1;
  
  public RawImage SpriteReward2;
  public TextMeshProUGUI countReward2;

  private LevelData _levelData;
  
  public void InitPanel(LevelData levelData)
  {
    _levelData = levelData;
    textLevel.text = _levelData.Level + " уровень";
    Plant plant = GameManager.instance.GetPlantToType(_levelData.OpenPlant);
    nameNewPlant.text =plant.namePlant;
    spriteNewPlant.texture = plant.spritePlant[4];
    coins.text = _levelData.CoinReward.ToString();
    
    SpriteReward1.gameObject.SetActive(true);
    SpriteReward1.texture = GameManager.instance.GetPlantToType(levelData.RewardLevelDataPlants[0].RewardPlant).spritePlant[4];
    countReward1.text = levelData.RewardLevelDataPlants[0].QuantityRewardPlant.ToString();

    if (levelData.RewardLevelDataPlants.Count > 1)
    {
      SpriteReward2.gameObject.SetActive(true);
    
      SpriteReward2.texture = GameManager.instance.GetPlantToType(levelData.RewardLevelDataPlants[1].RewardPlant).spritePlant[4];
      countReward2.text = levelData.RewardLevelDataPlants[1].QuantityRewardPlant.ToString();
      // Bag.instance.AddPlants(levelData.RewardLevelDataPlants[1].RewardPlant, levelData.RewardLevelDataPlants[1].QuantityRewardPlant);
    }
  }

  public void ApplyRewards()
  {
    GameManager.instance.LevelUpApply();
    SpriteReward1.gameObject.SetActive(false);
    SpriteReward2.gameObject.SetActive(false);
    gameObject.SetActive(false);
  }
}
