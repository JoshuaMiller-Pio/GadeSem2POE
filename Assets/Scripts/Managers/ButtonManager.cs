using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameManager _gameManager;
    public ProceduralEnemySpawner proceduralSpawner;
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
        if (_gameManager.player.currentGold >= 10 &&
            (defenderSpawnManager.selectedDefenderType == DefenderSpawnManager.DefenderType.Aoe))
        {
            _gameManager.player.currentGold -= 10;
            defenderSpawnManager.SpawnPurchasedTower();
            _uiManager.DeActivtePurchaseButton();
        }
        if (_gameManager.player.currentGold >= 15 &&
            (defenderSpawnManager.selectedDefenderType == DefenderSpawnManager.DefenderType.Debuff))
        {
            _gameManager.player.currentGold -= 15;
            defenderSpawnManager.SpawnPurchasedTower();
            _uiManager.DeActivtePurchaseButton();
        }
    }

    public void PurchaseTowerUpgrade()
    {
        switch (_gameManager.selectedTower.name)
        {
            case "BasicTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.Basic);
                }
                break;
                case "MidBasicTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.MidBasic);
                }
                break;
            case "BombTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.Aoe);
                }
                break;
            case "MidBombTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<MeleeDefender>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.MidAoe);
                }

                break;
            case "BuffTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<BuffTower>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<BuffTower>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.Debuff);
                }

                break;
            case "MidBuffTower(Clone)":
                if (_gameManager.player.currentGold >= _gameManager.selectedTower.GetComponent<BuffTower>().cost * 2)
                {
                    _gameManager.player.currentGold -=
                        _gameManager.selectedTower.GetComponent<BuffTower>().cost * 2;
                    defenderSpawnManager.UpgradeTower(_gameManager.selectedTower, DefenderSpawnManager.DefenderType.MidDebuff);
                }

                break;
            default:
                break;
        }
        
    }
    public void StartRound()
    {
        proceduralSpawner.StartProceduralRound();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
