using UnityEngine;
using System;
using System.Collections.Generic;


public class AttackManager : MonoBehaviour
{
    public List<GameObject> spellPrefabList;
    public Spell activeSpell = Spell.FireballRed;

    public Sprite defaultSprite;
    public Sprite attackSprite;

    public float MAX_SPELL_DURATION = 5000;
    private DateTime chestCollectedTime;

    private PlayerMovement playerMovement;
    private HealthBehaviour playerHealth;
    private SpriteRenderer playerRenderer;
    private float PLAYER_OFFSET = 1.5f;
    private DateTime lastSpell = DateTime.Now;

    public void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerHealth = gameObject.GetComponent<HealthBehaviour>();
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Fire();
        }
        else
        {
            SetDefaultSprite();
        }

        if (activeSpell != Spell.FireballRed && DateTime.Now > chestCollectedTime.AddMilliseconds(MAX_SPELL_DURATION))
        {
            ChooseSpell(Spell.FireballRed);
        }
    }

    public void Fire()
    {
        var now = DateTime.Now;
        if (now > lastSpell.AddMilliseconds(FireballBehaviour.SPELL_OFFSET_MS) && !playerHealth.isDead())
        {
            SpawnFireball();
            lastSpell = now;
            playerRenderer.sprite = attackSprite;
        }
    }

    private void SetDefaultSprite()
    {
        if (DateTime.Now > lastSpell.AddMilliseconds(1.2 * FireballBehaviour.SPELL_OFFSET_MS) && !playerHealth.isDead())
        {
            playerRenderer.sprite = defaultSprite;
        }
    }

    public void ChooseSpell(Spell spell)
    {
        activeSpell = spell;
        chestCollectedTime = DateTime.Now;
    }

    private void SpawnFireball()
    {
        var spellPrefab = spellPrefabList[(int)activeSpell];

        var lastPlayerDirection = playerMovement.LastDirection;
        var spawnPosition = gameObject.transform.localPosition + PLAYER_OFFSET * new Vector3(lastPlayerDirection.x, lastPlayerDirection.y, 0);
        var fireball = Instantiate(spellPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballBehaviour>().Initialize(lastPlayerDirection, activeSpell);
    }
}
