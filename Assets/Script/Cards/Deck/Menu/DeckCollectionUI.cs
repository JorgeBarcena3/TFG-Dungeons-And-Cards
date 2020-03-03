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
    public CSVReader parser;

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

    public void  open_panel_name_deck() 
    {
        panel_name.SetActive(true);
        panel_name.GetComponentInChildren<Text>().text = "Enter name...";
    }
    public void close_panel_name_deck()
    {
        panel_name.SetActive(false);
    }

    public void add_deck() 
    {
        panel_cards.SetActive(true);
        DeckCardsPackage my_deck = deck_collection.new_deck(panel_name.GetComponentInChildren<Text>().text);
        panel_cards.transform.FindChild("title").gameObject.GetComponent<Text>().text = my_deck.get_name();
        List<InfoCard> all_cards = parser.getCardsInfo();
        for (int i = 0; i < all_cards.Count; i++) 
        {
            GameObject cardHud = Instantiate(cardPrefab);
            cardHud.transform.SetParent(panel_cards.transform.FindChild("cardList"));
            cardHud.transform.FindChild("title").gameObject.GetComponent<Text>().text = all_cards[i].Name;
            cardHud.transform.FindChild("description").gameObject.GetComponent<Text>().text = all_cards[i].Description;
            //cardHud.transform.FindChild("Button").gameObject.GetComponent<Button>().
            //cardHud.GetComponent<Image>().sprite = Resources.Load<Sprite>("Assets/Art/Preview/Cards/Template/" + all_cards[i].Art);
            
        }
        //panel_cards.transform.FindChild("cardList");
    }

   
}
