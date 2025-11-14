using TMPro;
using UnityEngine;

public class Bet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI betText;

    private string GetTextSymbol(SymbolType symbol) => "<sprite=" + (int)symbol + ">";

    public void InitializeBet(BetData betData, int betNumber)
    {
        betText.text = $"Bet {betNumber}\n\nReward: +{betData.rewardCount}{GetTextSymbol(betData.rewardSymbol)}\nPunishment: -{betData.punishmentCount}{GetTextSymbol(betData.punishmentSymbol)}\nCondition: {betData.condition} {GetTextSymbol(betData.conditionSymbol)}";
    }
}
