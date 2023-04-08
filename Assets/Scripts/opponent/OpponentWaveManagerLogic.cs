using UnityEngine;

public class OpponentWaveManagerLogic : MonoBehaviour
{
    public GameObject opponentSpawner;
    
    public float timeBetweenWaves = 5f;

    void Start()
    {
        InvokeRepeating("SpawnWave", 5f, timeBetweenWaves);
    }

    private void SpawnWave()
    {
        opponentSpawner.GetComponent<OpponentSpawnerLogic>().spawnOpponent();
    }
}
