using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Baraja del jugador
/// </summary>
public class Deck : MonoBehaviour
{

    [Header("Prefabs")]
    /// <summary>
    /// Sprite que se asignará a la baraja
    /// </summary>
    public Sprite back;

    /// <summary>
    /// Prefab de la carta a instanciar
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// Prefab del anchor donde se atacharan las cartas
    /// </summary>
    public GameObject anchorPrefab;

    /// <summary>
    /// Background de la informacion de la carta
    /// </summary>
    public GameObject infoBackground;

    [Header("Player Settings")]
    /// <summary>
    /// Numero de cartas que tendrá en la mano
    /// </summary>
    public int cardsInHand = 5;

    /// <summary>
    /// Numero de cartas que habrá en la baraja
    /// </summary>
    public int cardsInDeck = 30;

    /// <summary>
    /// Arte de las cartas que vamos a meter en la baraja
    /// </summary>
    public List<Sprite> cardArt;

    /// <summary>
    /// Indice de las cartas para la creacion de la baraja
    /// </summary>
    private int cardArtIndex = 0;

    /// <summary>
    /// Informacion relativa a la baraja
    /// </summary>
    private DeckInfo deckInfo;

    /// <summary>
    /// Informacion relativa al canvas para la baraja
    /// </summary>
    private DeckCanvasInfo deckCanvasInfo;


    /// <summary>
    /// Se inicializa la baraja
    /// </summary>
    public void init()
    {
        deckInfo = this.gameObject.AddComponent<DeckInfo>();

        deckInfo.init(cardsInHand, cardsInDeck);

        deckCanvasInfo = new DeckCanvasInfo();

        deckCanvasInfo.getCanvasGameobject();

        SetSizeOfDeck();

        SetCardsAnchor();

        deckCanvasInfo.setDeckBack(cardPrefab.transform.GetChild(0).gameObject.GetComponent<Image>(), back, ref cardPrefab);

        InstantiateCards();

        SuffleDeck();
    }

    /// <summary>
    /// Crea y posiciona los anchors donde se van a parar las cartas
    /// </summary>
    private void SetCardsAnchor()
    {

        deckInfo.setCardsAnchor(ref deckCanvasInfo.anchorToCards, ref deckCanvasInfo.canvasGameObject, ref anchorPrefab);

    }

    /// <summary>
    /// Manda una carta al cementerio
    /// </summary>
    public void GoToCementery(GameObject card)
    {
        deckInfo.goToCementery(card, ref deckCanvasInfo.anchorToCards);
    }


    /// <summary>
    /// Determina el tamaño de la baraja según el tamaño del canvas
    /// </summary>
    private void SetSizeOfDeck()
    {

        RectTransform canvasComponent = deckCanvasInfo.canvasGameObject.GetComponent<RectTransform>();

        float width = canvasComponent.sizeDelta.x;
        float height = canvasComponent.sizeDelta.y;

        float cardWidth = width / (cardsInHand + 1);

        RectTransform transform = GetComponent<RectTransform>();

        float newX = cardWidth;
        float newY = transform.sizeDelta.y * cardWidth / transform.sizeDelta.x;

        transform.sizeDelta = new Vector2(newX, newY);

        Card.CARD_RECT_TRANSFORM = transform;

        ResizePrefabs(newX, newY);

    }

    /// <summary>
    /// Muestra la carta con toda su informacion
    /// </summary>
    /// <param name="gameObject"></param>
    public void ShowInfo(GameObject _gameObject = null)
    {
        if (deckInfo.infoCard == null)
        {
            deckInfo.infoCard = _gameObject.GetComponent<Card>();
            StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)deckInfo.infoCard.gameObject.transform, Vector3.zero));
            StartCoroutine(AuxiliarFuncions.SetSizeProgresive((RectTransform)deckInfo.infoCard.front.transform, Card.CARD_RECT_TRANSFORM.sizeDelta * 4)); //Tamaño visual
            StartCoroutine(AuxiliarFuncions.FadeIn(infoBackground.GetComponent<Image>(), infoBackground, 65, 1, true));

        }
        else if (_gameObject != null && deckInfo.infoCard == _gameObject.GetComponent<Card>())
        {
            Card card = deckInfo.infoCard;
            deckInfo.infoCard = null;
            deckInfo.goToCementery(_gameObject, ref deckCanvasInfo.anchorToCards);
            StartCoroutine(AuxiliarFuncions.SetSizeProgresive((RectTransform)card.front.transform, Card.CARD_RECT_TRANSFORM.sizeDelta));
            StartCoroutine(AuxiliarFuncions.FadeOut(infoBackground.GetComponent<Image>(), infoBackground, 1, true));

        }
        else
        {

            Card card = deckInfo.infoCard;
            deckInfo.infoCard = null;
            StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)card.gameObject.transform, deckCanvasInfo.anchorToCards[card.indexPosition.GetValueOrDefault()].transform.localPosition));
            StartCoroutine(AuxiliarFuncions.SetSizeProgresive((RectTransform)card.front.transform, Card.CARD_RECT_TRANSFORM.sizeDelta));
            StartCoroutine(AuxiliarFuncions.FadeOut(infoBackground.GetComponent<Image>(), infoBackground, 1, true));

        }
    }

    /// <summary>
    /// Crea un nuevo tamaño para el prefab
    /// </summary>
    /// <param name="newX">Nueva X</param>
    /// <param name="newY">Nueva Y</param>
    private void ResizePrefabs(float newX, float newY)
    {
        RectTransform prefabTransform = cardPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);

        prefabTransform = anchorPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);

    }


    /// <summary>
    /// Crea una instancia de todas las cartas
    /// </summary>
    private void InstantiateCards()
    {
        RectTransform rectTransformComponent = GetComponent<RectTransform>();

        for (int i = 0; i < cardsInDeck; ++i)
        {
            deckInfo.cardsGameObject.Add(Card.instantiateCard(cardPrefab, rectTransformComponent, deckCanvasInfo.canvasGameObject.transform, this));
            deckInfo.cardsGameObject.Last().GetComponent<Card>().SetCardArt(cardArt[cardArtIndex++]);

            if (cardArtIndex > cardArt.Count - 1)
                cardArtIndex = 0;

            deckInfo.activeCards.Add(deckInfo.cardsGameObject.Last());
        }
    }


    /// <summary>
    /// Se barajan todas las cartas de la baraja
    /// </summary>
    private void SuffleDeck()
    {
        List<GameObject> randomList = new List<GameObject>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (deckInfo.activeCards.Count > 0)
        {
            randomIndex = r.Next(0, deckInfo.activeCards.Count);
            randomList.Add(deckInfo.activeCards[randomIndex]);
            deckInfo.activeCards.RemoveAt(randomIndex);
        }

        deckInfo.activeCards = randomList;
    }

    /// <summary>
    /// Reparte las cartas al jugador
    /// </summary>
    /// <param name="count">Cantidad de cartas a repartir</param>
    /// <returns></returns>
    public void DealCards()
    {

        foreach (AnchorInfo position in deckCanvasInfo.anchorToCards)
        {
            if (!position.state)
            {
                position.state = true;

                GameObject card = deckInfo.activeCards.FirstOrDefault();

                if (!card)
                {
                    deckInfo.moveCementaryToActive(GetComponent<RectTransform>());
                    SuffleDeck();
                    card = deckInfo.activeCards.FirstOrDefault();

                }

                card.GetComponent<Card>().indexPosition = position.position;
                card.GetComponent<Card>().FlipCard();
                deckInfo.activeCards.RemoveAt(0);
                deckInfo.handCards.Add(card);

                StartCoroutine(AuxiliarFuncions.MoveObjectTo(card.GetComponent<RectTransform>(), position.transform.position));

            }
        }

        CheckIfCards();

    }

    /// <summary>
    /// Determina si hay cartas en la baraja activa
    /// </summary>
    private void CheckIfCards()
    {
        if (!deckInfo.activeCards.FirstOrDefault())
        {
            deckInfo.moveCementaryToActive(GetComponent<RectTransform>());
            SuffleDeck();
            deckInfo.FlipAllCards();

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
