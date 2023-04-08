using UnityEngine;
using System;

public class OpponentCollisionBehaviour : MonoBehaviour
{
    private const int ATTACK_DELAY_MS = 1000;
    private HealthBehaviour ownHealth;
    private HealthBehaviour playerHealth;
    private DateTime lastHit = DateTime.Now;

    public void Init(HealthBehaviour playerHealth)
    {
        this.playerHealth = playerHealth;
        this.ownHealth = GetComponent<HealthBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
            lastHit = DateTime.Now;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            ownHealth.TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        var now = DateTime.Now;
        if (collision.gameObject.CompareTag("Player") && now > lastHit.AddMilliseconds(ATTACK_DELAY_MS))
        {
            playerHealth.TakeDamage(10);
            lastHit = now;
        }
    }
}
