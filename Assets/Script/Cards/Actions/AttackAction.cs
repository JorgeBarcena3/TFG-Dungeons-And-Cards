using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Accion de ataque de la carta del jugador
/// </summary>
public class AttackAction : CardAction
{
    /// <summary>
    /// Tiles a las que se pueden acceder
    /// </summary>
    private List<TileWalkable> neighbourTiles;

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public override void clickOnTile(Tile tile)
    {
        GameObject enemy = GameManager.GetInstance().enemyGenerator.enemies.Where(m => m.GetComponent<Enemy>().currentCell == tile).FirstOrDefault();
        GameManager.GetInstance().enemyGenerator.enemies.Remove(enemy);
        Destroy(enemy);

        StartCoroutine(AuxiliarFuncions.MoveObjectTo(GameManager.GetInstance().player.gameObject.transform, tile.transform.position));

        GameManager.GetInstance().player.currentCell = tile;
        tile.contain = CELLCONTAINER.EMPTY;

        foreach (TileWalkable tl in neighbourTiles)
        {
            tl.assignedAction = null;
            Color selectedColor = new Color(1, 1, 1);
            SpriteRenderer spr = tl.gameObject.GetComponent<SpriteRenderer>();
            spr.color = selectedColor;
        }

        finishTurn();

    }

    /// <summary>
    /// Realiza la accion de moverse a una casilla
    /// </summary>
    public override void DoAction(GameObject player)
    {
        Vector2 position = player.GetComponent<Player>().currentCell.CellInfo.mapPosition;

        neighbourTiles = GetWalkableNeighbours(position, player);

        foreach (TileWalkable tile in neighbourTiles)
        {
            tile.assignedAction = this;
            Color selectedColor = new Color(0.94f, 0.27f, 0.27f);
            SpriteRenderer spr = tile.gameObject.GetComponent<SpriteRenderer>();
            spr.color = selectedColor;
        }
    }

    /// <summary>
    /// Obtiene los vecinos walkables a la celda donde esta el jugador
    /// </summary>
    /// <returns></returns>
    private List<TileWalkable> GetWalkableNeighbours(Vector2 position, GameObject player)
    {

        int radioVecinos = 1;

        Tablero board2D = GameManager.GetInstance().worldGenerator.board;
        List<GameObject> spriteBoard = GameManager.GetInstance().worldGenerator.SpriteBoard;

        List<Vector2> cell2D = new List<Vector2>();
        List<TileWalkable> tilesWalkables = new List<TileWalkable>();

        //Cogemos todos los vecinos
        for (int y = radioVecinos; y >= -radioVecinos; --y)
        {
            for (int x = radioVecinos; x >= -radioVecinos; --x)
            {
                int NeighborX = (int)position.x + x;
                int NeighborY = (int)position.y + y;

                if (
                    (NeighborX >= 0 && NeighborX < board2D.worldCells.GetLength(0))
                 && (NeighborY >= 0 && NeighborY < board2D.worldCells.GetLength(1))
                 && (Math.Abs(x) + Math.Abs(y) != 0)
                 )
                {

                    cell2D.Add(board2D[NeighborX, NeighborY].CellInfo.mapPosition);

                }
            }
        }

        foreach (GameObject item in spriteBoard)
        {
            Tile tile = item.GetComponent<Tile>();


            if (
                cell2D.Contains(tile.CellInfo.mapPosition)
             && tile.contain == CELLCONTAINER.ENEMY
                )
            {

                float distance = Vector2.Distance(player.transform.position, tile.transform.position);
                if (distance < 1)
                    tilesWalkables.Add(tile as TileWalkable);
            }
        }

        return tilesWalkables;
    }
}
