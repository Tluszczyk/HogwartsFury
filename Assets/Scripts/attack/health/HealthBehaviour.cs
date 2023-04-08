using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool destroyOnDeath;
    public HealthBarLogic healthBar;
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (destroyOnDeath && currentHealth <= 0)
        {
            Destroy(self);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}
