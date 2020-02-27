using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Informacion del jugador
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    [Header("Mana Info")]

    /// <summary>
    /// Cantidad de movimientos por turno
    /// </summary>
    public int maxManaPoints = 5;

    /// <summary>
    /// Cantidad de movimientos por turno
    /// </summary>
    public int currentManaPoints = 5;

    /// <summary>
    /// Actualiza la informacion del jugador
    /// </summary>
    public void refreshData()
    {
        GameManager.GetInstance().hud.manaManager.refreshMana(currentManaPoints > maxManaPoints ? currentManaPoints = maxManaPoints : currentManaPoints, (float)currentManaPoints / (float)maxManaPoints);
    }

    /// <summary>
    /// Usa el maná del jugador
    /// </summary>
    /// <param name="mana"></param>
    /// <returns>Devuelve true si queda mas maná</returns>
    public bool useMana(int mana)
    {
        currentManaPoints -= mana;

        if(currentManaPoints <= 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Añade n cantidad de mana
    /// </summary>
    /// <param name="mana"></param>
    /// <returns></returns>
    public void addMana(int mana)
    {
        currentManaPoints = currentManaPoints + mana > maxManaPoints ? maxManaPoints : currentManaPoints + mana;
    }

    /// <summary>
    /// Determina si se puede usar el mana o no
    /// </summary>
    /// <param name="mana"></param>
    /// <returns></returns>
    public bool canUseMana(int mana)
    {
        return (currentManaPoints - mana) >= 0;
    }
}
