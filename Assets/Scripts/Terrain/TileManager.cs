using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Grid _grid;
    // Start is called before the first frame update
    void Start()
    {
        _grid = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
