using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accion de movimiento del jugador
/// </summary>
public class DealCardsAction : CardAction
{
    /// <summary>
    /// Decide el tipo de carta ques es
    /// </summary>
    public new void Start()
    {
        cardType = ATTACKTYPE.DEALCARDSACTION;
        base.Start();
    }


    /// <summary>
    /// Comprobamos si la accion es posible o no
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public override bool checkAction(GameObject player)
    {
        DeckCanvasInfo info = GameManager.Instance.deck.deckCanvasInfo;

        foreach (AnchorInfo position in info.anchorToCards)
        {
            if (!position.state)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Realiza la accion de moverse a una casilla
    /// </summary>
    public override void DoAction(GameObject player)
    {
        GameManager GM = GameManager.Instance;

        StartCoroutine( GM.deck.DealCards() );

        finishTurn();

    }

}
