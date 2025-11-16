using System.Linq;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int healthSpriteId;
    [SerializeField] private int maxHealth;
    public int CurrentHealthCount { get; private set; }

    void Awake()
    {
        CurrentHealthCount = maxHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (CurrentHealthCount <= 0)
        {
            healthText.text = string.Empty;
            return;
        }

        healthText.text = string.Concat(Enumerable.Repeat($"<sprite={healthSpriteId}>", CurrentHealthCount));
    }

    public void AddHealth(int amount)
    {
        CurrentHealthCount = Mathf.Min(CurrentHealthCount + amount, maxHealth);
        if (CurrentHealthCount < 0) 
            Die();

        UpdateHealth();
    }

    private void Die()
    {
        
    }
}
