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
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        var now = DateTime.Now;
        if (now > lastSpell.AddMilliseconds(FireballBehaviour.SPELL_OFFSET_MS))
        {
            SpawnFireball();
            lastSpell = now;
        }
    }

    private void SpawnFireball()
    {
        if (player == null) {
        Debug.Log("HIIIII");
        }

        if (playerMovement == null) {
        Debug.Log("HIIIII2");
        }
        var lastPlayerDirection = playerMovement.LastDirection;
        var spawnPosition = player.transform.localPosition + PLAYER_OFFSET * new Vector3(lastPlayerDirection.x, lastPlayerDirection.y, 0);
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballBehaviour>().Initialize(lastPlayerDirection);
    }
}
