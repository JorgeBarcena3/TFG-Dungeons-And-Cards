using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Informacion sobre la baraja
/// </summary>
public class DeckInfo : MonoBehaviour
{
    /// <summary>
    /// Lista de Gameobjects que contienen las cartas
    /// </summary>
    public List<GameObject> cardsGameObject = new List<GameObject>();

    /// <summary>
    /// Cartas que pueden salir de la baraja
    /// </summary>
    public List<GameObject> activeCards = new List<GameObject>();

    /// <summary>
    /// Cartas que tiene en la mano actualmente
    /// </summary>
    public List<GameObject> handCards = new List<GameObject>();

    /// <summary>
    /// Cartas que han salido, van al cementerio
    /// </summary>
    public List<GameObject> cementeryCards = new List<GameObject>();

    /// <summary>
    /// Carta que esta siendo visitada por el jugador para obtener informacion
    /// </summary>
    public Card infoCard = null;

    /// <summary>
    /// Posicion del cementerio de cartas
    /// </summary>
    public Vector3 cementaryPosition;

    /// <summary>
    /// Numero de cartas que tendrá en la mano
    /// </summary>
    public int cardsInHand = 5;

    /// <summary>
    /// Numero de cartas que habrá en la baraja
    /// </summary>
    public int cardsInDeck = 30;

    /// <summary>
    /// Contructor con parametros de la infromacion de la cantidad de cartas
    /// </summary>
    /// <param name="_cardsInHands"></param>
    /// <param name="_cardsInDeck"></param>
    public void init(int _cardsInHands, int _cardsInDeck)
    {
        cardsInHand = _cardsInHands;
        cardsInDeck = _cardsInDeck;
    }

    /// <summary>
    /// Crea y posiciona los anchors donde se van a parar las cartas
    /// </summary>
    public void setCardsAnchor(ref List<AnchorInfo> anchorToCards, ref GameObject CanvasGameObject, ref GameObject anchorPrefab)
    {

        anchorToCards = new List<AnchorInfo>(cardsInHand);

        RectTransform canvasComponent = CanvasGameObject.GetComponent<RectTransform>();
        Vector2 sizeCanvas = AuxiliarFuncions.GetSizeFromRectTransform(canvasComponent);

        float width = sizeCanvas.x;
        float height = sizeCanvas.y;

        Vector2 CardSize = AuxiliarFuncions.GetSizeFromRectTransform(Card.CARD_RECT_TRANSFORM);

        float x = width / ((float)Math.Ceiling((double)cardsInHand/2));
        float y = CardSize.y * x / CardSize.x;


        for (int i = 0; i < cardsInHand; ++i)
        {
            //(-width / 2) + (x / 2) + (x * i) + ((i + 1) * xOffset)
            Vector3 position = new Vector3((-width / 2) + (x / 2) + i * x / 2, (-height / 2) + (height * 0.02f) + (y / 2), 0);
            GameObject anchor = Instantiate(anchorPrefab, position, Quaternion.identity, CanvasGameObject.transform);
            anchorToCards.Add(new AnchorInfo(false, anchor.transform, i));
        }

        cementaryPosition = new Vector3(canvasComponent.sizeDelta.x, canvasComponent.sizeDelta.y, 0);

    }

    /// <summary>
    /// Manda una carta al cementerio
    /// </summary>
    /// <param name="card"></param>
    public void goToCementery(GameObject card, ref List<AnchorInfo> anchorToCards)
    {
        if (!handCards.Contains(card))
            throw new Exception("La carta debe esta en la mano del jugado para poder ser enviada al cementerio");

        Card cardGameobject = card.GetComponent<Card>();


        foreach (AnchorInfo info in anchorToCards)
        {
            if (info.position == cardGameobject.indexPosition)
            {
                info.state = false;
                cardGameobject.indexPosition = null;

                handCards.Remove(card);

                cementeryCards.Add(card);

                StartCoroutine(AuxiliarFuncions.MoveObjectToLocal(card.GetComponent<RectTransform>(), cementaryPosition));
            }
        }

    }

    /// <summary>
    /// Mueve todas las cartas del cementerio a las cartas activas
    /// </summary>
    /// <param name="position"></param>
    public void moveCementaryToActive(RectTransform position)
    {
        activeCards.Clear();

        while (cementeryCards.Count >= 1)
        {
            activeCards.Add(cementeryCards.First());
            cementeryCards.RemoveAt(0);
            StartCoroutine(AuxiliarFuncions.MoveObjectTo(activeCards.Last().transform, position.position));
            //TODO: ESPERAR A QUE TODAS LAS CASRTAS HAYAN LLEGADO A SU DESTINO
        }

    }

    /// <summary>
    /// Se dan la vuelta a todas las cartas
    /// </summary>
    public void FlipAllCards()
    {
        foreach (GameObject cardObj in activeCards)
        {
            cardObj.GetComponent<Card>().FlipCard(false);
        }
    }
}