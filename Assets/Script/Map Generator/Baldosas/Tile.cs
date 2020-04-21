using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determina que contiene la celda
/// </summary>
public enum CELLCONTAINER
{
    EMPTY = 0,
    ENEMY = 1,
    WALL = 2,
    PLAYER = 3
}

/// <summary>
/// 
/// </summary>
public class Tile : MonoBehaviour
{
    //Posicion x
    public float X { get; private set; }

    //Posicion y
    public float Y { get; private set; }

    //Profundidaz 
    public float Z { get; private set; }

    //Indica si esta baldosa es transitable
    // public bool Walkable { get; set; }
    [HideInInspector]
    public CELLCONTAINER contain;

    //Referencia al spriterender de la baldosa
    [HideInInspector]
    public SpriteRenderer tileRender;

    //Referencia al transform de la baldosa
    [HideInInspector]
    public Transform tileTransform;

    /// <summary>
    /// Determina si tiene una accion asignada o no
    /// </summary>
    [HideInInspector]
    public CardAction assignedAction = null;

    /// <summary>
    /// Devuelve las tiles walkables
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public List<Tile> GetWalkableNeighboursForEnemies()
    {
        int radioVecinos = 1;

        Vector2 position = CellInfo.mapPosition; 

        Tablero board2D = GameManager.Instance.worldGenerator.board;
        List<GameObject> spriteBoard = GameManager.Instance.worldGenerator.SpriteBoard;

        List<Vector2> cell2D = new List<Vector2>();
        List<Tile> tilesWalkables = new List<Tile>();

        //Cogemos todos los vecinos
        for (int y = radioVecinos; y >= -radioVecinos; --y)
        {
            for (int x = radioVecinos; x >= -radioVecinos; --x)
            {
                int NeighborX = (int)position.x + x;
                int NeighborY = (int)position.y + y;

                if (
                    (NeighborX >= 0 && NeighborX < board2D.worldCells.GetLength(0))
                 && (NeighborY >= 0 && NeighborY < board2D.worldCells.GetLength(1))
                 && (Math.Abs(x) + Math.Abs(y) != 0)
                 )
                {

                    cell2D.Add(board2D[NeighborX, NeighborY].CellInfo.mapPosition);

                }
            }
        }

        foreach (GameObject item in spriteBoard)
        {
            Tile tile = item.GetComponent<Tile>();


            if (
                cell2D.Contains(tile.CellInfo.mapPosition)
             && (tile.contain == CELLCONTAINER.EMPTY || 
             tile.contain == CELLCONTAINER.PLAYER ||
             tile.contain == CELLCONTAINER.ENEMY)
                )
            {

                float distance = Vector2.Distance(this.transform.position, tile.transform.position);
                if (distance < 1)
                    tilesWalkables.Add(tile as TileWalkable);
            }
        }

        return tilesWalkables;

    }

    /// <summary>
    /// Informacion de la celda
    /// </summary>
    public CellInfo CellInfo;

    public Tile(CELLCONTAINER walkable = CELLCONTAINER.EMPTY)
    {
        this.contain = walkable;        

    }
    public void Start()
    {
        tileRender = GetComponent<SpriteRenderer>();
        tileTransform = GetComponent<Transform>();
        X = tileTransform.position.x;
        Y = tileTransform.position.y;
        Z = tileTransform.position.z;
        if (contain == CELLCONTAINER.WALL)
            Elevate();
<<<<<<< HEAD
=======



        tileRender.material.SetTexture("_texture", tileRender.sprite.texture);
        tileRender.material.SetFloat("_noise_efect", UnityEngine.Random.Range(30, 100));

>>>>>>> 4d576e3a1bd6b51baff097f73c12077fa2073481
    }
    public Vector2 GetPosition()
    {
        return new Vector2(X, Y);
    }
    public void SetPosition(Vector2 position)
    {
        X = position.x;
        Y = position.y;
        tileTransform.position = position;
    }
    public void Elevate()
    {
        Vector3 size = GetComponent<SpriteRenderer>().bounds.size;
        tileTransform.position = new Vector3(X, Y + size.y * 9 / 40, Z-1);

    }
    public void InitVisualMap(Texture2D texture) 
    {
        tileRender.material.SetTexture("_texture", texture);
        tileRender.material.SetFloat("_noise_efect", UnityEngine.Random.Range(30, 100));
        StartCoroutine(AnimationGeneration(10));

    }
    public IEnumerator AnimationGeneration(float time_animation) 
    {
        float value=0.0f;
        tileRender.material.SetFloat("_time_animation", time_animation);
        while (value < time_animation)
        {
            value += Time.deltaTime;
            tileRender.material.SetFloat("_interpolate", value);
            yield return null;
        }
        tileRender.material.SetFloat("_interpolate", time_animation);
        yield return null;
    }

    /// <summary>
    /// Cuando hacemos click en un objeto
    /// </summary>
    private void OnMouseDown()
    {
        if (assignedAction)
        {
            assignedAction.clickOnTile(this);
        }
    }

}
