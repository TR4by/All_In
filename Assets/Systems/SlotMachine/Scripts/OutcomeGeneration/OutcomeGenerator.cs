using System.Collections.Generic;
using UnityEngine;

public class OutcomeGenerator : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float winChance = 0.1f;
    [SerializeField, Range(0f, 1f)] private float deathChance = 0.1f;

    public Outcome GenerateOutcome()
    {
        float roll = Random.Range(0f, 1f);
        if (roll < deathChance)
            return Outcome.Loss;
        else if (roll < deathChance + winChance)
            return Outcome.Win;
        else
            return Outcome.Nothing;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Outcome outcome = GenerateOutcome();
            Debug.Log("Generated Outcome: " + outcome);
        }
    }

    private void GenerateSlotSymbolGroup(Outcome outcome)
    {
    }

    public enum Outcome
    {
        Win,
        Loss,
        Nothing
    }
}
