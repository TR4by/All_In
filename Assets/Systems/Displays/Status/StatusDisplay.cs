using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private string betMessage = "Place your bets";
    [SerializeField] private string waitMessage = "Spinning";
    [SerializeField] private string winMessage = "You Win";
    [SerializeField] private string loseMessage = "You Lose";
    [SerializeField] private float resultMessageDuration = 1f;
    [SerializeField] private string brokeMessage = "Broke!   Losing  hearts";
    [SerializeField] private float brokeMessageDuration = 1f;

    private string currentMessage;

    void Start()
    {
        UpdateStatus(betMessage);

        GameManager.Instance.OnRollingStarted.AddListener(HandleOnRollingStarted);
        GameManager.Instance.OnRollingFinished.AddListener(HandleRollingFinished);
    }

    void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnRollingStarted.AddListener(HandleOnRollingStarted);
        GameManager.Instance.OnRollingFinished.AddListener(HandleRollingFinished);
    }

    void OnDisable()
    {
        GameManager.Instance.OnRollingStarted.RemoveListener(HandleOnRollingStarted);
        GameManager.Instance.OnRollingFinished.RemoveListener(HandleRollingFinished);
    }

    private void HandleOnRollingStarted()
    {
        UpdateStatus(waitMessage);
    }

    private void HandleRollingFinished(List<SymbolType> symbols)
    {
        UpdateStatus(betMessage);

        if (GameManager.Instance.IsPlayerBroke)
        {
            StartCoroutine(TemporaryStatusSequence(brokeMessage, brokeMessageDuration));
            return;
        }

        if (GameManager.Instance.WasLastRollWon)
            StartCoroutine(TemporaryStatusSequence(winMessage, resultMessageDuration));
        else
            StartCoroutine(TemporaryStatusSequence(loseMessage, resultMessageDuration));
    }

    public void UpdateStatus(string message)
    {
        currentMessage = message;
        statusText.text = message;
    }

    public IEnumerator TemporaryStatusSequence(string message, float duration)
    {
        statusText.text = message;
        yield return new WaitForSeconds(duration);
        statusText.text = currentMessage;
    }
}