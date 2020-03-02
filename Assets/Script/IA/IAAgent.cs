using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// Si ha realizado la accion o no
    /// </summary>
    public bool actionDone = false;

    /// <summary>
    /// Efecto de FadeOut
    /// </summary>
    /// <param name="image">Imagen a la que se le aplicara el efecto</param>
    /// <returns></returns>
    public IEnumerator DestroyEnemy(float time = 1)
    {
        float t = 0;

        List<SpriteRenderer> images = new List<SpriteRenderer>(this.GetComponentsInChildren<SpriteRenderer>());

        while (images.Where(m => m.color.a != 0).Count() > 0)
        {
            t += Time.deltaTime / time;

            foreach (SpriteRenderer bodyPart in images)
            {
                bodyPart.color = Color.Lerp(bodyPart.color, new Color(bodyPart.color.r, bodyPart.color.g, bodyPart.color.b, 0), t);
            }

            yield return null;

        }

        GameManager.Instance.agents.Remove(this);
        GameManager.Instance.enemyGenerator.enemies.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

}
