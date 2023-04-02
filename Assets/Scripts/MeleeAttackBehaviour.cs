using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBehaviour playerHealth;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Detected collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
        }
    }

}
