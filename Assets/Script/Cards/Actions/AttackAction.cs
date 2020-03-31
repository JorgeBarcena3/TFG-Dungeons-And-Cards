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
    /// Decide el tipo de carta ques es
    /// </summary>
    public new void Start()
    {
        cardType = ATTACKTYPE.ATTACKACTION;
        setRadio();
    }

    /// <summary>
    /// Comprobamos que la accion es posible
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public override bool checkAction()
    {
        if (GameManager.Instance.turn == TURN.IA || GameManager.Instance.player.playerInfo.canUseMana(this.gameObject.GetComponent<Card>().info.Cost))
        {
            Vector2 position = actor.GetComponent<MapActor>().currentCell.CellInfo.mapPosition;

            neighbourTiles = GetWalkableNeighbours(position);

            if (neighbourTiles.Count > 0)
                return true;

        }

        return false;
    }

    /// <summary>
    /// Devuelve la tile recomendada segun el tipo de carta
    /// **SOLO SE DEBE LLAMAR SI SOMOS UN AGENTE CONTROLADO POR LA IA**
    /// </summary>
    public override Tile recommendTile() { return neighbourTiles.Where(m => m.contain == CELLCONTAINER.PLAYER).FirstOrDefault(); }


    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public override void clickOnTile(Tile tile)
    {
        MapActor actorToDestroy = MapActor.instances.Where(m => m.currentCell == tile).First();
        StartCoroutine(actorToDestroy.destroyActor());

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
    public override void DoAction()
    {
        foreach (TileWalkable tile in neighbourTiles)
        {
            tile.assignedAction = this;
            Color selectedColor = new Color(0.94f, 0.27f, 0.27f);
            SpriteRenderer spr = tile.gameObject.GetComponent<SpriteRenderer>();
            spr.color = selectedColor;
        }

        GameManager.Instance.deck.inCardAction = true;
    }

    /// <summary>
    /// Obtiene los vecinos walkables a la celda donde esta el jugador
    /// </summary>
    /// <returns></returns>
    private List<TileWalkable> GetWalkableNeighbours(Vector2 position)
    {

        Tablero board2D = GameManager.Instance.worldGenerator.board;
        List<GameObject> spriteBoard = GameManager.Instance.worldGenerator.SpriteBoard;

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
                && tile.contain == ( (actor.GetComponent<MapActor>().actorType == CELLCONTAINER.ENEMY) ? CELLCONTAINER.PLAYER  : CELLCONTAINER.ENEMY )
                )
            {

                float distance = Vector2.Distance(actor.transform.position, tile.transform.position);
                if (distance < 1 + ((radioVecinos - 1) * 0.5f))
                    tilesWalkables.Add(tile as TileWalkable);
            }
        }

        return tilesWalkables;
    }

}
