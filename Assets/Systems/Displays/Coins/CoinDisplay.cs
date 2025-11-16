using System.Collections;
using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int coinSpriteId;
    [SerializeField] private int maxCoins;
    public int CurrentCoinCount { get; private set; }

    void Awake()
    {
        CurrentCoinCount = 0;
        UpdateCoins(CurrentCoinCount);
    }

    private void UpdateCoins(int coins)
    {
        var displayedText = coins + $"<sprite={coinSpriteId}>";
        if (coins < 0)
            displayedText = "<color=red>" + displayedText + "</color>";
            
        coinText.text = displayedText;
    }

    public void AddCoins(int amount)
    {
        var newCoinCount = Mathf.Min(CurrentCoinCount + amount, maxCoins);
        StartCoroutine(StartCoinUpdateSequence(CurrentCoinCount, newCoinCount));
        CurrentCoinCount = newCoinCount;
        
    }

    private IEnumerator StartCoinUpdateSequence(int oldCount, int newCount)
    {
        int currentCount;
        var elapsedTime = 0f;
        var duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            currentCount = Mathf.FloorToInt(Mathf.Lerp(oldCount, newCount, t));
            UpdateCoins(currentCount);
            yield return null;
        }
    }
}
