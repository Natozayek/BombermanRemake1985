using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1.0f;
    [Range(0f, 1f)]
    public float ItemSpawnChance = 0.2f;
    public GameObject[] spawnPickup;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if(spawnPickup.Length > 0 && Random.value < ItemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnPickup.Length);
            Instantiate(spawnPickup[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
