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
    public TMP_Text selectedTowerName, selectedTowerATK, selectedTowerATKSPD, selectedTowerDescription;
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
    public void ShowSelectedTower(GameObject selectedTower)
    {
        
        if (selectedTower.CompareTag("Defender"))
        {
            var selectedTowerScript = selectedTower.GetComponent<MeleeDefender>();
            selectedTowerName.text = selectedTowerScript.name;
            selectedTowerATK.text = "ATK = " + selectedTowerScript.damage;
            selectedTowerATKSPD.text = "ATK SPD = " + selectedTowerScript.atkSpd;
            selectedTowerPanel.SetActive(true);
        }
       
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
