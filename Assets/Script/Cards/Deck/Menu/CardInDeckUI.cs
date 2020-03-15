using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Tools.Interfaces;


public class CardInDeckUI : IInfoUIElement<InfoCard>
{
    public Text title;
    public Text cost;
    public Image background;
    private InfoCard my_info;
    private bool selected;

    /// <summary>
    /// Se usa en la creacción de mazo
    /// </summary>
    private DeckCollectionUI deck_collection;

    public void set_collection(DeckCollectionUI collection)
    {
        deck_collection = collection;
    }

    public override void fillInfo(InfoCard info)
    {
        SwipeDetector.OnSwipe += discard_card;
        cost.text = info.Cost.ToString();
        title.text = info.Name.ToString();
        background.color = select_color(info.Art);
        my_info = info;
        selected = false;
    }

    private Color select_color(string color) 
    {
        if (color == "ATTACKACTION")
        {
            return Color.red;
        }
        else if (color == "ATTACKANDMOVEMENT")
        {
            return Color.yellow;
        }
        else if (color == "GIVENMANA")
        {
            return Color.cyan;
        }
        else if (color == "MOVEMENT")
        {
            return Color.green;
        }
        else if (color == "TELEPORT")
        {
            return Color.blue;
        }
        else if (color == "TEMPLATE")
        {
            return Color.gray;
        }
        else 
        {
            return Color.white;
        }
    }
    private void discard_card(SwipeData data)
    {
        //if (RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), ))
        if (GameManager.Instance.state == States.INMENU)
        {
            if (selected)
            {
                if (data.Direction == SwipeDirection.Left)
                {
                    deck_collection.remove_card(my_info);
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