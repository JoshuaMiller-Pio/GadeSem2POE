using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        _uiManager.ActivatePurchaseButton();
    }
    
    public void SelectDebuffTowerToSummon()
    {
        defenderSpawnManager.selectedDefenderType = DefenderSpawnManager.DefenderType.Debuff;
        _uiManager.ActivatePurchaseButton();
    }
    
    public void SelectAoeTowerToSummon()
    {
        defenderSpawnManager.selectedDefenderType = DefenderSpawnManager.DefenderType.Aoe;
        _uiManager.ActivatePurchaseButton();
    }

    public void SellTower()
    {
       _gameManager.SellTower();
    }
    public void PurchaseTower()
    {
        if (_gameManager.player.currentGold >= 5 &&
            (defenderSpawnManager.selectedDefenderType == DefenderSpawnManager.DefenderType.Basic))
        {
            _gameManager.player.currentGold -= 5;
            defenderSpawnManager.SpawnPurchasedTower();
            _uiManager.DeActivtePurchaseButton();
        }
       
    }

    public void StartRound()
    {
        _gameManager.StartRound();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
