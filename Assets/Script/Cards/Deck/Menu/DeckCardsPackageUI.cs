using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Se encarga de mostrar en el hud las barajas
/// </summary>
public class DeckCardsPackageUI : MonoBehaviour
{
    public DeckCardsPackage my_deck;
    public Text texUI;
    // Start is called before the first frame update
    void Start()
    {
        texUI = GetComponent<Text>();
       
        
    }
    public void InitDeck(DeckCardsPackage deck) 
    {
        my_deck = deck;
        texUI.text = my_deck.get_name();
    }
    public void open_deck() 
    {
        //abrir menu de cartas 
    }
}
