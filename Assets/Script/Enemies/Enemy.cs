using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase de enemigo
/// </summary>
public class Enemy : IAAgent
{
  
    /// <summary>
    /// Funcion de inicializacion de los enemigos
    /// </summary>
    /// <param name="_currentCell"></param>
    public void init(Tile _currentCell, Player _target)
    {
        currentCell = _currentCell;
        target = _target;

    }
}
