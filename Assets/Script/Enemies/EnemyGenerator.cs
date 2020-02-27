﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gemerador de enemigos
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// Numero de enemigos a generar
    /// </summary>
    public int EnemiesNumber;

    /// <summary>
    /// Prefabs de los enemigos
    /// </summary>
    public List<GameObject> enemiesPrefabs;

    /// <summary>
    /// Lista de enemigos generados
    /// </summary>
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    /// <summary>
    /// Inicializador de los enemigo
    /// </summary>
    public void init(Player target)
    {

        var world = GameManager.GetInstance().worldGenerator.SpriteBoard;
        var worldSize = GameManager.GetInstance().worldGenerator.size;

        int enemiesPlaced = 0;

        while (enemiesPlaced < EnemiesNumber)
        {

            int newX = UnityEngine.Random.Range(0, (int)worldSize.x);
            int newY = UnityEngine.Random.Range(0, (int)worldSize.y);
            Vector2 position = new Vector2(newX, newY);

            GameObject tileobj = world.Where(m => m.GetComponent<Tile>().CellInfo.mapPosition == position).FirstOrDefault();

            if (tileobj != null && tileobj.GetComponent<Tile>().contain == CELLCONTAINER.EMPTY)
            {
                int type = Random.Range(0, 9) >= 3 ? 0 : 1 ;
                tileobj.GetComponent<Tile>().contain = CELLCONTAINER.ENEMY;
                enemies.Add(Instantiate(enemiesPrefabs[type], tileobj.transform.position, Quaternion.identity));
                enemies.Last().AddComponent<Enemy>();
                enemies.Last().GetComponent<Enemy>().init(tileobj.GetComponent<Tile>(), target, type);
                enemiesPlaced++;
            }

        }
    }
}
