using UnityEngine;

[CreateAssetMenu(fileName = "BetData", menuName = "BetData", order = 0)]
public class BetData : ScriptableObject
{
    public int rewardCount;
    public SlotSymbol rewardSymbol;
    public int costCount;
    public SlotSymbol costSymbol;
    public string condition;
    public SlotSymbol conditionSymbol;

}