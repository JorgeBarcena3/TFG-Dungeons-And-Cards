using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tipo de ataque posibles
/// </summary>
public enum ATTACKTYPE
{
    MOVEMENT = 0,
    ATTACK = 1,
    DEFENSE = 2,
    SPECIAL = 3
}

/// <summary>
/// Clase que guarda la informacion de una carta
/// </summary>
public class Card : MonoBehaviour
{
    /// <summary>
    /// Tipo de carta
    /// </summary>
    public ATTACKTYPE type { get; private set; }

    /// <summary>
    /// Sprite de la carta en cuestion
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// Informacion relativa a cada carta
    /// </summary>
    private InfoCard info;

    /// <summary>
    /// Componente del UI que almacena las imagenes
    /// </summary>
    private Image ImageComponent;

    /// <summary>
    /// Baraja a la que pertenece
    /// </summary>
    private Deck deck;

    /// <summary>
    /// En caso de que esté en la mano del jugador, en que posicion está
    /// </summary>
    public int? indexPosition = null;

    /// <summary>
    /// Gameobject que almacena el background de la carta
    /// </summary>
    public GameObject background;

    /// <summary>
    /// Gameobject que almacena el front de la carta
    /// </summary>
    public GameObject front;

    /// <summary>
    /// Gameobject que almacena el coste de la carta
    /// </summary>
    public GameObject cost;

    /// <summary>
    /// Texto que almacena el coste de la carta
    /// </summary>
    public Text cost_lbl;

    /// <summary>
    /// Tamaño de la carta
    /// </summary>
    public static RectTransform CARD_RECT_TRANSFORM { get; set; }

    /// <summary>
    /// Coloca un sprite en el gameobject de la carta
    /// </summary>
    public void setSprite(Sprite sprt)
    {
        ImageComponent = GetComponent<Image>();
        ImageComponent.sprite = sprt;
    }

    /// <summary>
    /// Instanciamos una carta en la posicion X
    /// </summary>
    /// <param name="position">Posicion donde vamos a instanciar la carta</param>
    public static GameObject instantiateCard(GameObject prefab, RectTransform position, Transform _parent, Deck _deck)
    {
        GameObject cardGameobject = Instantiate(prefab, position.position, Quaternion.identity, _parent);
        Card cardComponent = cardGameobject.AddComponent<Card>();
        cardComponent.deck = _deck;

        cardComponent.background = cardComponent.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;

        cardComponent.front = cardComponent.gameObject.transform.GetChild(1).gameObject;
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        cardComponent.cost = cardComponent.front.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        cardComponent.cost_lbl = cardComponent.cost.GetComponentInChildren<Text>();
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        selectCardAction(cardGameobject);

        return cardGameobject;
    }

    /// <summary>
    /// Determina la accion que va a hacer una carta
    /// </summary>
    /// <param name="cardGameobject"></param>
    private static void selectCardAction(GameObject cardGameobject)
    {
        int random = UnityEngine.Random.Range(0, 6);

        if (random < 2)
        {
            cardGameobject.AddComponent<AttackAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 0.82f, 0.82f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.ATTACK;
        }
        else
        {
            cardGameobject.AddComponent<MovementAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.69f, 0.99f, 0.69f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.MOVEMENT;
        }

        cardGameobject.GetComponent<Card>().setCost(cardGameobject.GetComponent<CardAction>().setRadio());


    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    void OnMouseDown()
    {
        if (
            !GameManager.GetInstance().deck.inCardAction  &&
            GameManager.GetInstance().state == States.INGAME && 
            GameManager.GetInstance().turn == TURN.PLAYER && 
            indexPosition != null 
            )
            deck.ShowInfo(this.gameObject);
    }

    /// <summary>
    /// Da la vuelta a la carta en cuestion
    /// </summary>
    public void FlipCard(bool flipped = true)
    {
        if (flipped)
        {
            front.SetActive(true);
            ((RectTransform)front.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;
            ((RectTransform)background.transform).sizeDelta = new Vector2(0, 0);
        }
        else
        {
            front.SetActive(false);
            ((RectTransform)background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;
            ((RectTransform)front.transform).sizeDelta = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// Seleccionamos un arte de una carta
    /// </summary>
    public void SetCardArt(Sprite spr)
    {
        front.GetComponent<Image>().sprite = spr;
    }

    /// <summary>
    /// Cambia el lbl del coste
    /// </summary>
    /// <param name="str"></param>
    public void setCost(string str)
    {
        cost_lbl.text = str;
    }
}
