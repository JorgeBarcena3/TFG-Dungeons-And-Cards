using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Posblies tiles walkables
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
/// Componente del mapa que se puede pisar
/// </summary>
public class TileWalkable : Tile
{
    /// <summary>
    /// Tipo de celda
    /// </summary>
    public TileWalkableType tileType;

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public TileWalkable(int x, int y) : base( walkable: CELLCONTAINER.EMPTY) { }



    /// <summary>
    /// Start
    /// </summary>
    public void Awake()
    {
        contain = CELLCONTAINER.EMPTY;
        tileRender.sprite = GameArtTheme.Instance.currentTheme.floor;
        InitVisualMap(GameArtTheme.Instance.currentTheme.floor.texture);
    }

}

