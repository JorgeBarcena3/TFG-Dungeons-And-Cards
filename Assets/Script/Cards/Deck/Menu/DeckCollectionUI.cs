using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Assets.Script.Tools;

public class DeckCollectionUI : MonoBehaviour
{
    DeckCollection deck_collection;
    public PanelListDeckCardsPackage panelDecks;
    public GameObject panel_name;
    public GameObject panel_cards;
    public CSVReader parser;
    private int deck_selected;
    DeckCardsPackage my_deck;

    private void Start()
    {
        //print_deck_list();
        deck_collection = new DeckCollection();

    }

    public void back_to_init_menu()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void open_panel_name_deck()
    {
        panel_name.SetActive(true);
        panel_name.transform.Find("name").GetComponent<InputField>().text = "";
    }
    public void close_panel_name_deck()
    {
        panel_name.SetActive(false);
    }
    /// <summary>
    /// Se crea un mazo y se activa la ventana de seleccion de cartas
    /// </summary>
    public void add_deck() 
    {
        panel_cards.SetActive(true);

        //my_deck = deck_collection.new_deck(panel_name.transform.Find("name").transform.Find("Text").gameObject.GetComponent<Text>().text);
        my_deck = new DeckCardsPackage(panel_name.transform.Find("name").transform.Find("Text").gameObject.GetComponent<Text>().text); ///creo un nuevo mazo
        panelDecks.add_item(my_deck); ///se añade a la vita

        panel_cards.transform.Find("title").gameObject.GetComponent<Text>().text = my_deck.get_name();
        List<InfoCard> all_cards = parser.getCardsInfo();
        PanelListCards panel = panel_cards.transform.Find("cardList").gameObject.GetComponent<PanelListCards>();
        panel.set_collection(this);
        for (int i = 0; i < all_cards.Count; i++)
        {
            panel.add_item(all_cards[i]);
        }
        panel_name.SetActive(false);

    }
    /// <summary>
    /// Añade una carta al mazo que estamos creando
    /// </summary>
    /// <param name="info"></param>
    public void add_card(InfoCard info) 
    {

        my_deck.add_card(info);
        panel_cards.transform.Find("cradsInDeck").GetComponent<PanelListCardsInDeck>().add_item(info);
        //GameObject card = Instantiate(card_in_deck_prefab,default,default, panel_cards.transform.Find("cradsInDeck"));
        //card.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
        //card.GetComponent<CardInDeckUI>().fillInfo(info);
    }

    public void save_deck() 
    {
        panel_cards.SetActive(false);
        deck_collection.new_deck(my_deck as DeckCardsPackage);
        panel_cards.transform.Find("cradsInDeck").GetComponent<PanelListCardsInDeck>().Reset();
        panel_cards.transform.Find("cardList").gameObject.GetComponent<PanelListCards>().Reset();
        deck_selected = -1;


    }
    public void cancel_deck()
    {
        panelDecks.GetComponent<PanelListDeckCollection>().delete_last();
        panel_cards.transform.Find("cradsInDeck").GetComponent<PanelListCardsInDeck>().Reset();
        panel_cards.transform.Find("cardList").gameObject.GetComponent<PanelListCards>().Reset();
        panel_cards.SetActive(false);
        deck_selected = -1;
    }

    private void print_deck_list() 
    {
        if (deck_collection != null)
        {
            for (int i = 0; i < deck_collection.deckCollection.Count; i++)
            {
                
                //GameObject deck = Instantiate(deckPrefab,default,default,panelDecks.transform);
                //deck.GetComponent<DeckCardsPackageUI>().InitDeck(deck_collection.deckCollection[i]);
                //deck.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            deck_collection = new DeckCollection();
        }
    }

   
}
 