using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DeckCollectionUI : MonoBehaviour
{
    DeckCollection deck_collection;
    public GameObject panelDecks;
    public GameObject deckPrefab;
    public GameObject cardPrefab;
    public GameObject panel_name;
    public GameObject panel_cards;
    public GameObject card_in_deck_prefab;
    public CSVReader parser;

    private void Start()
    {
        print_deck_list();


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
            GameObject cardHud = Instantiate(cardPrefab,default,default, panel_cards.transform.Find("cardList"));
            //cardHud.transform.SetParent(panel_cards.transform.Find("cardList"));
            cardHud.GetComponentInChildren<HUDCard>().fillInfo(all_cards[i]);
            cardHud.GetComponentInChildren<HUDCard>().set_collection(this);
            cardHud.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;




        }
    }
    /// <summary>
    /// Añade una carta al mazo que estamos creando
    /// </summary>
    /// <param name="info"></param>
    public void add_card(InfoCard info) 
    {
        deck_collection.deckCollection[deck_collection.deckCollection.Count-1].add_card(info);
        GameObject card = Instantiate(card_in_deck_prefab,default,default, panel_cards.transform.Find("cradsInDeck"));
        card.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
        card.GetComponent<CardInDeckUI>().fillInfo(info);
    }

    public void save_deck() 
    {
        panel_cards.SetActive(false);
        print_deck_list();
    }
    public void cancel_deck() 
    {
        deck_collection.delete_deck(deck_collection.deckCollection[deck_collection.deckCollection.Count-1]);
        panel_cards.SetActive(false);
        print_deck_list();
    }

    private void print_deck_list() 
    {
        if (deck_collection != null)
        {
            for (int i = 0; i < deck_collection.deckCollection.Count; i++)
            {
                GameObject deck = Instantiate(deckPrefab,default,default,panelDecks.transform);
                deck.GetComponent<DeckCardsPackageUI>().InitDeck(deck_collection.deckCollection[i]);
                deck.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            deck_collection = new DeckCollection();
        }
    }

   
}
