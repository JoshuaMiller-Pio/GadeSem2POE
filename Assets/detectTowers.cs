using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Unity.VisualScripting;
using UnityEngine;

public class detectTowers : MonoBehaviour
{
    public List<GameObject> towers = new List<GameObject>();

    public DefenderScriptable.DefenderType type ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DefenderScriptable.DefenderType TowerType;
        if (other.tag == "Defender")
        {
            towers.Add(other.gameObject);
        }
        TowerType other.gameObject.GetComponent(MeleeDefender).
        switch (type)
        {
            case other.GameObject()
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Defender")
        {
            towers.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
