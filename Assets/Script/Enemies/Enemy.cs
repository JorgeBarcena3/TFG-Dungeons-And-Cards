using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tipo de enemigo
/// </summary>
enum ENEMY_TYPE : int
{
    HYPER_FAST = 2,
    FAST = 1,
    SLOW = 0
}

/// <summary>
/// Clase de enemigo
/// </summary>
public class Enemy : IAAgent
{

    /// <summary>
    /// Tipo de enemigo
    /// </summary>
    ENEMY_TYPE type;

    /// <summary>
    /// Funcion de inicializacion de los enemigos
    /// </summary>
    /// <param name="_currentCell"></param>
    public void init(Tile _currentCell, Player _target, int _type)
    {
        currentCell = _currentCell;
        target = _target;
        setType(_type);
    }

    /// <summary>
    /// Determina el tipo de enemigo
    /// </summary>
    private void setType(int index)
    {
        type = (ENEMY_TYPE)Enum.Parse(typeof(ENEMY_TYPE), index.ToString());
    }

    /// <summary>
    /// Devuelve el avance del enemigo
    /// </summary>
    /// <returns></returns>
    public int getAvance()
    {
        return (int)type;
    }


}
