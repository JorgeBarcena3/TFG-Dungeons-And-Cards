using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    /// <summary>
    /// Profundidad de la Z
    /// </summary>
    [HideInInspector]
    public Vector3 zOffset = new Vector3(0, 0, -0.2f);

    /// <summary>
    /// Tile en la que se encuentra el jugador
    /// </summary>
    [HideInInspector]
    public Tile currentCell;

    /// <summary>
    /// Informacion del player
    /// </summary>
    public PlayerInfo playerInfo;

    /// <summary>
    /// Funcion de inicializacion del jugador
    /// </summary>
    public void init()
    {
        var world = GameManager.Instance.worldGenerator.SpriteBoard;
        var worldSize = GameManager.Instance.worldGenerator.size;
        bool spawned = false;

        do
        {
            int newX = UnityEngine.Random.Range(0, (int)worldSize.x);
            int newY = UnityEngine.Random.Range(0, (int)worldSize.y);

            currentCell = world[newX + (newY * (int)worldSize.x)].GetComponent<Tile>();
            Tile cell = currentCell.GetComponent<Tile>();

            if (currentCell.contain == CELLCONTAINER.EMPTY)
            {
                currentCell = cell;
                currentCell.contain = CELLCONTAINER.PLAYER;
                spawned = true;
            }

        } while (!spawned);


        this.transform.position = currentCell.gameObject.transform.position + zOffset;
        this.gameObject.SetActive(true);

        playerInfo.currentManaPoints = playerInfo.maxManaPoints;
        refreshPlayerData();
    }

    /// <summary>
    /// Refresca el los datos del jugador
    /// </summary>
    public void refreshPlayerData()
    {
        playerInfo.refreshData();
    }

    /// <summary>
    /// Teletransporta al jugador a una posicion
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public IEnumerator TeleportPlayerTo(Vector3 goal, float time = 1f)
    {
        float t = 0;

        StartCoroutine(FadeOutPlayer(time / 4));
        goal += zOffset;

        while (Vector3.Distance(this.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            this.transform.position = Vector3.Lerp(this.transform.position, goal, t);
            yield return null;
        }

        this.transform.position = goal;
        StartCoroutine(FadeInPlayer(time / 4));
    }

    /// <summary>
    /// Efecto de fadeout del jugador
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOutPlayer(float time = 1f)
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
    public IEnumerator FadeInPlayer(float time = 1f)
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
