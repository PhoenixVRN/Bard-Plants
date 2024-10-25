using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CfgLevelData : MonoBehaviour
{
    public List<LevelData> AllLevelData;
}

[Serializable]
public class LevelData
{
    public int Level;
    public int NumberCompletedOrders;
    public ETypePlant OpenPlant;
    public int CoinReward;
    public List<RewardLevelDataPlants> RewardLevelDataPlants;
}

[Serializable]
public class RewardLevelDataPlants
{
    public ETypePlant RewardPlant;
    public int QuantityRewardPlant;
}