using UnityEngine;
using System;

public class AttackManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    private GameObject player;
    private PlayerMovement playerMovement;
    private float PLAYER_OFFSET = 1.5f;
    private DateTime lastSpell = DateTime.Now;

    public void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        var now = DateTime.Now;
        if (Input.GetButton("Fire1") && now > lastSpell.AddMilliseconds(FireballBehaviour.SPELL_OFFSET_MS))
        {
            SpawnFireball();
            lastSpell = now;
        }
    }

    private void SpawnFireball()
    {
        var lastPlayerDirection = playerMovement.LastDirection;
        var spawnPosition = player.transform.localPosition + PLAYER_OFFSET * new Vector3(lastPlayerDirection.x, lastPlayerDirection.y, 0);
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballBehaviour>().Initialize(lastPlayerDirection);
    }
}
