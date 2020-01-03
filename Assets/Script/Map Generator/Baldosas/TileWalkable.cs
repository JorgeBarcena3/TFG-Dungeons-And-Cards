using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public enum TileWalkableType
{
    TILEFLOOR,
    SPAWNTILE,
    EXITTILE,
    SHRINE,
    HALLWAY
}

/// <summary>
/// 
/// </summary>
public class TileWalkable : Tile
{
    public TileWalkableType tileType;
    public TileWalkable(int x, int y) : base( walkable: true) { }
    public void Awake()
    {
        Walkable = true;
        tileRender.sprite = sprites[(int)tileType];
    }
}
