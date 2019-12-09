using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tipos de cartas 
/// </summary>
public enum CardKind 
{
    MOVE,       //<<< de movimiento
    DAMAGE,     //<<< de daño
    POISON,     //<<< de veneno
    GUARD,      //<<< de defensa
    SPECIAL,    //<<< especial
}
/// <summary>
/// Modelo de datos de las cartas
/// </summary>
public class InfoModel
{
    /// <summary>
    /// Tipo de carta
    /// </summary>
    private CardKind card_kind;
    /// <summary>
    /// Precio de la carta
    /// </summary>
    private int cost;
    /// <summary>
    /// Distancia que alcanza
    /// </summary>
    private int distance;
    /// <summary>
    /// radio del area en el que afecta 
    /// </summary>
    private int area_radius;
    /// <summary>
    /// Turnos que dura el efecto
    /// </summary>
    private int turn;


    /// <summary>
    /// Constructor de info de la carta
    /// </summary>
    /// <param name="_card_kind">Indica el tipo de carta</param>
    /// <param name="_cost">coste de la carta</param>
    /// <param name="_distance">distancia de accion</param>
    /// <param name="_area_radius">area de accion</param>
    /// <param name="_turn">turnos de accion</param>
    public InfoModel(CardKind _card_kind, int _cost = 0, int _distance = 1, int _area_radius = 0, int _turn = 1 ) 
    {
        card_kind = _card_kind;
        cost = _cost;
        distance = _distance;
        area_radius = _area_radius;
        turn = _turn;

    }


} 