using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


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
    [HideInInspector]
    public InfoCard info;

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
    /// Completa la HUD de la carta
    /// </summary>
    public HUDCard HUDCard;

    /// <summary>
    /// Tamaño de la carta
    /// </summary>
    public static RectTransform CARD_RECT_TRANSFORM { get; set; }

    /// <summary>
    /// Tamaño de la carta
    /// </summary>
    public static Vector2 ORIGINAL_SIZE { get; set; }

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
        cardComponent.HUDCard = cardGameobject.GetComponent<HUDCard>();

        cardComponent.background = cardComponent.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;

        cardComponent.front = cardComponent.gameObject.transform.GetChild(1).gameObject;
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
        int random = UnityEngine.Random.Range(0, 10);
        Card card = cardGameobject.GetComponent<Card>();


        if (random < 2)
        {

            cardGameobject.AddComponent<GivenManaAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.43f, 0.89f, 0.98f);
            card.type = ATTACKTYPE.SPECIAL;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            card.info = new InfoCard(
                ATTACKTYPE.SPECIAL,
                01,
                "Recuperación de Maná",
                "Cuando utilices esta carta se te recuperaran " + power + " puntos de maná, ue podras utilizar durante este turno",
                power,
                power);

        }
        else if (random < 6)
        {
            cardGameobject.AddComponent<AttackAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 0.82f, 0.82f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.DAMAGE;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.DAMAGE,
                01,
                "Ataque",
                "Cuando utilices esta carta podrás matar cualquier enemigo (sin moverte de la casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );

        }
        else
        {
            cardGameobject.AddComponent<MovementAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.69f, 0.99f, 0.69f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.MOVEMENT;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );
        }

        card.HUDCard.fillInfo(cardGameobject.GetComponent<Card>().info);

    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    void OnMouseDown()
    {
        if (
            !GameManager.GetInstance().deck.inCardAction &&
            GameManager.GetInstance().state == States.INGAME &&
            GameManager.GetInstance().turn == TURN.PLAYER &&
            indexPosition != null
            )
            deck.ClickOnCard(this.gameObject);
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


}
