using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public enum TileUnwalkableType
{
    WALL,
    IMPENETRABLEWALL
}

/// <summary>
/// 
/// </summary>
public class TileUnwalkable : Tile
{
   
    public TileUnwalkableType tileType;
    public TileUnwalkable(int x, int y) : base( walkable: false) {  }

    public void Awake()
    {
        Walkable = false;
        tileRender.sprite = sprites[(int)tileType];
    }
   
}
