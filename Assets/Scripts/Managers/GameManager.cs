using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Characters.Player;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private EnemySpawnManager _enemySpawnManager;
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
    // Start is called before the first frame update
    void Start()
    {
        _enemySpawnManager = GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        playerTower = GameObject.FindGameObjectWithTag("Player");
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = playerTower.GetComponent<Player>();
        currentRound = 0;
        deadEnemies = 0;
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
    }
    public void RoundEnd()
    {
        currentRound += 1;
        roundActive = false;
        _uiManager.ActivateRoundStartButton();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
