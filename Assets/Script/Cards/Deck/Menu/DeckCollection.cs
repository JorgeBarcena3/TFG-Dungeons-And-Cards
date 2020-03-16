using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckCollection
{
    /// <summary>
    /// Almacena todos los mazos creados
    /// </summary>
    public List<DeckCardsPackage> deckCollection = new List<DeckCardsPackage>();
    /// <summary>
    /// indica el mazo seleccionado para la partida
    /// </summary>
    public int deck_selected;
    /// <summary>
    /// Crea un mazo nuevo
    /// </summary>
    /// <param name="name">nomre del mazo</param>
    /// <returns>referencia del mazo</returns>
    public DeckCardsPackage new_deck(string name) 
    {
        DeckCardsPackage my_deck = new DeckCardsPackage(name);
        deckCollection.Add(my_deck);
        return my_deck;
    }
    /// <summary>
    /// guarda un mazo en la lista de mazos
    /// </summary>
    /// <param name="deck">mazo</param>
    /// <returns>mazo</returns>
    public DeckCardsPackage new_deck(DeckCardsPackage deck)
    {
        deckCollection.Add(deck);
        return deck;
    }
    /// <summary>
    /// Elimina un mazo
    /// </summary>
    /// <param name="my_deck">referencia del mazo</param>
    public void delete_deck(DeckCardsPackage my_deck) 
    {
        deckCollection.Remove(my_deck);
    }
    /// <summary>
    /// retorna el mazo seleccionado
    /// </summary>
    /// <returns></returns>
    public DeckCardsPackage get_deck_selected()
    {
        return deckCollection[deck_selected] as DeckCardsPackage;
    }

    public int select_deck(DeckCardsPackage deck)
    {
        for (int i = 0; i < deckCollection.Count; i++)
        {
            if (deck == deckCollection[i])
            {
                deck_selected = i;
                break;
            }
        }
        return deck_selected;
    }
   
}
