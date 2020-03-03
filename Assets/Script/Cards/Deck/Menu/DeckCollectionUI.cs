using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCollectionUI : MonoBehaviour
{
    DeckCollection deck_collection;
    public GameObject deckPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < deck_collection.deckCollection.Count; i++) 
        {
            GameObject deck = Instantiate(deckPrefab);
            deck.GetComponent<DeckCardsPackageUI>().my_deck = deck_collection.deckCollection[i];
            deck.transform.SetParent(gameObject.transform.parent);
        }
        
    }

    public void  add_deck() 
    {
        //TODO
    }

   
}
