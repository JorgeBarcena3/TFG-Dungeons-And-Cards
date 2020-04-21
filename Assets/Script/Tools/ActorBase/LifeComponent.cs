using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja la vida de un actor
/// </summary>
public class LifeComponent
{
    /// <summary>
    /// Maxima vida del jugador
    /// </summary>
    public int maxLife { get; private set; }

    /// <summary>
    /// Vida actual del actor
    /// </summary>
    public int currentLife { get; private set; }

    /// <summary>
    /// Devuelve el porcentage de vida que nos queda
    /// </summary>
    public float lifePercentage { get { return (float)currentLife / (float)maxLife; } }

    /// <summary>
    /// Modifica la vida de un jugador
    /// </summary>
    /// <param name="v"></param>
    /// <returns> Devuelve false si el actor ha muerto </returns>
    public bool changeLife(int v)
    {

        currentLife = Mathf.Max(currentLife + v, v > 0 ? maxLife : 0);
        return currentLife > 0;
    }

    /// <summary>
    /// Determina si es el ultimo golpe o no
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public bool isUltimateHit(int v)
    {

        return currentLife + v <= 0;
    }

    /// <summary>
    /// Reseteamos al maximo la vida del jugador
    /// </summary>
    /// <param name="v"></param>
    public void resetMaxLife(int v = 5)
    {
        maxLife = currentLife = v;
    }


}
