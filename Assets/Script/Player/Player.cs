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
        currentCell = world.FirstOrDefault().GetComponent<Tile>();

        foreach (var cellObj in world)
        {
            Tile cell = cellObj.GetComponent<Tile>();
            if (cell.Walkable)
            {
                currentCell = cell;
                break;
            }

        }

        this.transform.position = new Vector3(currentCell.gameObject.transform.position.x, currentCell.gameObject.transform.position.y, 100);
        this.gameObject.SetActive(true);
    }
}
