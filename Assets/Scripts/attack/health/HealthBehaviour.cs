using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarLogic healthBar;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}
