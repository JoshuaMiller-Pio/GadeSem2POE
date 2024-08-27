using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnManager : MonoBehaviour
{
    public GameObject[] defenderPrefabs;
    public enum DefenderType
    {
        Basic,
        Debuff,
        Aoe
    }

    public DefenderType selectedDefenderType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
