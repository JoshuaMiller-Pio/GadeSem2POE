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
    public Button RoundStart;
    public TMP_Text selectedTowerName, selectedTowerATK, selectedTowerATKSPD, selectedTowerDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateRoundStartButton()
    {
        RoundStart.interactable = true;
    }
    public void ShowSelectedTower(GameObject selectedTower)
    {
        
        if (selectedTower.CompareTag("BasicTower"))
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
