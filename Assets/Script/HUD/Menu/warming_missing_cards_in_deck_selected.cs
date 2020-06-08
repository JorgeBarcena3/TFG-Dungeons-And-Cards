using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Hay un warning de que faltan cartas por completar una baraja
/// </summary>
public class warming_missing_cards_in_deck_selected : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DeckCollection.currentDeck != null) 
        {
            if (DeckCollection.currentDeck.get_cards().Count < 15)
            {
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
        }
        
        
    }
}
