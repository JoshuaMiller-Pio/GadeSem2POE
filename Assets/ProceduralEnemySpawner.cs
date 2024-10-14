using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Characters.Player;
using UnityEngine;

public class ProceduralEnemySpawner : MonoBehaviour
{
    public GameManager gameManager;
    public float currentRound;
    public float playerHealth;
    public GameObject detector;
    public List<detectTowers> Lanes;
    public List<int> LaneTtowercount ;
    

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager = GameManager.Instance;

       
        
        
        // Initialize procedural parameters
        currentRound = gameManager.currentRound;
        playerHealth = gameManager.player.currentHealth;
        
        for (int i = 0; i < 3; i++)
        {
            
            Lanes.Add(Instantiate(detector, gameManager._enemySpawnManager.spawnPoints[i], Quaternion.identity).GetComponent<detectTowers>());
        }
        
        
        StartProceduralRound();
    }

    // Start a new round with procedural adjustments
    public void StartProceduralRound()
    {
        UpdatePlayerMetrics();
        
        CategorizeTowersByLane();
        
        AdjustSpawnParameters();

        gameManager.StartRound();
    }

    // Categorize towers by lane to adjust enemy spawning
    private void CategorizeTowersByLane()
    {
        for (int i = 0; i < Lanes.Count; i++)
        {
            if (LaneTtowercount.Count <3)
            {
                LaneTtowercount.Add(Lanes[i].towers.Count);

            }
            else
            {
                LaneTtowercount[i] = Lanes[i].towers.Count;
            }

            Debug.Log(0);
        }

        UpdatePlayerMetrics();
    }

   

    // Adjust spawn parameters based on towers in each lane
    private void AdjustSpawnParameters()
    {
        int laneIndex = -1;
        foreach (var lane in Lanes)
        {
            laneIndex++;
            bool hasBombTower = false;
            bool hasBasicTower = false;
            bool hasBuffTower = false;

             //Check what types of towers are present in the lane
            
                if (lane.AOE > 0)
                    hasBombTower = true;
                if (lane.BASIC > 0)
                    hasBasicTower = true;
                if (lane.DEBUFF > 0)
                    hasBuffTower = true;
            

            // Adjust spawns based on tower presence
            if (hasBombTower)
            {
                // Spawn more basic enemies in this lane, since they're weak to bombs
                SpawnEnemiesForLane(laneIndex, "basic");
            }
            else if (hasBasicTower)
            {
                // Spawn more ranged enemies, weak to basic attacks
                SpawnEnemiesForLane(laneIndex, "ranged");
            }
            else if (hasBuffTower)
            {
                // Spawn more tanks, weak to buffed attacks
                SpawnEnemiesForLane(laneIndex, "tank");
            }
        }
    }

    private void SpawnEnemiesForLane(int lane, string enemyTypeWeakness)
    {
        switch (enemyTypeWeakness)
        {
            case "basic":
                gameManager._enemySpawnManager.objToSpwn = 0; 
                break;
            case "ranged":
                gameManager._enemySpawnManager.objToSpwn = 1;  
                break;
            case "tank":
                gameManager._enemySpawnManager.objToSpwn = 2;  
                break;
        }

        gameManager._enemySpawnManager.spawnPoint = lane; 
        StartCoroutine(gameManager._enemySpawnManager.SpawnObject());
    }

    // Updates player metrics and dynamically changes difficulty
    public void UpdatePlayerMetrics()
    {
        currentRound = gameManager.currentRound;
        playerHealth = gameManager.player.currentHealth;

        // Apply spawn adjustments based on updated metrics
        AdjustSpawnParameters();
    }
}
