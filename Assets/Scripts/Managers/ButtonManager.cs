using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameManager _gameManager;
    public UIManager _uiManager;
    public DefenderSpawnManager defenderSpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        defenderSpawnManager = GameObject.FindGameObjectWithTag("DefenderSpawnManager")
            .GetComponent<DefenderSpawnManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void SelectBasicTowerToSummon()
    {
        defenderSpawnManager.selectedDefenderType = DefenderSpawnManager.DefenderType.Basic;
    }
    
    public void SelectDebuffTowerToSummon()
    {
        defenderSpawnManager.selectedDefenderType = DefenderSpawnManager.DefenderType.Debuff;
    }
    
    public void SelectAoeTowerToSummon()
    {
        defenderSpawnManager.selectedDefenderType = DefenderSpawnManager.DefenderType.Aoe;
    }

    public void SellTower()
    {
       _gameManager.SellTower();
    }
    public void PurchaseTower()
    {
        defenderSpawnManager.SpawnPurchasedTower();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
