using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameManager _gameManager;

    public GameObject[] defenderPrefabs;

    public DefenderScriptable chosenTowerScript;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void SelectTower()
    {
        
    }
    public void PurchaseDefender(GameObject selectedTower)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
