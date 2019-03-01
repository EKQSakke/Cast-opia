using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] enemySpawnLocations;
    [SerializeField] int minEnemyAmount;
    [SerializeField] int maxEnemyAmount;
    List<int> usedSpawns = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int enemyAmount = (int)UnityEngine.Random.Range(minEnemyAmount, maxEnemyAmount);
        for (int i = 0; i < enemyAmount; i++)
        {
            int positionRnd = (int)UnityEngine.Random.Range(0, enemySpawnLocations.Length);

            while (usedSpawns.Contains(positionRnd))
            {
                positionRnd = (int)UnityEngine.Random.Range(0, enemySpawnLocations.Length);
            }

            usedSpawns.Add(positionRnd);

            GameObject enemyClone = Instantiate(enemy);
            enemyClone.transform.position 
                = enemySpawnLocations[positionRnd].position;
        }
    }

    

}
