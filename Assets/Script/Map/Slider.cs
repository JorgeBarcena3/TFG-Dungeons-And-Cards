using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Funcion que permite mover una superficie. Requiere el componente de boxColider2D
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Slider : MonoBehaviour
{

    /// <summary>
    /// Componente BoxCollider2D
    /// </summary>
    private BoxCollider2D boxCollider;

    /// <summary>
    /// Funcion que inicializa el componente
    /// </summary>
    private void init()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Activa la funcion de scroll
    /// </summary>
    public void activate(SpriteRenderer sprite, Vector2 mapSize)
    {
        init();
        adaptBoxCollider(sprite, mapSize);
    }

    /// <summary>
    /// Adaptar la box collider a todos sus hijos
    /// </summary>
    private void adaptBoxCollider(SpriteRenderer spr, Vector2 mapSize)
    {
        float sizeX = spr.size.x * mapSize.x;
        float sizeY = mapSize.y * spr.size.y * 23 / 40;

        boxCollider.size = new Vector2(sizeX, sizeY);

       boxCollider.offset = new Vector2(spr.size.x * (mapSize.x / 2), - mapSize.y * spr.size.y * 23 / 40/2);

    }
}
