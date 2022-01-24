using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public List<Transform> spawnPoints;
    public float spawnRate;

    private float nextSpawn = 0f;    

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
            int indexSpawnPoints = Random.Range(0, spawnPoints.Count);
            Instantiate(enemiesPrefabs[index],spawnPoints[indexSpawnPoints].transform.position ,enemiesPrefabs[index].transform.rotation);
        }
    }

    
}
