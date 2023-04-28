using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawnerLogic : MonoBehaviour
{
    public GameObject opponentPrefab;

    public GameObject spawnTerritory;
    private List<GameObject> spawnAreas = new List<GameObject>();
    private RestartHandler restartHandler;

    public void Start() {
        this.restartHandler = GameObject.Find("RestartHandler").GetComponent<RestartHandler>();
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

        var player = GameObject.Find("Player");
        opponent.GetComponent<OpponentMovement>().SetTarget(player.transform);
        opponent.GetComponent<OpponentCollisionBehaviour>().Init(player.GetComponent<HealthBehaviour>(), player.GetComponent<ScoreTracker>(), restartHandler);
    }
}
