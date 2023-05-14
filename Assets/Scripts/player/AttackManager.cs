using UnityEngine;
using System;
using System.Collections.Generic;


public class AttackManager : MonoBehaviour
{
    public List<GameObject> spellPrefabList;
    public Spell activeSpell = Spell.FireballRed;

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
        if (Input.GetButton("Fire2"))
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

    public void ChooseSpell(Spell spell)
    {
        activeSpell = spell;
    }

    private void SpawnFireball()
    {
        var spellPrefab = spellPrefabList[ (int)activeSpell ];

        var lastPlayerDirection = playerMovement.LastDirection;
        var spawnPosition = gameObject.transform.localPosition + PLAYER_OFFSET * new Vector3(lastPlayerDirection.x, lastPlayerDirection.y, 0);
        var fireball = Instantiate(spellPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballBehaviour>().Initialize(lastPlayerDirection, activeSpell);
    }
}
