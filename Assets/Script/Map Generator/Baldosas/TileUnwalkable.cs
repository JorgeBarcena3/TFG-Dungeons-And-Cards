using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Posblies tiles unwalkables
/// </summary>
public enum TileUnwalkableType
{
    WALL,
    IMPENETRABLEWALL
}

/// <summary>
/// Componente del mapa que no se  puede atravesar
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
