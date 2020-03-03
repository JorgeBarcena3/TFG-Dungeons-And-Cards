
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

    public DeckCardsPackage() {}

    public DeckCardsPackage(string name)
    {
        this.name = name; 
    }
    
    public void add_card(InfoCard card) 
    {
        cards.Add(card);
    }
    public void delete_card(InfoCard card) 
    {
        cards.Remove(card);
    }
    public string get_name() 
    {
        return name;
    }
    public List<InfoCard> get_cards() 
    {
        return cards;
    }
    
}
