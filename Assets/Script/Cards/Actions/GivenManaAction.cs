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
        return ( GameManager.GetInstance().player.playerInfo.currentManaPoints < GameManager.GetInstance().player.playerInfo.maxManaPoints );
    }

    public override void DoAction(GameObject player)
    {
        GameManager.GetInstance().deck.inCardAction = true;
        GameManager.GetInstance().player.playerInfo.addMana(GetComponent<Card>().info.Power);

        finishTurn();

    }

    protected override void finishTurn()
    {

        GameManager GM = GameManager.GetInstance();
        GM.deck.inCardAction = false;

        if (GM.turnManager.isIATurn())
        {
            GM.turn = TURN.IA;
        }

        GM.player.refreshPlayerData();

    }


}
