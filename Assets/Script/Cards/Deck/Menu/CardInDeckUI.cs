using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Informacion de la carta mostrada en la UI
/// </summary>
public class CardInDeckUI : IInfoUIElement<InfoCard>
{
    /// <summary>
    /// Campo del titulo
    /// </summary>
    public Text title;

    /// <summary>
    /// Campo del coste
    /// </summary>
    public Text cost;

    /// <summary>
    /// Background de la imagen
    /// </summary>
    public Image background;

    /// <summary>
    /// INformacion de la carta
    /// </summary>
    private InfoCard my_info;

    /// <summary>
    /// Si esta seleccionada o no
    /// </summary>
    private bool selected;

    /// <summary>
    /// Se usa en la creacción de mazo
    /// </summary>
    private DeckCollectionUI deck_collection;

    public void set_collection(DeckCollectionUI collection)
    {
        deck_collection = collection;
    }
    /// <summary>
    /// rellena la lista con una carta mas
    /// </summary>
    /// <param name="info">carta</param>
    public override void fillInfo(InfoCard info)
    {
        SwipeDetector.OnSwipe += discard_card;
        cost.text = info.Cost.ToString();
        title.text = info.Name.ToString();
        background.color = select_color(info.Art);
        my_info = info;
        selected = false;
    }
    /// <summary>
    /// determina el color de la carta dependiendo del tipo de carta
    /// </summary>
    /// <param name="color">tipo de carta</param>
    /// <returns></returns>
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
    /// <summary>
    /// descarta una carta
    /// </summary>
    /// <param name="data">gesto del dedo en la pantalla</param>
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
    private void OnDestroy()
    {
        SwipeDetector.OnSwipe -= discard_card;
    }

}