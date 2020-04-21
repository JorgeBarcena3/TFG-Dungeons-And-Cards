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
    /// Posicion del click
    /// </summary>
    private Vector3 mousePosition;

    /// <summary>
    /// Posicion del primer touch
    /// </summary>
    private Vector3 touchStart;

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

        boxCollider.offset = new Vector2(spr.size.x * (mapSize.x / 2.5f) , -mapSize.y * spr.size.y * 23 / 40 / 2.3f);

    }


    void Update()
    {
        if (GameManager.Instance.state == States.INGAME && GameManager.Instance.turn == TURN.PLAYER &&
            GameManager.Instance.deck.deckInfo.infoCard == null && !GameManager.Instance.hud.enemyHUDManager.active)
        {
            /// Determina cuando se esta arrastrando el raton
            if (Input.GetMouseButtonDown(0) && boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            } else

            if (Input.GetMouseButton(0) && boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
                //Camera.main.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Force);
            }
        }


    }


}
