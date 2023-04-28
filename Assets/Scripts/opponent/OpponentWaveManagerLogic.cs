using UnityEngine;

public class OpponentWaveManagerLogic : MonoBehaviour
{
    public GameObject opponentSpawner;
    public GameObject player;
    private HealthBehaviour playerHealth;
    public float timeBetweenWaves = 5f;

    void Start()
    {
        playerHealth = player.GetComponent<HealthBehaviour>();
        InvokeRepeating("SpawnWave", 5f, timeBetweenWaves);
    }

    private void SpawnWave()
    {
        if (!playerHealth.isDead())
        {
            opponentSpawner.GetComponent<OpponentSpawnerLogic>().spawnOpponent();
        }
    }
}
