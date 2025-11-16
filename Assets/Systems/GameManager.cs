using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnityEvent OnRollingStarted => slotMachine.OnRollingStarted;
    public UnityEvent<List<SymbolType>> OnRollingFinished => slotMachine.OnRollingFinished;

    public bool IsPlayerBroke => coinDisplay.CurrentCoinCount < 0;
    public bool WasLastRollWon { get; private set; }
    public bool BetChosen => bettingPanel.GetSelectedBet() != null;

    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private BettingPanel bettingPanel;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private CoinDisplay coinDisplay;
    [SerializeField] private EventSystem eventSystem;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        slotMachine.OnRollingStarted.AddListener(HandleOnRollingStarted);
        slotMachine.OnRollingFinished.AddListener(HandleOnRollingFinished);
    }

    void OnDisable()
    {
        slotMachine.OnRollingStarted.RemoveListener(HandleOnRollingStarted);
        slotMachine.OnRollingFinished.RemoveListener(HandleOnRollingFinished);
    }

    private void HandleOnRollingStarted()
    {
        eventSystem.enabled = false;
    }

    private void HandleOnRollingFinished(List<SymbolType> rollResult)
    {
        eventSystem.enabled = true;

        var wasBroke = IsPlayerBroke;
        var currentBet = bettingPanel.GetSelectedBet().betData;
        WasLastRollWon = EvaluateSelectedBet(rollResult, currentBet);
        GiveRewards(WasLastRollWon, currentBet);

        bettingPanel.InitializeBets();

        if (healthDisplay.CurrentHealthCount < 0)
            KillPlayer();

        HandlePlayerBroke(wasBroke);
    }

    private void HandlePlayerBroke(bool wasBroke)
    {
        if (!wasBroke) return;

        if (healthDisplay.CurrentHealthCount == 0 && coinDisplay.CurrentCoinCount < 0)
            KillPlayer();
        else if (coinDisplay.CurrentCoinCount < 0)
            healthDisplay.AddHealth(-1);
    }

    private void GiveRewards(bool didWin, BetData currentBet)
    {
        if (didWin)
        {
            Debug.Log("Won!");
            if (currentBet.rewardSymbol == SymbolType.Coin)
                coinDisplay.AddCoins(currentBet.rewardCount);
            else if (currentBet.rewardSymbol == SymbolType.Heart)
                healthDisplay.AddHealth(currentBet.rewardCount);
        }
        else
        {
            Debug.Log("Lost!");
            if (currentBet.punishmentSymbol == SymbolType.Coin)
                coinDisplay.AddCoins(-currentBet.punishmentCount);
            else if (currentBet.punishmentSymbol == SymbolType.Heart)
                healthDisplay.AddHealth(-currentBet.punishmentCount);
        }
    }

    private bool EvaluateSelectedBet(List<SymbolType> rollResult, BetData currentBet)
    {
        var result = BetEvaluator.EvaluateBet(currentBet, rollResult);
        return result;
    }

    private void KillPlayer()
    {
        Debug.Log("Player Died");
    }
}
