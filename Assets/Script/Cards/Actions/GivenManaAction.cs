using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Da X mana al jugador
/// </summary>
public class GivenManaAction : CardAction
{

    /// <summary>
    /// Decide el tipo de carta ques es
    /// </summary>
    public new void Start()
    {
        cardType = ATTACKTYPE.GIVENMANA;
        setRadio();
    }

    public override bool checkAction()
    {
        return ( GameManager.Instance.player.playerInfo.currentManaPoints < GameManager.Instance.player.playerInfo.maxManaPoints );
    }

    public override void DoAction()
    {
        GameManager.Instance.deck.inCardAction = true;
        GameManager.Instance.player.playerInfo.addMana(GetComponent<Card>().info.Power);

        finishTurn();

    }

    protected override void finishTurn()
    {

        GameManager GM = GameManager.Instance;
        GM.deck.inCardAction = false;
        GM.GameInfo.cartasUtilizadas.Add(GetComponent<Card>().info);
        FirebaseAnalyticsManager.Instance.sendCard(new CardInfoDto(FIREBASE_CARDSTATE.USED, GetComponent<Card>().info));

        if (GM.turnManager.isIATurn())
        {
            GM.turn = TURN.IA;
        }

        GM.player.refreshPlayerData();

    }


}
