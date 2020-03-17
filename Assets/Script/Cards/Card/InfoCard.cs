using UnityEngine;
/// <summary>
/// Modelo de datos de las cartas
/// </summary>
public class InfoCard 
{
    /// <summary>
    /// Identificador de la carta
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// Nombre de la carta
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Descripcion del funcionamiento o uso de la carta
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    /// Tipo de carta
    /// </summary>
    public ATTACKTYPE Card_kind { get; private set; }

    /// <summary>
    /// Precio de la carta
    /// </summary>
    public int Cost { get; private set; }

    /// <summary>
    /// Indica la fuerza de la carta, si es de daño indicaria cuanto daño, si es de defensa cuanta defensa, etc
    /// </summary>
    public int Power { get; private set; }
   
    /// <summary>
    /// Turnos que dura el efecto
    /// </summary>
    public int Turn { get; private set; }
    /// <summary>
    /// Indica a que distancia tiene efecto
    /// </summary>
    public int Distance { get; private set; }
    /// <summary>
    /// Indica el aspecto de la carta
    /// </summary>
    public string Art { get; private set; }
    /// <summary>
    /// Indica si esta carta esta desbloqueada
    /// </summary>
    public bool Possession { get; private set; }


    /// <summary>
    /// Constructor de info de la carta
    /// </summary>
    /// <param name="_card_kind">Indica el tipo de carta</param>
    /// <param name="_cost">coste de la carta</param>
    /// <param name="_turn">turnos de accion</param>
    public InfoCard(ATTACKTYPE _card_kind, int _id, string _name, string _description, int _cost = 0, int _power = 1, int _turn = 0, int _distance = 1, string _art = "",  bool _posesion = false)
    {
        Card_kind = _card_kind;
        //los primeros numeros del id indican el tipo de carta, los ultimos 5 numeros indican las variables de esta
        Id = ((_id * 10 + _cost) * 10 + _power)  * 10 + _turn;
        Name = _name;
        Description = _description;
        Cost = _cost;
        Power = _power;
        Turn = _turn;
        Distance = _distance;
        Art = _art;
        Possession = _posesion;


    }
}