using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "CreateTile")]
public class TileScriptable : ScriptableObject
{
    
    public enum TileType
    {
        Path,
        Spawn,
        Inland
    }

    public string[] names = new string[3] { "Path", "Spawn", "Inland" }; 
   
    public Color Color;
    public string name;
    public bool isPath = false;
    public bool isSummonable  = false;
    public TileType tileType;
}