using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tipos de celda que puede haber en nuestro mapa
/// </summary>
public enum CELLSTYPE
{
    DEAD = 0,
    ALIVE = 1
}

/// <summary>
/// Celda de juego
/// </summary>
public class Cell
{

    /// <summary>
    /// Informacion de la celda en un tablero
    /// </summary>
    public CellInfo CellInfo { get; set; }

    /// <summary>
    /// Valor del contenido de la celda
    /// </summary>
    public CELLSTYPE Value { get; set; }

    /// <summary>
    /// Cuenta de vecinos vivos
    /// </summary>
    public int CountNeighborsAlive { get; set; }

    /// <summary>
    /// Probabilidad de iniciar como suelo
    /// </summary>
    public float ProbabilityAlive { get; set; }

    /// <summary>
    /// Color de representacion de la célula
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public Cell()
    {
    }

    /// <summary>
    /// Constructor para cuando inicializamos una celda en un mundo
    /// </summary>
    public Cell(int x, int y, float _probability_alive = 0.4f)
    {
        this.ProbabilityAlive = _probability_alive;
        float prob = UnityEngine.Random.Range(0f, 1f);
        this.Value = (prob < ProbabilityAlive) ? CELLSTYPE.DEAD : CELLSTYPE.ALIVE;
        this.CellInfo = new CellInfo(x, y);
        this.Color = this.Value == CELLSTYPE.DEAD ? Color.black : Color.white;

    }

    /// <summary>
    /// Constructor de copia
    /// </summary>
    /// <param name="oldCell">Celda antigua</param>
    public Cell(Cell oldCell)
    {
        this.Value = oldCell.Value;
        this.CellInfo = new CellInfo(oldCell.CellInfo.x, oldCell.CellInfo.y);
        this.ProbabilityAlive = oldCell.ProbabilityAlive;
        this.CountNeighborsAlive = oldCell.CountNeighborsAlive;
        this.Color = oldCell.Color;

    }

    /// <summary>
    /// Constructor para cuando inicializamos una celda en un mundo
    /// </summary>
    public Cell(int x, int y, CELLSTYPE _value, float _probability_alive = 0.4f)
    {
        this.Value = _value;
        this.CellInfo = new CellInfo(x, y);
        this.ProbabilityAlive = _probability_alive;
        this.Color = this.Value == CELLSTYPE.DEAD ? Color.black : Color.white;


    }

    /// <summary>
    /// Devuelve los vecinos que son walkables
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public List<Cell> GetWalkableNeighbours(Tablero board)
    {
        int radioVecinos = board.NeighboursRadius;

        List<Cell> celdasWalkables = new List<Cell>();

        //Cogemos todos los vecinos
        for (int y = radioVecinos; y >= -radioVecinos; --y)
        {
            for (int x = radioVecinos; x >= -radioVecinos; --x)
            {
                int NeighborX = CellInfo.x + x;
                int NeighborY = CellInfo.y + y;

                if (
                    (NeighborX >= 0 && NeighborX < board.world_cell.GetLength(0))
                 && (NeighborY >= 0 && NeighborY < board.world_cell.GetLength(1))
                 && (Math.Abs(x) + Math.Abs(y) != 0)
                 && (NeighborX == CellInfo.x || NeighborY == CellInfo.y)
                 )
                {

                    celdasWalkables.Add(board[NeighborX, NeighborY]);

                }
            }
        }

        return celdasWalkables;
    }

    /// <summary>
    /// Devuelve los vecinos que son walkables
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public List<Cell> getNeighbours(Tablero board)
    {
        int radioVecinos = board.NeighboursRadius;

        List<Cell> celdasWalkables = new List<Cell>();

        //Cogemos todos los vecinos
        for (int y = radioVecinos; y >= -radioVecinos; --y)
        {
            for (int x = radioVecinos; x >= -radioVecinos; --x)
            {
                int NeighborX = CellInfo.x + x;
                int NeighborY = CellInfo.y + y;

                if (
                    (NeighborX >= 0 && NeighborX < board.world_cell.GetLength(0))
                 && (NeighborY >= 0 && NeighborY < board.world_cell.GetLength(1))
                 && (Math.Abs(x) + Math.Abs(y) != 0)
                 )
                {

                    celdasWalkables.Add(board[NeighborX, NeighborY]);

                }
            }
        }

        return celdasWalkables;
    }



}
