using UnityEngine;

[CreateAssetMenu(fileName = "CostEndCoefficientDTO", menuName = "Scriptable Objects/CostEndCoefficientDTO")]
public class CostEndCoefficientDTO : ScriptableObject
{
    public int ElementaryCostSpeed;
    public float CoefficientCostSpeed;
    public int ElementaryCostSpeedAction;
    public float CoefficientCostSpeedAction;
    public int ElementaryCostAction;
    public float CoefficientCostAction;
}
