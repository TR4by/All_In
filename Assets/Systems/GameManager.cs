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

    [SerializeField] private int requiredCoins = 1000;

    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private BettingPanel bettingPanel;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private CoinDisplay coinDisplay;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private StoryScreensManager endingManager;

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

    public void BlockInteraction(bool state)
    {
        eventSystem.enabled = !state;
    }

    private void HandleOnRollingStarted()
    {
        BlockInteraction(true);
    }

    private void HandleOnRollingFinished(List<SymbolType> rollResult)
    {
        BlockInteraction(false);

        var wasBroke = IsPlayerBroke;
        var currentBet = bettingPanel.GetSelectedBet().betData;
        WasLastRollWon = EvaluateSelectedBet(rollResult, currentBet);
        GiveRewards(WasLastRollWon, currentBet);

        bettingPanel.InitializeBets();

        if (healthDisplay.CurrentHealthCount < 0)
            TriggerDeathEnding();

        HandleIfPlayerBroke(wasBroke);
        HandleIfPlayerWon();
    }

    private void HandleIfPlayerBroke(bool wasBroke)
    {
        if (!wasBroke) return;

        if (healthDisplay.CurrentHealthCount == 0 && coinDisplay.CurrentCoinCount < 0)
            TriggerDeathEnding();
        else if (coinDisplay.CurrentCoinCount < 0)
            healthDisplay.AddHealth(-1);
    }

    private void HandleIfPlayerWon()
    {
        if (coinDisplay.CurrentCoinCount < requiredCoins) return;
        TriggerVictoryEnding();
    }

    private void GiveRewards(bool didWin, BetData currentBet)
    {
        if (didWin)
        {
            if (currentBet.rewardSymbol == SymbolType.Coin)
                coinDisplay.AddCoins(currentBet.rewardCount);
            else if (currentBet.rewardSymbol == SymbolType.Heart)
                healthDisplay.AddHealth(currentBet.rewardCount);
        }
        else
        {
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

    private void TriggerDeathEnding()
    {
        eventSystem.enabled = false;
        endingManager.StartDeathEndingSequence();
    }

    private void TriggerVictoryEnding()
    {
        eventSystem.enabled = false;
        endingManager.StartWinEndingSequence();
    }
}
