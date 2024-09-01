using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public MeshGenV2 _meshGenV2;
    public GameObject[] enemyPrefabs;
    public List<Vector3> spawnPoints;
    public float summonedEnemies, maxSummonedEnemies, currentRound;
    public GameObject Player;
    public Vector3 spawnPosition;
    public static List<GameObject> spawnedObjects = new List<GameObject>();
    public GameManager _gameManager;
    public int spawnPoint, objToSpwn;
    private float  spawnRate;
    // Update is called once per frame
    public void Start()
    {
        _meshGenV2 = GameObject.FindGameObjectWithTag("Terrain").GetComponent<MeshGenV2>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentRound = 1;
        spawnedObjects = new List<GameObject>();
        
        
       
    }

    public void FindSpawnTiles()
    {
       /* for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _gameManager.pathWaypoints[i].positions.Count; j++)
            {
                var temp = _gameManager.pathWaypoints[i].positions[j];
                if (temp == _gameManager.pathWaypoints[i].positions[17])
                {
                    spawnPoints[i] = temp;
                }
            }
            
            
            
        }*/
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
            //todo when adding in more enemy types use random range to select them
             objToSpwn = 0;
             spawnRate = Random.Range(1, 4);
             spawnPoint = Random.Range(0, spawnPoints.Count);
             spawnPosition = spawnPoints[spawnPoint];
          
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

