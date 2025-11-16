using System.Collections;
using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int coinSpriteId;
    [SerializeField] private int maxCoins;
    private int currentCoins;

    void Awake()
    {
        currentCoins = 0;
        UpdateCoins(currentCoins);
    }

    private void UpdateCoins(int coins)
    {
        coinText.text = coins + $"<sprite={coinSpriteId}>";
    }

    public void AddCoins(int amount)
    {
        var newCoinCount = Mathf.Min(currentCoins + amount, maxCoins);
        StartCoroutine(StartCoinUpdateSequence(currentCoins, newCoinCount));
        currentCoins = newCoinCount;
        
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
