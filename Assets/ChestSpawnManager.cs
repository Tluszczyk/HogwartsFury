using UnityEngine;
using System;

public class ChestSpawnManager : MonoBehaviour
{
    private DateTime lastChestTime = DateTime.Now;

    void Start() {
    }

    void SpawnChest() {
        // var chest = Instantiate(gameObject, gameObject.transform.localPosition, Quaternion.identity);
        // chest.GetComponent<ChestBehaviour>().Initialize();
    }

    void Update()
    {
        
    }
}
