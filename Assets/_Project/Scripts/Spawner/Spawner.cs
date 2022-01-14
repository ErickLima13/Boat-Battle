using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public float spawnRate;

    private float nextSpawn = 0f;
    private float xRange = 22;
    private float ySpawnPos = 6;

    private void Initialization()
    {
        spawnRate = GameManager.instance.spawnRate;
    }

    private void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameActive)
        {
            
            SpawnEnemies();
        }
        
    }

    private void SpawnEnemies()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            int index = Random.Range(0, enemiesPrefabs.Count);
            Instantiate(enemiesPrefabs[index],transform.position = RandomSpawnPos(),enemiesPrefabs[index].transform.rotation);
        }
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), Random.Range(-ySpawnPos, ySpawnPos));

    }
}
