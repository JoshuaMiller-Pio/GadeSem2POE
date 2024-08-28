using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Characters.Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float currentRound;
    public GameObject playerTower;
    public Player player;
    public GameObject selectedTower;
    public List<GameObject> spawnedEnemies;
    public List<GameObject> spawnedDefenders;
    public float sellCost;
    // Start is called before the first frame update
    void Start()
    {
        playerTower = GameObject.FindGameObjectWithTag("Player");
        player = playerTower.GetComponent<Player>();
        currentRound = 1;
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
    public void RoundEnd()
    {
        currentRound += 1;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
