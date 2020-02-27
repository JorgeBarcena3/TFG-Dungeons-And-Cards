
/// <summary>
/// Tipos de cartas 
/// </summary>
public enum CardKind
{
    MOVE,       //<<< de movimiento
    DAMAGE,     //<<< de daño
    POISON,     //<<< de veneno
    GUARD,      //<<< de defensa
    SPECIAL,    //<<< especial
}
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
    /// Descripcion del funcionamiento o uso de la carta
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    /// Tipo de carta
    /// </summary>
    public CardKind Card_kind { get; private set; }
    /// <summary>
    /// Precio de la carta
    /// </summary>
    public int Cost { get; private set; }
    /// <summary>
    /// Indica la fuerza de la carta, si es de daño indicaria cuanto daño, si es de defensa cuanta defensa, etc
    /// </summary>
    public int Power { get; private set; }
    /// <summary>
    /// Distancia que alcanza
    /// </summary>
    public int Distance { get; private set; }
    /// <summary>
    /// radio del area en el que afecta 
    /// </summary>
    public int AreaRadius { get; private set; }
    /// <summary>
    /// Turnos que dura el efecto
    /// </summary>
    public int Turn { get; private set; }


    /// <summary>
    /// Constructor de info de la carta
    /// </summary>
    /// <param name="_card_kind">Indica el tipo de carta</param>
    /// <param name="_cost">coste de la carta</param>
    /// <param name="_distance">distancia de accion</param>
    /// <param name="_area_radius">area de accion</param>
    /// <param name="_turn">turnos de accion</param>
    public InfoCard(CardKind _card_kind, int _id, string _description ,int _cost = 0, int _power = 1, int _distance = 1, int _area_radius = 0, int _turn = 1)
    {
        Card_kind = _card_kind;
        //los primeros numeros del id indican el tipo de carta, los ultimos 5 numeros indican las variables de esta
        Id = ((((_id * 10 + _cost) * 10 + _power) * 10 + _distance) * 10 + _area_radius) * 10 + _turn;
        Description = _description;
        Cost = _power;
        Power = _power;
        Distance = _distance;
        AreaRadius = _area_radius;
        Turn = _turn;

    }
}