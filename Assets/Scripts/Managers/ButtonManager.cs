using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameManager _gameManager;

    public DefenderSpawnManager defenderSpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        defenderSpawnManager = GameObject.FindGameObjectWithTag("DefenderSpawnManager")
            .GetComponent<DefenderSpawnManager>();
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

    public void PurchaseTower()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
