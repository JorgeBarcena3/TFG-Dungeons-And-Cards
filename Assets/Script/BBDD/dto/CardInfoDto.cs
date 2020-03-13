using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Estado de la carta
/// </summary>
public enum FIREBASE_CARDSTATE
{
    DISCARD,
    USED,
    NONE
}

/// <summary>
/// Informacion de la carta que queremos enviar a la bbdd
/// </summary>
public class CardInfoDto
{
    /// <summary>
    /// Estado de la carta
    /// </summary>
    public FIREBASE_CARDSTATE EstadoDeCarta { get; set; }

    /// <summary>
    /// Nombre de la carta
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Tipo de carta
    /// </summary>
    public ATTACKTYPE Card_kind { get; set; }

    /// <summary>
    /// Precio de la carta
    /// </summary>
    public int Cost { get; set; }

    /// <summary>
    /// Indica la fuerza de la carta, si es de daño indicaria cuanto daño, si es de defensa cuanta defensa, etc
    /// </summary>
    public int Power { get; set; }

    /// <summary>
    /// Constructor de la carta
    /// </summary>
    public CardInfoDto(
         FIREBASE_CARDSTATE _estadoDeCarta,
         string             _Name,
         ATTACKTYPE         _Card_kind,
         int                _cost,
         int                _Power
        )
    {
        this.EstadoDeCarta = _estadoDeCarta;
        this.Name = _Name;
        this.Card_kind = _Card_kind;
        this.Cost = _cost;
        this.Power = _Power;
    } 
    
    /// <summary>
    /// Constructor de la carta
    /// </summary>
    public CardInfoDto(
         FIREBASE_CARDSTATE _estadoDeCarta,
         InfoCard info
        )
    {
        this.EstadoDeCarta = _estadoDeCarta;
        this.Name =      info.Name;
        this.Card_kind = info.Card_kind;
        this.Cost =      info.Cost;
        this.Power =     info.Power;
    }

     
}

