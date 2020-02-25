using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Agentes controlables por la IA
/// </summary>
public class IAAgent : MonoBehaviour
{
    /// <summary>
    /// Celda en la que se encuentra el enemigo
    /// </summary>
    public Tile currentCell;

    /// <summary>
    /// Objetivo del agente
    /// </summary>
    public Player target;

}
