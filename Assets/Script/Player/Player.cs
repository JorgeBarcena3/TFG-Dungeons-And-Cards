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
    private int zDepth;

    /// <summary>
    /// Tile en la que se encuentra el jugador
    /// </summary>
    public Tile currentCell;

    /// <summary>
    /// Funcion de inicializacion del jugador
    /// </summary>
    public void init()
    {
        var world = GameManager.GetInstance().worldGenerator.SpriteBoard;
        var worldSize = GameManager.GetInstance().worldGenerator.size;
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


        this.transform.position = new Vector3(currentCell.gameObject.transform.position.x, currentCell.gameObject.transform.position.y, 100);
        this.gameObject.SetActive(true);
    }
}
