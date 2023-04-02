using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarLogic healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taken " + damage + " damage");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }
}
