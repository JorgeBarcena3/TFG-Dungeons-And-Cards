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
    /// Multiplicador del size
    /// </summary>
    [Header("HUD")]
    public float SizeMultiplier = 3;

    /// <summary>
    /// Informacion relativa a la baraja
    /// </summary>
    [HideInInspector]
    public DeckInfo deckInfo;

    /// <summary>
    /// Informacion relativa al canvas para la baraja
    /// </summary>
    [HideInInspector]
    public DeckCanvasInfo deckCanvasInfo;

    /// <summary>
    /// Determina si estamos realizando una accion o no
    /// </summary>
    [HideInInspector]
    public bool inCardAction;

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

        infoBackground.transform.SetSiblingIndex(100);

        SwipeDetector.OnSwipe += onCardSwipe;

    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    private void onCardSwipe(SwipeData data)
    {
        if (GameManager.Instance.state == States.INGAME &&
           GameManager.Instance.turn == TURN.PLAYER)
        {
            if (
            data.Direction == SwipeDirection.Up &&
            !inCardAction &&
            deckInfo.infoCard
            )
            {
                ClickOnCard(deckInfo.infoCard.gameObject);
            }
            else if (
              data.Direction == SwipeDirection.Right &&
              !inCardAction &&
              deckInfo.infoCard)
            {
                matarCartas(deckInfo.infoCard.gameObject, false);
            }
            else if (deckInfo.infoCard != null)
            {
                ClickOnCard();
            }
        }

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

        float cardWidth = width / cardsInHand - 1;

        RectTransform transform = GetComponent<RectTransform>();

        float newX = cardWidth;
        float newY = transform.sizeDelta.y * cardWidth / transform.sizeDelta.x;

        //transform.sizeDelta = new Vector2(newX, newY);

        Card.CARD_RECT_TRANSFORM = transform;

        ResizePrefabs(newX, newY);

        Card.ORIGINAL_SIZE = transform.localScale;

    }

    /// <summary>
    /// Muestra la carta con toda su informacion
    /// </summary>
    /// <param name="gameObject"></param>
    public void ClickOnCard(GameObject _gameObject = null)
    {
        if (deckInfo.infoCard == null)
        {
            showCardInfo(_gameObject);

        }
        else if (_gameObject != null && deckInfo.infoCard == _gameObject.GetComponent<Card>())
        {

            if (_gameObject.GetComponent<CardAction>().checkAction(GameManager.Instance.player.gameObject))
            {
                matarCartas(_gameObject);
            }
            else
                hideCardInfo();

        }
        else
        {
            hideCardInfo();

        }
    }

    /// <summary>
    /// Muestra la informacion de una carta
    /// </summary>
    /// <param name="_gameObject"></param>
    private void showCardInfo(GameObject _gameObject)
    {
        deckInfo.infoCard = _gameObject.GetComponent<Card>();

        infoBackground.transform.SetSiblingIndex(100);
        _gameObject.transform.SetSiblingIndex(101);

        //TODO: Chapuza
        StopAllCoroutines();

        StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)deckInfo.infoCard.gameObject.transform, Vector3.zero));
        StartCoroutine(AuxiliarFuncions.SetLocalScaleProgresive(deckInfo.infoCard.gameObject.transform, Card.ORIGINAL_SIZE * SizeMultiplier));
        StartCoroutine(AuxiliarFuncions.FadeIn(infoBackground.GetComponent<Image>(), infoBackground, 65, 1, true));
    }

    /// <summary>
    /// Manda una carta al cementerio
    /// </summary>
    /// <param name="_gameObject"></param>
    private void matarCartas(GameObject _gameObject, bool doAction = true)
    {
        Card card = deckInfo.infoCard;
        deckInfo.infoCard = null;
        card.transform.SetSiblingIndex(card.indexPosition.GetValueOrDefault());

        //TODO: Chapuza
        StopAllCoroutines();

        if (doAction)
            _gameObject.GetComponent<CardAction>().DoAction(GameManager.Instance.player.gameObject);
        else // Si la descartamos
        {
            GameManager.Instance.player.playerInfo.addMana(1);
            GameManager.Instance.player.refreshPlayerData();

            if (GameManager.Instance.turnManager.isIATurn())
                GameManager.Instance.turn = TURN.IA;

        }

        deckInfo.goToCementery(_gameObject, ref deckCanvasInfo.anchorToCards);

        StartCoroutine(AuxiliarFuncions.SetLocalScaleProgresive(card.gameObject.transform, Card.ORIGINAL_SIZE));
        StartCoroutine(AuxiliarFuncions.FadeOut(infoBackground.GetComponent<Image>(), infoBackground, 1, true));
    }

    /// <summary>
    /// Oculta la informacion de la carta
    /// </summary>
    private void hideCardInfo()
    {
        Card card = deckInfo.infoCard;
        deckInfo.infoCard = null;

        reorderHandsCards();

        //TODO: Chapuza
        StopAllCoroutines();

        card.gameObject.GetComponent<BoxCollider2D>().size = Card.CARD_RECT_TRANSFORM.sizeDelta;
        card.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)card.gameObject.transform, deckCanvasInfo.anchorToCards[card.indexPosition.GetValueOrDefault()].transform.localPosition));
        StartCoroutine(AuxiliarFuncions.SetLocalScaleProgresive(card.gameObject.transform, Card.ORIGINAL_SIZE));
        StartCoroutine(AuxiliarFuncions.FadeOut(infoBackground.GetComponent<Image>(), infoBackground, 1, true));
    }

    /// <summary>
    /// Reordenamos las cartas de la mano
    /// </summary>
    private void reorderHandsCards()
    {
        int SiblingIndex = cardsInHand;

        var orderAnchors = deckCanvasInfo.anchorToCards.OrderByDescending(o => o.position).ToList();

        foreach (AnchorInfo info in orderAnchors)
        {
            GameObject cardobj = deckInfo.handCards.Where(m => m.GetComponent<Card>().indexPosition == info.position).FirstOrDefault();

            if (cardobj)
            {
                Card cardInfo = cardobj.GetComponent<Card>();
                cardInfo.transform.SetSiblingIndex(0);


            }
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

        Vector2 originalSize = new Vector2(prefabTransform.sizeDelta.x, prefabTransform.sizeDelta.y);
        Vector2 incremento = new Vector2(newX - originalSize.x, newY - originalSize.y);

        prefabTransform.sizeDelta = new Vector2(newX, newY);


        ((RectTransform)(cardPrefab.transform.GetChild(1).transform)).sizeDelta += incremento;

        prefabTransform = anchorPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);

        cardPrefab.GetComponent<BoxCollider2D>().size = new Vector2(newX, newY);

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
    public IEnumerator DealCards()
    {

        var delta = this.transform.GetSiblingIndex();

        foreach (AnchorInfo position in deckCanvasInfo.anchorToCards)
        {
            if (!position.state)
            {
                position.state = true;

                GameObject card = deckInfo.activeCards.FirstOrDefault();

                if (!card)
                {
                    StartCoroutine(CheckIfCards());

                    yield return new WaitUntil(() => deckInfo.cementeryCards.Count == 0);

                    yield return new WaitForSeconds(1);
                    card = deckInfo.activeCards.FirstOrDefault();

                }

                card.transform.SetSiblingIndex(position.position);
                card.GetComponent<Card>().indexPosition = position.position;
                card.GetComponent<Card>().FlipCard();

                deckInfo.activeCards.RemoveAt(0);
                deckInfo.handCards.Add(card);

                StartCoroutine(AuxiliarFuncions.MoveObjectTo(card.GetComponent<RectTransform>(), position.transform.position));

            }
        }

        StartCoroutine(CheckIfCards());

    }

    /// <summary>
    /// Determina si hay cartas en la baraja activa
    /// </summary>
    private IEnumerator CheckIfCards()
    {
        if (!deckInfo.activeCards.FirstOrDefault())
        {
            StartCoroutine(deckInfo.moveCementaryToActive(GetComponent<RectTransform>()));
        }

        yield return new WaitUntil(() => deckInfo.cementeryCards.Count == 0);

        SuffleDeck();

    }


}
