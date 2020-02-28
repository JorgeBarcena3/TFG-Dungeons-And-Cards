using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase utilizada para encontrar el pathFinding
/// </summary>
public static class PathFindingHexagonal
{

    //Lista de nodos abiertos
    private static  List<NodoHexagonal> abierta = new List<NodoHexagonal>();

    //Limite de nodos
    private static  int nodosActuales = 0;

    /// <summary>
    /// Devuelve la ruta desde una posicion a otra
    /// </summary>
    /// <param name="board"></param>
    /// <param name="currentPos"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public static  List<Tile> calcularRuta( Tile currentPos, Tile goal, int limiteNodos = 10000)
    {
        nodosActuales = 0;
        abierta = new List<NodoHexagonal>();

        //Añadimos el nodo principal
        abierta.Add(new NodoHexagonal(currentPos, null));

        //Nodo actual
        NodoHexagonal nodo = null;

        while (abierta.Count > 0 && nodosActuales < limiteNodos)
        {
            nodo = abierta[0];
            abierta.RemoveAt(0);

            if (nodo.esMeta(nodo, goal))
            {
                return nodoACelda(nodo);

            }
            else
            {
                List<NodoHexagonal> sucesores = nodo.Expandir();

                foreach (NodoHexagonal s in sucesores)
                {
                    //Si no lo contiene lo añadimos
                    if (!estaEnLaLista(s, abierta))
                    {
                        abierta.Add(s);
                        nodosActuales++;
                    }
                }
            }

        }

        return new List<Tile>();
    }

    /// <summary>
    /// Recoje el camino necesario para llegar a esa celda
    /// </summary>
    /// <param name="abierta"></param>
    /// <returns></returns>
    private static List<Tile> nodoACelda(NodoHexagonal abierta)
    {
        List<Tile> wayPoints = new List<Tile>();

        NodoHexagonal nodo = abierta;
        while (nodo.getPadre() != null)
        {
            wayPoints.Add(nodo.estado);
            nodo = nodo.getPadre();
        }

        wayPoints.Reverse();

        return wayPoints;
    }

    /// <summary>
    /// Se determina si el nodo correspondiente esta en la lista de abiertos
    /// </summary>
    /// <param name="nodo"></param>
    /// <param name="abierta"></param>
    /// <returns></returns>
    public static bool estaEnLaLista(NodoHexagonal nodo, List<NodoHexagonal> abierta)
    {
        bool esta = false;

        foreach (NodoHexagonal s in abierta)
        {
            if (s.estado.CellInfo.x == nodo.estado.CellInfo.x
                && s.estado.CellInfo.y == nodo.estado.CellInfo.y
                )
                esta = true;
        }
        return esta;
    }

}

