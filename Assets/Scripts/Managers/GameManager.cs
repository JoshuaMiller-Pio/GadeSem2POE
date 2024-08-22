using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float currentRound;
    public GameObject playerTower;
    public Player player;
    public GameObject selectedTower;
    public List<GameObject> spawnedEnemies;
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
    public void RoundEnd()
    {
        currentRound += 1;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
