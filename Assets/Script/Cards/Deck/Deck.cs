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
    /// Informacion relativa a la baraja
    /// </summary>
    private DeckInfo deckInfo;

    /// <summary>
    /// Informacion relativa al canvas para la baraja
    /// </summary>
    private DeckCanvasInfo deckCanvasInfo;

    /// <summary>
    /// Funcion del start
    /// </summary>
    void Start()
    {

        init();

    }

    /// <summary>
    /// Se inicializa la baraja
    /// </summary>
    private void init()
    {
        deckInfo = this.gameObject.AddComponent<DeckInfo>();

        deckInfo.init(cardsInHand, cardsInDeck);

        deckCanvasInfo = new DeckCanvasInfo();

        deckCanvasInfo.getCanvasGameobject();

        setSizeOfDeck();

        setCardsAnchor();

        deckCanvasInfo.setDeckBack(cardPrefab.GetComponent<Image>(), back, ref cardPrefab);

        instantiateCards();

        suffleDeck();
    }

    /// <summary>
    /// Crea y posiciona los anchors donde se van a parar las cartas
    /// </summary>
    private void setCardsAnchor()
    {

        deckInfo.setCardsAnchor(ref deckCanvasInfo.anchorToCards, ref deckCanvasInfo.CanvasGameObject, ref anchorPrefab);

    }

    /// <summary>
    /// Manda una carta al cementerio
    /// </summary>
    public void goToCementery(GameObject card)
    {
        deckInfo.goToCementery(card, ref deckCanvasInfo.anchorToCards);
    }


    /// <summary>
    /// Determina el tamaño de la baraja según el tamaño del canvas
    /// </summary>
    private void setSizeOfDeck()
    {

        RectTransform canvasComponent = deckCanvasInfo.CanvasGameObject.GetComponent<RectTransform>();

        float width = canvasComponent.sizeDelta.x;
        float height = canvasComponent.sizeDelta.y;

        float cardWidth = width / (cardsInHand + 1);

        RectTransform transform = GetComponent<RectTransform>();

        float newX = cardWidth;
        float newY = transform.sizeDelta.y * cardWidth / transform.sizeDelta.x;

        transform.sizeDelta = new Vector2(newX, newY);

        Card.CARD_RECT_TRANSFORM = transform;

        resizePrefabs(newX, newY);

    }

    /// <summary>
    /// Crea un nuevo tamaño para el prefab
    /// </summary>
    /// <param name="newX">Nueva X</param>
    /// <param name="newY">Nueva Y</param>
    private void resizePrefabs(float newX, float newY)
    {
        RectTransform prefabTransform = cardPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);

        prefabTransform = anchorPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);

    }


    /// <summary>
    /// Crea una instancia de todas las cartas
    /// </summary>
    private void instantiateCards()
    {
        RectTransform rectTransformComponent = GetComponent<RectTransform>();

        for (int i = 0; i < cardsInDeck; ++i)
        {
            deckInfo.cardsGameObject.Add(Card.instantiateCard(cardPrefab, rectTransformComponent, deckCanvasInfo.CanvasGameObject.transform, this));
            deckInfo.activeCards.Add(deckInfo.cardsGameObject.Last());
        }
    }


    /// <summary>
    /// Se barajan todas las cartas de la baraja
    /// </summary>
    private void suffleDeck()
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
    public void dealCards()
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
                    suffleDeck();
                    card = deckInfo.activeCards.FirstOrDefault();

                }

                card.GetComponent<Card>().indexPosition = position.position;
                deckInfo.activeCards.RemoveAt(0);
                deckInfo.handCards.Add(card);

                StartCoroutine(AuxiliarFuncions.moveObjectTo(card.GetComponent<RectTransform>(), position.transform.position));

            }
        }

        checkIfCards();

    }

    /// <summary>
    /// Determina si hay cartas en la baraja activa
    /// </summary>
    private void checkIfCards()
    {
        if (!deckInfo.activeCards.FirstOrDefault())
        {
            deckInfo.moveCementaryToActive(GetComponent<RectTransform>());
            suffleDeck();

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            dealCards();
    }
}
