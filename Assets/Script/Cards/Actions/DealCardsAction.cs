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
        setRadio();
    }


    /// <summary>
    /// Comprobamos si la accion es posible o no
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public override bool checkAction()
    {

        if (!GameManager.Instance.player.playerInfo.canUseMana(this.gameObject.GetComponent<Card>().info.Cost))
            return false;

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
    public override void DoAction()
    {
        GameManager GM = GameManager.Instance;

        StartCoroutine( GM.deck.DealCards() );

        finishTurn();

    }

}
