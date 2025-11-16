using System.Linq;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int healthSpriteId;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (currentHealth <= 0)
        {
            healthText.text = string.Empty;
            return;
        }

        healthText.text = string.Concat(Enumerable.Repeat($"<sprite={healthSpriteId}>", currentHealth));
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        if (currentHealth < 0) 
            Die();

        UpdateHealth();
    }

    private void Die()
    {
        
    }
}
