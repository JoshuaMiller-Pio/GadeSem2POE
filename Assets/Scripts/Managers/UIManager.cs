using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject selectedTowerPanel, shopPanel, gameOverPanel;
    //public DefenderScriptable selectedTowerScript;
    public Image selectedTowerImage;
    public Button roundStart, purchaseTower;
    public TMP_Text selectedTowerName, selectedTowerATK, selectedTowerATKSPD, selectedTowerRustRating, currentGold, enemiesKilled, playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        purchaseTower.interactable = false;
        
    }

    public void ActivateRoundStartButton()
    {
        roundStart.interactable = true;
    }

    public void ActivatePurchaseButton()
    {
        purchaseTower.interactable = true;
    }

    public void DeActivtePurchaseButton()
    {
        purchaseTower.interactable = false;
    }
    //Updates the on screen UI with the stats of the selected tower
    public void ShowSelectedTower(GameObject selectedTower)
    {
        
        if (selectedTower.name == "BasicTower(Clone)" || selectedTower.name == "MidBasicTower(Clone)"|| selectedTower.name == "BigBasicTower(Clone)"|| selectedTower.name == "BombTower(Clone)" || selectedTower.name == "MidBombTower(Clone)" || selectedTower.name == "BigBombTower(Clone)")
        {
            var selectedTowerScript = selectedTower.GetComponent<MeleeDefender>();
            selectedTowerName.text = selectedTowerScript.name;
            selectedTowerATK.text = "ATK = " + selectedTowerScript.damage;
            selectedTowerATKSPD.text = "ATK SPD = " + selectedTowerScript.atkSpd;
            selectedTowerRustRating.text = "Rust Level = " + selectedTowerScript.Rrating;
            selectedTowerPanel.SetActive(true);
        }
        if( selectedTower.name == "BuffTower(Clone)" || selectedTower.name == "MidBuffTower(Clone)" || selectedTower.name == "BigBuffTower(Clone)")
        {
            var selectedTowerScript = selectedTower.GetComponent<BuffTower>();
            selectedTowerName.text = selectedTowerScript.name;
            selectedTowerATK.text = "ATK = " + selectedTowerScript.damage;
            selectedTowerATKSPD.text = "ATK SPD = " + selectedTowerScript.atkSpd;
            selectedTowerRustRating.text = "Rust Level = " + selectedTowerScript.Rrating;
            selectedTowerPanel.SetActive(true);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        currentGold.text = "Gold: " +GameManager.Instance.player.currentGold;
        enemiesKilled.text = "Enemies killed: " + GameManager.Instance.deadEnemies;
       playerHealth.text = "Health: " + GameManager.Instance.player.currentHealth;
    }
}
