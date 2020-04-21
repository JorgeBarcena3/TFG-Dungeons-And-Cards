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

    /// <summary>
    /// Imagen para rellenar la barra de vida
    /// </summary>
    [SerializeField]
    private Image life = null;

    /// <summary>
    /// Cambia la barra de porcentaje el porcentaje del mana
    /// </summary>
    /// <param name="percentage"> Valores entre 0 y 1 </param>
    public IEnumerator setLifePercentajeBar(float percentage, float time)
    {
        float t = 0;

        while (life.fillAmount != percentage)
        {
            t += Time.deltaTime / time;
            life.fillAmount = Mathf.Lerp(life.fillAmount, percentage, t);
            yield return null;
        }

        life.fillAmount = percentage;
    }
}