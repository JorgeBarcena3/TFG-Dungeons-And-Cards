using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase de enemigo
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Celda en la que se encuentra el enemigo
    /// </summary>
    Tile currentCell;

    /// <summary>
    /// Funcion de inicializacion de los enemigos
    /// </summary>
    /// <param name="_currentCell"></param>
    public void init(Tile _currentCell)
    {
        currentCell = _currentCell;
    }
}
