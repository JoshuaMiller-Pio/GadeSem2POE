using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnManager : MonoBehaviour
{
    public GameObject[] defenderPrefabs;
    public GameObject selectedTile;
    public Vector3 spawnPoint;
    public UIManager _uiManager;
    public GameManager _gameManager;
    public enum DefenderType
    {
        Basic,
        Debuff,
        Aoe
    }

    public DefenderType selectedDefenderType;

    //Sets the spawn point to the selected tile and opens the shop panel
    public void SelectTile(GameObject clickedTile)
    {
        selectedTile = clickedTile;
        spawnPoint = selectedTile.transform.position;
        spawnPoint.y += 1;
        _uiManager.shopPanel.SetActive(true);
    }

    //Creates a prefab of the selected tower at the chosen spawn point
    public void SpawnPurchasedTower()
    {
        GameObject newTower;
        switch (selectedDefenderType)
        {
                
            case DefenderType.Basic:
                 newTower = Instantiate(defenderPrefabs[0], spawnPoint, Quaternion.identity);
               _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.Debuff:
                 newTower = Instantiate(defenderPrefabs[1], spawnPoint, Quaternion.identity);
                 _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.Aoe:
                newTower = Instantiate(defenderPrefabs[2], spawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            default: break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
