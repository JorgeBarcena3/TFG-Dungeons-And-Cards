using System.Linq;
using UnityEngine;

/// <summary>
/// Tipo de ataque posibles
/// </summary>
public enum ATTACKTYPE
{
    ATTACKACTION = 0,
    ATTACKANDMOVEMENT = 1,
    GIVENMANA = 2,
    MOVEMENT = 3,
    TELEPORT = 4,
    DEALCARDSACTION = 5,
    TEMPLATE
}

/// <summary>
/// Se encarga de unificar la accion que se debe hacer
/// </summary>
public abstract class CardAction : MonoBehaviour
{

    /// <summary>
    /// Tipo de carta
    /// </summary>
    public ATTACKTYPE cardType;

    /// <summary>
    /// Radio de vecinos
    /// </summary>
    [HideInInspector]
    public int radioVecinos;

    /// <summary>
    /// Actor que va a relizar la accion
    /// </summary>
    public GameObject actor;

    /// <summary>
    /// Devuelve la tile recomendada segun el tipo de carta
    /// **SOLO SE DEBE LLAMAR SI SOMOS UN AGENTE CONTROLADO POR LA IA**
    /// </summary>
    public virtual Tile recommendTile() { return null;  } 

    /// <summary>
    /// Realiza la accion 
    /// </summary>
    public abstract void DoAction();

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public virtual void clickOnTile(Tile tile) { }

    /// <summary>
    /// Determina si la accion se puede o no hacer
    /// </summary>
    public abstract bool checkAction();

    /// <summary>
    /// Funcion de start
    /// </summary>
    public void Start()
    {
        setRadio();
    }

    /// <summary>
    /// Determina el radio de accion de la carta
    /// </summary>
    /// <returns></returns>
    public string setRadio()
    {
        radioVecinos = this.gameObject.GetComponent<Card>().info.Power;
        return radioVecinos.ToString();
    }

    /// <summary>
    /// Determina que actor va a realizar la accion
    /// </summary>
    /// <param name="actor"></param>
    public void setActor(GameObject _actor)
    {
        actor = _actor;
        setRadio();
    }

    /// <summary>
    /// Finalizamos el turno
    /// </summary>
    protected virtual void finishTurn()
    {
        GameManager GM = GameManager.Instance;

        if (GM.turn == TURN.PLAYER)
        {
            GM.StartCoroutine(GM.hud.setLifePercentajeBar(actor.GetComponent<MapActor>().lifeManager.lifePercentage, 0.5f));


            GM.GameInfo.cartasUtilizadas.Add(GetComponent<Card>().info);
            FirebaseAnalyticsManager.Instance.sendCard(new CardInfoDto(FIREBASE_CARDSTATE.USED, GetComponent<Card>().info));

            GM.deck.inCardAction = false;
            GM.player.playerInfo.useMana(this.gameObject.GetComponent<Card>().info.Cost);

            if (GM.turnManager.isIATurn())
            {
                GM.turn = TURN.IA;
            }

            GM.player.refreshPlayerData();

        }
        else
        {
            GM.StartCoroutine(GM.hud.setLifePercentajeBar(GM.player.GetComponent<MapActor>().lifeManager.lifePercentage, 0.5f));

        }

    }
}