using System.Collections.Generic;
using System.Linq;

class BetEvaluator
{
    public static bool EvaluateBet(BetData betData, List<SymbolType> rollResult)
    {
        int conditionSymbolCount = rollResult.Count(symbol => symbol == betData.conditionSymbol);

        switch (betData.condition)
        {
            case ConditionType.AtLeastOne:
                return conditionSymbolCount >= 1;
            case ConditionType.AtLeastTwo:
                return conditionSymbolCount >= 2;
            case ConditionType.ExactlyOne:
                return conditionSymbolCount == 1;
            case ConditionType.ExactlyTwo:
                return conditionSymbolCount == 2;
            case ConditionType.None:
                return conditionSymbolCount == 0;
            case ConditionType.All:
                return conditionSymbolCount == rollResult.Count;
            default:
                return false;
        }
    }
}