using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private SlotCyllinder[] slotCyllinders;
    [SerializeField] private Button spinButton;

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
        StartCoroutine(SpinSequence());
    }
    
    IEnumerator SpinSequence()
    {
        StartSpinningAllCyllinders();

        foreach (var slotCyllinder in slotCyllinders)
        {
            yield return new WaitForSeconds(1f);
            slotCyllinder.StopSpinning();
        }
    }
}
