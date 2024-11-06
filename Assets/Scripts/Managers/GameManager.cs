using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Characters.Player;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public LayerMask IgnoreMouse; 
    public EnemySpawnManager _enemySpawnManager;
    public UIManager _uiManager;
    public float currentRound, deadEnemies;
    public GameObject playerTower;
    public Player player;
    public GameObject selectedTower;
    public List<GameObject> spawnedEnemies;
    public List<GameObject> spawnedDefenders;
    public float sellCost;
    public bool roundActive = false;
    public List<PathData> pathWaypoints;
    public GameObject EnemyProc;
    public DefenderSpawnManager _defenderSpawnManager;
    public float timer;
    public List<GameObject> Border,towers;
    // Event to notify about weather warning
    public event Action WeatherWarning;

    void Start()
    {
        _defenderSpawnManager = GameObject.FindGameObjectWithTag("DefenderSpawnManager")
            .GetComponent<DefenderSpawnManager>();
        _enemySpawnManager = GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        playerTower = GameObject.FindGameObjectWithTag("Player");
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = playerTower.GetComponent<Player>();
        currentRound = 0;
        deadEnemies = 0;
       // EnemyProc = GameObject.FindGameObjectWithTag("EnemyProc");

        EnemyProc.SetActive(true);

        // Start the weather warning timer
        StartCoroutine(WeatherWarningTimer());
    }

    public void TowerSelected(GameObject chosenTower)
    {
        selectedTower = chosenTower;
    }

    public void SellTower()
    {
        player.currentGold += sellCost;
        
        for (int i = 0; i < spawnedDefenders.Count; i++)
        {
            if (spawnedDefenders[i] == selectedTower)
            {
                for (int j = 0; j < _defenderSpawnManager.tilesSummonedOn.Count; j++)
                {
                    if (selectedTower.transform.position.x == _defenderSpawnManager.tilesSummonedOn[j].transform.position.x)
                    {
                        Debug.Log("Hit sell if");
                        _defenderSpawnManager.tilesSummonedOn[j].GetComponent<DefenderTiles>().hasTower = false;
                        _defenderSpawnManager.tilesSummonedOn.Remove(selectedTower);
                    }
                }
                spawnedDefenders.Remove(selectedTower);
            }
        }
        Destroy(selectedTower);
    }

    public void GameOver()
    {
        _uiManager.gameOverPanel.SetActive(true);
    }

    public void StartRound()
    {
        _enemySpawnManager.StartNewRound();
        roundActive = true;
        foreach (var tower in spawnedDefenders)
        {
            if (tower.name == "BuffTower(Clone)" || tower.name == "MidBuffTower(Clone)" || tower.name == "BigBuffTower(Clone)")
            {
                var buffTowerBrain = tower.GetComponent<BuffTower>();
                buffTowerBrain.FindTowersInRange();
            }
        }
    }

    public void RoundEnd()
    {
        currentRound += 1;
        roundActive = false;
        _uiManager.ActivateRoundStartButton();
    }

    // Coroutine for random weather warning timer
    private IEnumerator WeatherWarningTimer()
    {
        while (true)
        {
            // Wait for a random time between 5 and 60 seconds
            float randomInterval = UnityEngine.Random.Range(5f, 60f);
            timer = randomInterval;
            yield return new WaitForSeconds(randomInterval);

            // Trigger the WeatherWarning event
            WeatherWarning?.Invoke();
            Debug.Log("Weather warning triggered!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(0))  
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast but exclude the ignoreLayer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~IgnoreMouse))
            {
                // Handle the interaction with objects behind the ignored collider
                Debug.Log("Clicked on: " + hit.collider.name);
            }
        }*/
    }
}
