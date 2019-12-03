using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tipo de ataque posibles
/// </summary>
public enum ATTACKTYPE
{
    movement = 0,
    attack = 1,
    defense = 2,
    special = 3
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
        return cardGameobject;
    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    void OnMouseDown()
    {
        deck.goToCementery(this.gameObject);
    }


}
