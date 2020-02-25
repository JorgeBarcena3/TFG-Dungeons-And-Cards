using UnityEngine;

/// <summary>
/// Se encarga de unificar la accion que se debe hacer
/// </summary>
public abstract class CardAction : MonoBehaviour
{
    /// <summary>
    /// Realiza la accion 
    /// </summary>
    public abstract void DoAction(GameObject player);

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public abstract void clickOnTile(Tile tile);

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public abstract bool checkAction(GameObject player);

    /// <summary>
    /// Finalizamos el turno
    /// </summary>
    protected void finishTurn()
    {
        GameManager.GetInstance().turn = TURN.IA;
    }
}