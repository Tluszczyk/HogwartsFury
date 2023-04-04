using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawnerLogic : MonoBehaviour
{
    public GameObject opponentPrefab;

    public GameObject spawnTerritory;
    private List<GameObject> spawnAreas = new List<GameObject>();

    public void Start() {
        foreach (Transform child in spawnTerritory.transform) {
            if ( child.tag == "OpponentSpawnArea" )
                spawnAreas.Add(child.gameObject);
        }
    }

    public void spawnOpponent()
    {
        int spawnAreaIndex = Random.Range(0, spawnAreas.Count);
        GameObject spawnArea = spawnAreas[spawnAreaIndex];

        Bounds spawnBounds = spawnArea.GetComponent<Renderer>().bounds;
        
        Vector3 spawnPosition = new Vector2(
            Random.Range(spawnBounds.min.x, spawnBounds.max.x),
            Random.Range(spawnBounds.min.y, spawnBounds.max.y)
        );

        GameObject opponent = Instantiate(opponentPrefab, spawnPosition, Quaternion.identity);

        opponent.GetComponent<OpponentMovement>().SetTarget(GameObject.Find("Player").transform);
        opponent.GetComponent<MeleeAttackBehaviour>().SetPlayerHealth(GameObject.Find("Player").GetComponent<HealthBehaviour>());
    }
}
