using UnityEngine;

[CreateAssetMenu(fileName = "BetData", menuName = "BetData", order = 0)]
public class BetData : ScriptableObject
{
    public SymbolType rewardSymbol;
    public int rewardCount;
    [Space]
    public SymbolType punishmentSymbol;
    public int punishmentCount;
    [Space]
    public SymbolType conditionSymbol;
    public ConditionType condition;
}