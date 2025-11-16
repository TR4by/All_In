using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnityEvent OnRollingStarted => slotMachine.OnRollingStarted;
    public UnityEvent<List<SymbolType>> OnRollingFinished => slotMachine.OnRollingFinished;
    public bool WasLastRollWon { get; private set; }

    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private BettingPanel bettingPanel;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private CoinDisplay coinDisplay;
    
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
        //bettingPanel.LockInteraction();
        //slotMachine.LockInteraction();
    }

    private void HandleOnRollingFinished(List<SymbolType> rollResult)
    {
        var currentBet = bettingPanel.GetSelectedBet().betData;
        WasLastRollWon = EvaluateSelectedBet(rollResult, currentBet);
        GiveRewards(WasLastRollWon, currentBet);

        bettingPanel.InitializeBets();
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
}
