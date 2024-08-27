using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject selectedTowerPanel, shopPanel;
    public DefenderScriptable selectedTowerScript;
    public Image selectedTowerImage;
    public TMP_Text selectedTowerName, selectedTowerATK, selectedTowerATKSPD, selectedTowerDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowSelectedTower(GameObject selectedTower)
    {
        selectedTowerScript = selectedTower.GetComponent<DefenderScriptable>();
        selectedTowerName.text = selectedTowerScript.defenderName;
        selectedTowerATK.text = "ATK = " + selectedTowerScript.damage;
        selectedTowerATKSPD.text = "ATK SPD = " + selectedTowerScript.atkSpeed;
        selectedTowerPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
