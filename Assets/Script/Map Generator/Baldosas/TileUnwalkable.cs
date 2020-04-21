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
    public TileUnwalkable(int x, int y) : base( walkable: CELLCONTAINER.WALL) {  }

    public void Awake()
    {
        contain = CELLCONTAINER.WALL;
        tileRender.sprite = GameArtTheme.Instance.currentTheme.wall;
        InitVisualMap(GameArtTheme.Instance.currentTheme.wall.texture);
    }

}
