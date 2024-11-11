using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;

public class DefenderSpawnManager : MonoBehaviour
{
    public GameObject[] defenderPrefabs;
    public GameObject selectedTile;
    public Vector3 spawnPoint;
    public UIManager _uiManager;
    public GameManager _gameManager;
    public DefenderTiles selectedTileBrain;
    public List<GameObject> tilesSummonedOn;
    public enum DefenderType
    {
        Basic,
        MidBasic,
        LargeBasic,
        Debuff,
        MidDebuff,
        LargeDebuff,
        Aoe,
        MidAoe,
        LargeAoe
    }

    public DefenderType selectedDefenderType;

    //Sets the spawn point to the selected tile and opens the shop panel
    public void SelectTile(GameObject clickedTile)
    {
        selectedTile = clickedTile;
        selectedTileBrain = selectedTile.GetComponent<DefenderTiles>();
        if (selectedTileBrain.hasTower == false)
        {
            spawnPoint = selectedTile.transform.position;
            spawnPoint.y += 1;
            _uiManager.shopPanel.SetActive(true);
        }
        
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
               selectedTileBrain.hasTower = true;
                 tilesSummonedOn.Add(selectedTile);
                break;
            case DefenderType.Debuff:
                 newTower = Instantiate(defenderPrefabs[3], spawnPoint, Quaternion.identity);
                 _gameManager.spawnedDefenders.Add(newTower);
                 selectedTileBrain.hasTower = true;
                 tilesSummonedOn.Add(selectedTile);
                break;
            case DefenderType.Aoe:
                newTower = Instantiate(defenderPrefabs[6], spawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                selectedTileBrain.hasTower = true;
                tilesSummonedOn.Add(selectedTile);
                break;
            default: break;
            
        }

       

    }

    //This method is used to invoke the destruction of the existing defender tower and the instantiation of the upgraded version in its same location
    public void UpgradeTower(GameObject SelectedTower,DefenderType upgradeType)
    {
        
        Vector3 upgradeSpawnPoint = SelectedTower.transform.position;
        GameObject newTower;
        switch (upgradeType)
        {
                
            case DefenderType.Basic:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[1], upgradeSpawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.MidBasic:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[2], upgradeSpawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.Debuff:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[4], upgradeSpawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.MidDebuff:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[5], upgradeSpawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.Aoe:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[7], upgradeSpawnPoint, Quaternion.identity);
                _gameManager.spawnedDefenders.Add(newTower);
                break;
            case DefenderType.MidAoe:
                _gameManager.spawnedDefenders.Remove(SelectedTower);
                Destroy(SelectedTower);
                newTower = Instantiate(defenderPrefabs[8], upgradeSpawnPoint, Quaternion.identity);
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
