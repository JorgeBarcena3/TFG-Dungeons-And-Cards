using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// De quien es el turno en cada momento
/// </summary>
public enum TURN
{
    PLAYER = 0,
    IA = 1
}

/// <summary>
/// Manager del turno
/// </summary>
public class TurnManager : MonoBehaviour
{

    /// <summary>
    /// Quien tiene el turno actualmente
    /// </summary>
    public TURN? turn = null;

    /// <summary>
    /// Numero de turnos
    /// </summary>
    private int turnNumber = 0;

    /// <summary>
    /// Update del GameManager
    /// </summary>
    public IEnumerator TurnUpdate()
    {
        GameManager GM = GameManager.GetInstance();

        while (GameManager.GetInstance().state == States.INGAME)
        {
            if (turn == TURN.IA)
            {
                turnNumber++;

                GM.checkEndGame();

                if (GM.deck.deckCanvasInfo.anchorToCards.Where(m => !m.state).Count() > 0)
                {
                    StartCoroutine(GM.deck.DealCards());
                    yield return new WaitUntil(() => GM.deck.deckCanvasInfo.anchorToCards.Where(m => !m.state).Count() == 0);
                }

                GM.player.playerInfo.addMana(1);
                GM.player.refreshPlayerData();

                //Mostramos la animacion del IA
                GM.hud.turnlbl.showTurn("ENEMIGOS");
                yield return new WaitForSeconds(GM.hud.turnlbl.getTimeAnimation());

                StartCoroutine(GM.IA.doAction(GM.agents));
                yield return new WaitUntil(() => GM.IA.actionDone);

                GM.cameraFunctions.moveCameraTo(GM.player.transform.position);
                yield return new WaitForSeconds(1.5f);

                //Mostramos la animacion del player
                GM.hud.turnlbl.showTurn("JUGADOR");
                yield return new WaitForSeconds(GM.hud.turnlbl.getTimeAnimation());

                turn = TURN.PLAYER;

            }

            yield return null;
        }

    }

    /// <summary>
    /// Es turno de la IA
    /// </summary>
    /// <returns></returns>
    public bool isIATurn()
    {
        GameManager GM = GameManager.GetInstance();

        return ( GM.player.playerInfo.currentManaPoints <= 0 ||
                GM.deck.deckCanvasInfo.anchorToCards.Where(m => !m.state).Count() == GM.deck.deckCanvasInfo.anchorToCards.Count - 1);
    }

    /// <summary>
    /// Pasa de turno
    /// </summary>
    public void changeTurnToIA()
    {
        if (turn == TURN.PLAYER && !GameManager.GetInstance().deck.inCardAction)
            turn = TURN.IA;
    }


}
