using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Todos los objetos del juego heredan de esta clase
/// </summary>
public class MapActor : MonoBehaviour
{

    /// <summary>
    /// Guarda una lista de todos los elementos del mapa instanciados
    /// </summary>
    public static List<MapActor> instances = new List<MapActor>();

    /// <summary>
    /// Tile en la que se encuentra el jugador
    /// </summary>
    [HideInInspector]
    public Tile currentCell;

    /// <summary>
    /// Tipo de actor
    /// </summary>
    public CELLCONTAINER actorType;

    /// <summary>
    /// Destruye el actor
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public virtual IEnumerator destroyActor(float time = 1) { yield return null;  }

    /// <summary>
    /// Instancia del start
    /// </summary>
    public void Start()
    {
        MapActor.instances.Add(this);
    }

    /// <summary>
    /// Determina el tipo de actor que es
    /// </summary>
    /// <param name="_type"></param>
    public void setActorType(CELLCONTAINER _type)
    {
        actorType = _type;
    }

    /// <summary>
    /// Teletransporta al actor a una posicion
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public virtual IEnumerator TeleportActorTo(Vector3 goal, float time = 1f)
    {
        float t = 0;

        StartCoroutine(FadeOutActor(time / 4));

        while (Vector3.Distance(this.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            this.transform.position = Vector3.Lerp(this.transform.position, goal, t);
            yield return null;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) + (GameManager.Instance.player != null ? GameManager.Instance.player.zOffset : Vector3.zero);

        //this.transform.position = goal;
        StartCoroutine(FadeInActor(time / 4));
    }

    /// <summary>
    /// Efecto de fadeout del jugador
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOutActor(float time = 1f)
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

    }

    /// <summary>
    /// Efecto de fadein del jugador
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeInActor(float time = 1f)
    {
        float t = 0;

        List<SpriteRenderer> images = new List<SpriteRenderer>(this.GetComponentsInChildren<SpriteRenderer>());

        while (images.Where(m => m.color.a != 1).Count() > 0)
        {
            t += Time.deltaTime / time;

            foreach (SpriteRenderer bodyPart in images)
            {
                bodyPart.color = Color.Lerp(bodyPart.color, new Color(bodyPart.color.r, bodyPart.color.g, bodyPart.color.b, 1), t);
            }

            yield return null;

        }
    }

}
