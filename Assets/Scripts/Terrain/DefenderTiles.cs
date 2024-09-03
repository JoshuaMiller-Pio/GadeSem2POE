using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderTiles : MonoBehaviour
{
    public ButtonManager _buttonManager;
    public MeshRenderer meshRenderer;
    public DefenderSpawnManager _defenderSpawn;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        _defenderSpawn = GameObject.FindGameObjectWithTag("DefenderSpawnManager").GetComponent<DefenderSpawnManager>();
    }

    private void OnMouseDown()
    {
        meshRenderer.material.color = Color.green;
        _defenderSpawn.SelectTile(this.gameObject);
        
    }

    private void OnMouseOver()
    {
        meshRenderer.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
