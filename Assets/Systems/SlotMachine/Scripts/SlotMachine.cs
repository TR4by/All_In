using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public UnityEvent OnRollingStarted;
    public UnityEvent<List<SymbolType>> OnRollingFinished;

    [SerializeField] private SlotCyllinder[] slotCyllinders;
    [SerializeField] private Button spinButton;

    List<SymbolType> results = new();

    private void OnEnable()
    {
        spinButton.onClick.AddListener(StartSpinSequence);
    }

    private void OnDisable()
    {
        spinButton.onClick.RemoveListener(StartSpinSequence);
    }

    private void StartSpinningAllCyllinders()
    {
        foreach (var slotCyllinder in slotCyllinders)
            slotCyllinder.StartSpinning();
    }

    private void StartSpinSequence()
    {
        if (GameManager.Instance.BetChosen)
            StartCoroutine(SpinSequence());
    }
    
    IEnumerator SpinSequence()
    {
        OnRollingStarted?.Invoke();
        results.Clear();
        StartSpinningAllCyllinders();

        foreach (var slotCyllinder in slotCyllinders)
        {
            yield return new WaitForSeconds(1f);
            results.Add(slotCyllinder.StopSpinning());
        }
        Debug.Log("Spin results: " + string.Join(", ", results));
        OnRollingFinished?.Invoke(results);
    }
}
