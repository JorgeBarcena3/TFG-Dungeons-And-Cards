
using System.Collections.Generic;

/// <summary>
/// Contiene una lista de cartas
/// </summary>
public class DeckCardsPackage 
{
    
    /// <summary>
    /// Lista de cartas
    /// </summary>
    List<InfoCard> cards;
    /// <summary>
    /// nombre de la baraja
    /// </summary>
    string name;
    /// <summary>
    /// Constructor de clase por defecto
    /// </summary>
    public DeckCardsPackage() { cards = new List<InfoCard>(); }
    /// <summary>
    /// Constructor de clase que crea un mazo con su nombre y una serie de cartas
    /// </summary>
    /// <param name="name">nombre del mazo</param>
    /// <param name="card_list">lista de cartas</param>
    public DeckCardsPackage(string name, List<InfoCard> card_list)
    {
        cards = card_list;
        this.name = name;
    }
    /// <summary>
    /// constructor de clase que inicia con un nombre el mazo
    /// </summary>
    /// <param name="name">nombre del mazo</param>
    public DeckCardsPackage(string name)
    {
        this.name = name;
        cards = new List<InfoCard>();
    }
    /// <summary>
    /// añade una carta al mazo
    /// </summary>
    /// <param name="card">carta</param>
    public void add_card(InfoCard card) 
    {
        cards.Add(card);
    }
    /// <summary>
    /// elimina una carta del mazo
    /// </summary>
    /// <param name="card">carta</param>
    public void delete_card(InfoCard card) 
    {
        cards.Remove(card);
    }
    /// <summary>
    /// retorna el nombre del mazo
    /// </summary>
    /// <returns></returns>
    public string get_name() 
    {
        return name;
    }
    /// <summary>
    /// retorna una lista de cartas
    /// </summary>
    /// <returns>lista de cartas del mazo</returns>
    public List<InfoCard> get_cards() 
    {
        return cards;
    }
    
}
