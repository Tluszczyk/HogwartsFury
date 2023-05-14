using UnityEngine;
using System;

public class ChestSpawnManager : MonoBehaviour
{
    private DateTime lastChestTime = DateTime.Now;
    public float chestSpawnTime = 1f;

    public GameObject chestPrefab;

    private Vector2 topLeft;
    private Vector2 bottomRight;

    void Start()
    {
        topLeft     = transform.position - transform.localScale / 2;
        bottomRight = transform.position + transform.localScale / 2;
    }

    void SpawnChest() {
        Vector2 spawnPosition = new Vector2(
            UnityEngine.Random.Range(topLeft.x, bottomRight.x),
            UnityEngine.Random.Range(bottomRight.y, topLeft.y)
        );

        var chest = Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }

    void Update()
    {
        if (DateTime.Now > lastChestTime.AddSeconds(chestSpawnTime))
        {
            SpawnChest();
            lastChestTime = DateTime.Now;
        }
    }
}
