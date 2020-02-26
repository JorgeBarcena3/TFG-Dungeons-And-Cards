using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Controla los nodos hexagonales
/// </summary>
public class NodoHexagonal
{
    /// <summary>
    /// Estado de la celda del nodo
    /// </summary>
    public Tile estado;

    /// <summary>
    /// Nodo padre
    /// </summary>
    public NodoHexagonal nodoPadre;

    /// <summary>
    /// Constructor con parametros
    /// </summary>
    /// <param name="_estado"></param>
    /// <param name="_padre"></param>
    public NodoHexagonal(Tile _estado, NodoHexagonal _padre)
    {

        estado = _estado;
        nodoPadre = _padre;

    }

    /// <summary>
    /// Expandimos la lista, con todos los vecinos walkables por el usuario
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public virtual List<NodoHexagonal> Expandir()
    {

        List<NodoHexagonal> result = new List<NodoHexagonal>();
        List<Tile> vecinos = estado.GetWalkableNeighboursForEnemies();

        for (int i = 0; i < vecinos.Count; i++)
        {
            Tile vecino = vecinos[i];

            NodoHexagonal nuevo = new NodoHexagonal(vecino, this);
            result.Add(nuevo);


        }

        return result;
    }

    /// <summary>
    /// Devuelve el nodo padre
    /// </summary>
    /// <returns></returns>
    public NodoHexagonal getPadre()
    {
        return nodoPadre;
    }

    /// <summary>
    /// Determina si el nodo ha llegado a la meta
    /// </summary>
    /// <param name="nodo">Nodo a comprobar</param>
    /// <param name="to">Celda meta</param>
    /// <returns></returns>
    public bool esMeta(NodoHexagonal nodo, Tile to)
    {
        return nodo.estado.CellInfo.x == to.CellInfo.x && nodo.estado.CellInfo.y == to.CellInfo.y;
    }
}

