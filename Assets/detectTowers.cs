using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Defenders;
using Unity.VisualScripting;
using UnityEngine;

public class detectTowers : MonoBehaviour
{
    public List<GameObject> towers = new List<GameObject>();
    public int AOE=0, BASIC=0, DEBUFF=0;

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
            TowerType = other.gameObject.GetComponent<MeleeDefender>().defenderScript.defenderType;
            switch (TowerType)
            {
                case DefenderScriptable.DefenderType.Aoe:
                    AOE++;
                    break;
                case DefenderScriptable.DefenderType.Basic:
                    BASIC++;
                    break;
                case DefenderScriptable.DefenderType.Debuff:
                    DEBUFF++;
                    break;

            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Defender")
        {        
            DefenderScriptable.DefenderType TowerType;
            towers.Remove(other.gameObject);
            TowerType = other.gameObject.GetComponent<MeleeDefender>().defenderScript.defenderType;
            switch (TowerType)
            {
                case DefenderScriptable.DefenderType.Aoe:
                    AOE--;
                    break;
                case DefenderScriptable.DefenderType.Basic:
                    BASIC--;
                    break;
                case DefenderScriptable.DefenderType.Debuff:
                    DEBUFF--;
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
