using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBackground : MonoBehaviour
{
    /// <summary>
    /// Baraja de la partida
    /// </summary>
    public Deck deck;

    /// <summary>
    /// determina si el objeto está en una transicion o no
    /// </summary>
    public static bool IS_TRANSITION = false;

#if UNITY_EDITOR

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    void OnMouseDown()
    {
        //if (!IS_TRANSITION)
        //   deck.ClickOnCard();
    }

#endif
}
