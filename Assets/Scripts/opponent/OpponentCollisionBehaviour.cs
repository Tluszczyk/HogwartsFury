using UnityEngine;
using UnityEngine.UI;
using System;

public class OpponentCollisionBehaviour : MonoBehaviour
{
    private const int ATTACK_DELAY_MS = 1000;
    private const int OPPONENT_SCORE = 10;
    private HealthBehaviour ownHealth;
    private HealthBehaviour playerHealth;
    private ScoreTracker tracker;
    private DateTime lastHit = DateTime.Now;
    private RestartHandler restartHandler;
    private bool isColliding = false;

    public void Init(HealthBehaviour playerHealth, ScoreTracker tracker, RestartHandler restartHandler)
    {
        this.playerHealth = playerHealth;
        this.ownHealth = GetComponent<HealthBehaviour>();
        this.tracker = tracker;
        this.restartHandler = restartHandler;
    }

    void Update() {
        var now = DateTime.Now;
        if (this.isColliding && now > lastHit.AddMilliseconds(ATTACK_DELAY_MS))
        {
            playerHealth.TakeDamage(10);
            lastHit = now;
            restartHandler.CheckDeathCondition();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
            lastHit = DateTime.Now;
            restartHandler.CheckDeathCondition();

            this.isColliding = true;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            var spell = collision.gameObject.GetComponent<FireballBehaviour>().spell;
            var damage = (Damage) Enum.Parse(typeof(Damage), spell.ToString());

            ownHealth.TakeDamage( (int)damage ); 
            
            Destroy(collision.gameObject);

            if (ownHealth.GetHealth() <= 0)
            {
                Destroy(gameObject);
                tracker.UpdateScore(OPPONENT_SCORE);
            }

            if (spell == Spell.FireballBlue) {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                gameObject.GetComponent<OpponentMovement>().SlowDown();
            }

        }
    }


    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     var now = DateTime.Now;
    //     if (collision.gameObject.CompareTag("Player") && now > lastHit.AddMilliseconds(ATTACK_DELAY_MS))
    //     {
    //         playerHealth.TakeDamage(10);
    //         lastHit = now;
    //     }
    // }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.isColliding = false;
        }
    }
}
