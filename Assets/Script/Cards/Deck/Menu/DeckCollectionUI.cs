﻿using System.Collections;
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
    private Vector2 panel_name_init_position;
    public GameObject panel_cards;
    private Vector2 panel_cards_init_position;
    public CSVReader parser;
    DeckCardsPackage my_deck;
    private Vector2 init_position;

    private void Start()
    {
        //print_deck_list();
        deck_collection = new DeckCollection();
        init_position = transform.parent.position;
        panel_name_init_position = new Vector2(panel_name.transform.position.x,0);
        panel_cards_init_position = new Vector2(panel_cards.transform.position.x,0);
        deck_collection.load_decks(parser, this);
        

    }
    /// <summary>
    /// vuelve al menu de inicio
    /// </summary>
    public void back_to_init_menu()
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(transform.parent, init_position, 1f));

        //Se vuelve a activar el menu de inicio y se vuelve a suscribir al delegado
        transform.parent.parent.transform.Find("play").gameObject.SetActive(true);
        transform.parent.parent.transform.Find("deck").gameObject.SetActive(true);
        transform.parent.parent.transform.Find("settings").gameObject.SetActive(true);
        transform.parent.parent.transform.Find("exit").gameObject.SetActive(true);

        SwipeDetector.OnSwipe += transform.parent.parent.transform.Find("play").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe += transform.parent.parent.transform.Find("deck").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe += transform.parent.parent.transform.Find("settings").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe += transform.parent.parent.transform.Find("exit").GetComponent<ButtonPlay>().action;
    }
    /// <summary>
    /// abre el menu para crear un mazo nuevo (panel del nombre de mazo)
    /// </summary>
    public void open_panel_name_deck()
    {
       
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_name.transform, new Vector2(-1, 0), 1f));
        panel_name.transform.Find("name").GetComponent<InputField>().text = "";

        //Cuando se entra en el menu de gestion de mazo se desactiba todo el menu inicial para que no interfiera
        transform.parent.parent.transform.Find("play").gameObject.SetActive(false);
        transform.parent.parent.transform.Find("deck").gameObject.SetActive(false);
        transform.parent.parent.transform.Find("settings").gameObject.SetActive(false);
        transform.parent.parent.transform.Find("exit").gameObject.SetActive(false);

        SwipeDetector.OnSwipe -= transform.parent.parent.transform.Find("play").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe -= transform.parent.parent.transform.Find("deck").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe -= transform.parent.parent.transform.Find("settings").GetComponent<ButtonPlay>().action;
        SwipeDetector.OnSwipe -= transform.parent.parent.transform.Find("exit").GetComponent<ButtonPlay>().action;
    }
    /// <summary>
    /// Cierra el panel del nombre
    /// </summary>
    public void close_panel_name_deck()
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_name.transform, panel_name_init_position, 1f));
    }
    /// <summary>
    /// Se crea un mazo y se activa la ventana de seleccion de cartas
    /// </summary>
    public void add_deck() 
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_cards.transform, new Vector2(-1, 0), 1f));
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_name.transform, panel_name_init_position, 1f));
        my_deck = new DeckCardsPackage(panel_name.transform.Find("name").transform.Find("Text").gameObject.GetComponent<Text>().text); ///creo un nuevo mazo
        panelDecks.set_collection(this);
        panelDecks.add_item(my_deck); ///se añade a la vita
       

        panel_cards.transform.Find("title").gameObject.GetComponent<Text>().text = my_deck.get_name();
        List<InfoCard> all_cards = parser.getCardsInfo();
        PanelListCards panel = panel_cards.transform.Find("frameCardList").GetChild(0).gameObject.GetComponent<PanelListCards>();
        panel.set_collection(this);
        for (int i = 0; i < all_cards.Count; i++)
        {
            panel.add_item(all_cards[i]);
        }
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().set_collection(this);
        panelDecks.gameObject.SetActive(false);

    }
    /// <summary>
    /// Enseña las cartas de contiene un mazo y permite edotarlo
    /// </summary>
    /// <param name="deck">mazo</param>
    public void edit_deck(DeckCardsPackage deck)
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_cards.transform, new Vector2(-1, 0), 1f));
        my_deck = deck;
        panel_cards.transform.Find("title").gameObject.GetComponent<Text>().text = my_deck.get_name();
        List<InfoCard> all_cards = parser.getCardsInfo();
        PanelListCards panel = panel_cards.transform.Find("frameCardList").GetChild(0).gameObject.GetComponent<PanelListCards>();
        panel.set_collection(this);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().set_collection(this);
        panel.add_list(all_cards);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().add_list(my_deck.get_cards());
        panelDecks.gameObject.SetActive(false);


    }
    /// <summary>
    /// Selecciona el mazo con el que se va a jugar
    /// </summary>
    /// <param name="deck">mazo</param>
    public void select_deck(DeckCardsPackage deck)
    {
        panelDecks.select_deck(deck_collection.select_deck(deck));
    }
    /// <summary>
    /// Añade una carta al mazo que estamos creando
    /// </summary>
    /// <param name="info"></param>
    public void add_card(InfoCard info) 
    {

        my_deck.add_card(info);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().add_item(info);
       
    }
    /// <summary>
    /// Elimina una carta del mazo
    /// </summary>
    /// <param name="info"></param>
    public void remove_card(InfoCard info)
    {
        my_deck.delete_card(info);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().delete_item(info);
    }
    /// <summary>
    /// Guarda los cambios realizados o el mazo nuevo
    /// </summary>
    public void save_deck() 
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_cards.transform, panel_cards_init_position, 1f));
        deck_collection.new_deck(my_deck as DeckCardsPackage);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().Reset();
        panel_cards.transform.Find("frameCardList").GetChild(0).gameObject.GetComponent<PanelListCards>().Reset();
        deck_collection.save_decks();
        panelDecks.gameObject.SetActive(true);


    }
    /// <summary>
    /// cancela el mazo o elimina el mazo que se estubiese editando
    /// </summary>
    public void cancel_deck()
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(panel_cards.transform, panel_cards_init_position, 1f));
        panelDecks.GetComponent<PanelListDeckCardsPackage>().delete_item(my_deck);
        deck_collection.delete_deck(my_deck);
        panel_cards.transform.Find("frameCardInDeck").GetChild(0).GetComponent<PanelListCardsInDeck>().Reset();
        panel_cards.transform.Find("frameCardList").GetChild(0).gameObject.GetComponent<PanelListCards>().Reset();
        panelDecks.gameObject.SetActive(true);
    }

    public void load_deck()
    {
        panelDecks.set_collection(this);
        panelDecks.add_list(deck_collection.deckCollection);
    }

}
 