using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderTiles : MonoBehaviour
{
    public ButtonManager _buttonManager;

    public DefenderSpawnManager _defenderSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        _defenderSpawn = GameObject.FindGameObjectWithTag("DefenderSpawnManager").GetComponent<DefenderSpawnManager>();
    }

    private void OnMouseDown()
    {
        _defenderSpawn.SelectTile(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
