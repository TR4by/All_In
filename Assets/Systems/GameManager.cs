using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private BettingPanel bettingPanel;
    
    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        slotMachine.OnRollingStarted.AddListener(OnRollingStarted);
        slotMachine.OnRollingFinished.AddListener(OnRollingFinished);
    }

    void OnDisable()
    {
        slotMachine.OnRollingStarted.RemoveListener(OnRollingStarted);
        slotMachine.OnRollingFinished.RemoveListener(OnRollingFinished);
    }

    private void OnRollingStarted()
    {
        // Todo: Disallow clicking anything!
    }

    private void OnRollingFinished(List<SymbolType> rollResult)
    {
        if (EvaluateSelectedBet(rollResult))
            Debug.Log("Player WON the bet!");
        else
            Debug.Log("Player LOST the bet!");
    }

    private bool EvaluateSelectedBet(List<SymbolType> rollResult)
    {
        var currentBet = bettingPanel.GetSelectedBet();
        var result = BetEvaluator.EvaluateBet(currentBet.betData, rollResult);
        return result;
    }
}
