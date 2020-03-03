using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Informacion del canvas relativa a la baraja
/// </summary>
public class DeckCanvasInfo
{

    /// <summary>
    /// Componente del UI que almacena las imagenes
    /// </summary>
    public Image imageComponent;

    /// <summary>
    /// Obtiene una referencia al canvas
    /// </summary>
    public GameObject canvasGameObject;

    /// <summary>
    /// Diccionario que guarda las posiciones de las cartas y si estan ocupadas o no
    /// </summary>
    public List<AnchorInfo> anchorToCards;


    /// <summary>
    /// Obtiene el gameobject donde pinta el canvas
    /// </summary>
    /// <param name="tag">Parametro opcional que contiene el tag a buscar</param>
    public void getCanvasGameobject(string tag = "GameCanvas")
    {
        canvasGameObject = GameObject.FindGameObjectWithTag(tag);
    }

}