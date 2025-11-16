using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BettingPanel : MonoBehaviour
{
    [SerializeField] private ToggleGroup betToggleGroup;
    [SerializeField] private List<Bet> displayedBets;
    [SerializeField] private List<BetData> betsData;

    private void Awake()
    {
        InitializeBets();
    }

    public void InitializeBets()
    {
        betToggleGroup.SetAllTogglesOff();
        var randomBets = GetRandomUniqueItems(betsData, 3);

        for (int i = 0; i < randomBets.Count; i++)
            displayedBets[i].InitializeBet(randomBets[i], i+1);
    }

    public Bet GetSelectedBet()
    {
        var selectedToggle = betToggleGroup.ActiveToggles().FirstOrDefault();
        return selectedToggle.GetComponent<Bet>();
    }

    public void LockInteraction()
    {
    }

    private List<T> GetRandomUniqueItems<T>(List<T> list, int count)
    {
        return list
            .OrderBy(x => Random.value)
            .Take(count)
            .ToList();
    }
}
