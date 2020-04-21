using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controlador del HUD
/// </summary>
public class HUDController : MonoBehaviour
{
    /// <summary>
    /// Cartel de turnos
    /// </summary>
    public Turnlbl turnlbl;

    /// <summary>
    /// Manager del mana del jugador
    /// </summary>
    public ManaManager manaManager;

    /// <summary>
    /// Manager del HUD del enemigo
    /// </summary>
    public EnemyHudManager enemyHUDManager;
}