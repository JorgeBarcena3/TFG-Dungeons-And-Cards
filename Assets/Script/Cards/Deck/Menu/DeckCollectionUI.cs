using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DeckCollectionUI : MonoBehaviour
{
    DeckCollection deck_collection;
    public GameObject deckPrefab;
    public GameObject cardPrefab;
    public GameObject panel_name;
    public GameObject panel_cards;
    public GameObject card_in_deck_prefab;
    public CSVReader parser;

    private void Start()
    {
        if (deck_collection != null)
        {
            for (int i = 0; i < deck_collection.deckCollection.Count; i++)
            {
                GameObject deck = Instantiate(deckPrefab);
                deck.GetComponent<DeckCardsPackageUI>().my_deck = deck_collection.deckCollection[i];
                deck.transform.SetParent(gameObject.transform.parent);
            }
        }
        else 
        {
            deck_collection = new DeckCollection();
        }
       
    }


    public void  open_panel_name_deck() 
    {
        panel_name.SetActive(true);
        panel_name.transform.Find("name").transform.Find("Text").gameObject.GetComponent<Text>().text = "";
    }
    public void close_panel_name_deck()
    {
        panel_name.SetActive(false);
    }

    public void add_deck() 
    {
        panel_cards.SetActive(true);
        DeckCardsPackage my_deck = deck_collection.new_deck(panel_name.transform.Find("name").transform.Find("Text").gameObject.GetComponent<Text>().text);
        panel_cards.transform.Find("title").gameObject.GetComponent<Text>().text = my_deck.get_name();
        List<InfoCard> all_cards = parser.getCardsInfo();
        for (int i = 0; i < all_cards.Count; i++) 
        {
            GameObject cardHud = Instantiate(cardPrefab);
            cardHud.transform.SetParent(panel_cards.transform.Find("cardList"));
            cardHud.GetComponentInChildren<HUDCard>().fillInfo(all_cards[i]);
            cardHud.GetComponentInChildren<HUDCard>().set_collection(this);
            
           
            
        }
    }
    /// <summary>
    /// Añade una carta al mazo que estamos creando
    /// </summary>
    /// <param name="info"></param>
    public void add_card(InfoCard info) 
    {
        deck_collection.deckCollection[deck_collection.deckCollection.Count-1].add_card(info);
        GameObject card = Instantiate(card_in_deck_prefab);
        card.transform.SetParent(panel_cards.transform.Find("cradsInDeck"));
        card.GetComponent<CardInDeckUI>().fillInfo(info);
    }

   
}
