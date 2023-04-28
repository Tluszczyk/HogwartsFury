using UnityEngine;
using System;

public class AttackManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    private PlayerMovement playerMovement;
    private HealthBehaviour playerHealth;
    private float PLAYER_OFFSET = 1.5f;
    private DateTime lastSpell = DateTime.Now;

    public void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerHealth = gameObject.GetComponent<HealthBehaviour>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        var now = DateTime.Now;
        if (now > lastSpell.AddMilliseconds(FireballBehaviour.SPELL_OFFSET_MS) && !playerHealth.isDead())
        {
            SpawnFireball();
            lastSpell = now;
        }
    }

    private void SpawnFireball()
    {
        var lastPlayerDirection = playerMovement.LastDirection;
        var spawnPosition = gameObject.transform.localPosition + PLAYER_OFFSET * new Vector3(lastPlayerDirection.x, lastPlayerDirection.y, 0);
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballBehaviour>().Initialize(lastPlayerDirection);
    }
}
