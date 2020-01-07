using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool Walkable { get; set; }

    //Referencia al spriterender de la baldosa
    [HideInInspector]
    public SpriteRenderer tileRender;

    //Referencia al transform de la baldosa
    [HideInInspector]
    public Transform tileTransform;

    //array de posibles sprites
    public Sprite[] sprites;

    /// <summary>
    /// Determina si tiene una accion asignada o no
    /// </summary>
    public CardAction assignedAction = null;

    /// <summary>
    /// Informacion de la celda
    /// </summary>
    public CellInfo CellInfo;

    public Tile(bool walkable = true)
    {
        this.Walkable = walkable;        

    }
    public void Start()
    {
        tileRender = GetComponent<SpriteRenderer>();
        tileTransform = GetComponent<Transform>();
        X = tileTransform.position.x;
        Y = tileTransform.position.y;
        Z = tileTransform.position.z;
        if (!Walkable)
            Elevate();

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
