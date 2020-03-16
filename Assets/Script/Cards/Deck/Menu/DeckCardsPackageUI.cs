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
    private bool selected;

    /// <summary>
    /// Se usa en la creacción de mazo
    /// </summary>
    private DeckCollectionUI deck_collection;

    public void set_collection(DeckCollectionUI collection)
    {
        deck_collection = collection;
    }



    public override void fillInfo(DeckCardsPackage info)
     {
        my_deck = info;
        if (texUI == null)
            texUI = GetComponent<Text>();

        texUI.text = my_deck.get_name().ToString();
        selected = false;
        SwipeDetector.OnSwipe += open_deck;
    }

    public void select_deck_to_play()
    {
        if (texUI == null)
            texUI = GetComponent<Text>();

        texUI.color = Color.green;
    }
    public void unselect_deck_to_play()
    {
        if (texUI == null)
            texUI = GetComponent<Text>();

        texUI.color = Color.white;
    }

    public void open_deck(SwipeData data)
    {
        if (GameManager.Instance.state == States.INMENU /*&& !gameObject.transform.parent.gameObject.transform.parent.Find("add_deck").gameObject.active*/)
        {
            if (selected)
            {
                if (data.Direction == SwipeDirection.Right)
                {
                    deck_collection.edit_deck(my_deck);
                }
                else if (data.Direction == SwipeDirection.Left)
                {
                    deck_collection.select_deck(my_deck);
                }
                else
                {
                    selected = false;
                }
            }

        }
    }

    void OnMouseDown()
    {
        selected = true;
    }
}
