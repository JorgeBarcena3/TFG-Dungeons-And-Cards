using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCollection
{
    public List<DeckCardsPackage> deckCollection;
    public void new_deck(string name) 
    {
        deckCollection.Add(new DeckCardsPackage(name));
    }
    public void delete_deck(DeckCardsPackage my_deck) 
    {
        deckCollection.Remove(my_deck);
    }
   
}
