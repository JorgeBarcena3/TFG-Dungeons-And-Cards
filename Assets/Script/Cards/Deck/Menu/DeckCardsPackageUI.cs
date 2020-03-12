using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Tools.Interfaces;

/// <summary>
/// Se encarga de mostrar en el hud las barajas
/// </summary>
public class DeckCardsPackageUI : IInfoUIElement<DeckCardsPackage>
{
    public DeckCardsPackage my_deck;
    public Text texUI;
    // Start is called before the first frame update
    
    public void open_deck() 
    {
        //abrir menu de cartas 
    }

    public override void fillInfo(DeckCardsPackage info)
    {
        my_deck = info;
        if (texUI == null)
            texUI = GetComponent<Text>();

        texUI.text = my_deck.get_name().ToString();
    }
}
