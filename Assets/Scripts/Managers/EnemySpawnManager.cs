using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public List<GameObject> spawnPoints;
    public float summonedEnemies, maxSummonedEnemies, currentRound;
    public GameObject Player;
    public Vector3 spawnPosition;
    public static List<GameObject> spawnedObjects = new List<GameObject>();
    public GameManager _gameManager;
    // Update is called once per frame
    public void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentRound = 1;
        spawnedObjects = new List<GameObject>();
        StartCoroutine(SpawnObject());
        spawnPosition = spawnPoints[0].transform.position;
        StartNewRound();
    }

    public void StartNewRound()
    {
        currentRound = _gameManager.currentRound;
        maxSummonedEnemies = 10 + (currentRound * 10) / 2;
        StartCoroutine("SpawnObject");
    }
    void Update()
    {

    }

    public IEnumerator SpawnObject()
    {
        int i = 0;
        for (int j = 0; j < maxSummonedEnemies; j++)
        {
            int objToSpwn = Random.Range(0, enemyPrefabs.Length);
            int spawnRate = Random.Range(1, 4);
            int spawnPoint = Random.Range(0, spawnPoints.Count);
            spawnPosition = spawnPoints[spawnPoint].transform.position;
          
            //Instantiates a new object at the spawn position
            GameObject newEnemy = enemyPrefabs[objToSpwn];
            Instantiate(newEnemy, spawnPosition, Quaternion.identity);
            _gameManager.spawnedEnemies.Add(newEnemy);
           
            i++;
            yield return new WaitForSeconds(spawnRate);
        }
        StopCoroutine("SpawnObject");
    }
}

