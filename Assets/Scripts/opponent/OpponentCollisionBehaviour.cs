using UnityEngine;
using UnityEngine.UI;
using System;

public class OpponentCollisionBehaviour : MonoBehaviour
{
    public GameObject self;
    private const int ATTACK_DELAY_MS = 1000;
    private const int OPPONENT_SCORE = 10;
    private HealthBehaviour ownHealth;
    private HealthBehaviour playerHealth;
    private ScoreTracker tracker;
    private DateTime lastHit = DateTime.Now;

    public void Init(HealthBehaviour playerHealth, ScoreTracker tracker)
    {
        this.playerHealth = playerHealth;
        this.ownHealth = GetComponent<HealthBehaviour>();
        this.tracker = tracker;
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

            if (ownHealth.GetHealth() <= 0)
            {
                Destroy(self);
                tracker.UpdateScore(OPPONENT_SCORE);
            }
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
