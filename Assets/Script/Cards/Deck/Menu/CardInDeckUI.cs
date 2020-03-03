using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardInDeckUI : MonoBehaviour
{
    public Text title;
    public Text cost;
    public Image background;

    public void fillInfo(InfoCard info)
    {
        cost.text = info.Cost.ToString();
        title.text = info.Name.ToString();
        background.color = select_color(info.Art);
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
}