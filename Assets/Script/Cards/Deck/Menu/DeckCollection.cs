using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCollection
{
    public List<DeckCardsPackage> deckCollection = new List<DeckCardsPackage>();
    public DeckCardsPackage new_deck(string name) 
    {
        DeckCardsPackage my_deck = new DeckCardsPackage(name);
        deckCollection.Add(my_deck);
        return my_deck;
    }
    public DeckCardsPackage new_deck(DeckCardsPackage deck)
    {
        deckCollection.Add(deck);
        return deck;
    }
    public void delete_deck(DeckCardsPackage my_deck) 
    {
        deckCollection.Remove(my_deck);
    }
   
}
