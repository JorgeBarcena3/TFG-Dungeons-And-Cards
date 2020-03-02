using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Da X mana al jugador
/// </summary>
public class GivenManaAction : CardAction
{
    public override bool checkAction(GameObject player)
    {
        return ( GameManager.Instance.player.playerInfo.currentManaPoints < GameManager.Instance.player.playerInfo.maxManaPoints );
    }

    public override void DoAction(GameObject player)
    {
        GameManager.Instance.deck.inCardAction = true;
        GameManager.Instance.player.playerInfo.addMana(GetComponent<Card>().info.Power);

        finishTurn();

    }

    protected override void finishTurn()
    {

        GameManager GM = GameManager.Instance;
        GM.deck.inCardAction = false;

        if (GM.turnManager.isIATurn())
        {
            GM.turn = TURN.IA;
        }

        GM.player.refreshPlayerData();

    }


}
