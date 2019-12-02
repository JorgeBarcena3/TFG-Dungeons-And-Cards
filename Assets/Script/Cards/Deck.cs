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

    /// <summary>
    /// Sprite que se asignará a la baraja
    /// </summary>
    public Sprite back;

    /// <summary>
    /// Prefab de la carta a instanciar
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// Lista de Gameobjects que contienen las cartas
    /// </summary>
    private List<GameObject> cardsGameObject = new List<GameObject>();

    /// <summary>
    /// Componente del UI que almacena las imagenes
    /// </summary>
    private Image ImageComponent;

    /// <summary>
    /// Obtiene una referencia al canvas
    /// </summary>
    private GameObject CanvasGameObject;

    /// <summary>
    /// Cartas que pueden salir de la baraja
    /// </summary>
    private List<GameObject> activeCards = new List<GameObject>();

    /// <summary>
    /// Cartas que tiene en la mano actualmente
    /// </summary>
    private List<GameObject> handCards = new List<GameObject>();

    /// <summary>
    /// Cartas que han salido, van al cementerio
    /// </summary>
    private List<GameObject> cementeryCards = new List<GameObject>();

    /// <summary>
    /// Funcion del start
    /// </summary>
    void Start()
    {

        getCanvasGameobject();

        setSizeOfDeck();

        setDeckBack();

        instantiateCards();

        suffleDeck();

    }


    /// <summary>
    /// Determina el tamaño de la baraja según el tamaño del canvas
    /// </summary>
    private void setSizeOfDeck()
    {

        RectTransform canvasComponent = CanvasGameObject.GetComponent<RectTransform>();

        float width = canvasComponent.sizeDelta.x;
        float height = canvasComponent.sizeDelta.y;

        float cardWidth = width / 6;

        RectTransform transform = GetComponent<RectTransform>();

        float newX = cardWidth;
        float newY = transform.sizeDelta.y * cardWidth / transform.sizeDelta.x;

        transform.sizeDelta = new Vector2(newX, newY);

        resizePrefab(newX, newY);

    }

    /// <summary>
    /// Crea un nuevo tamaño para el prefab
    /// </summary>
    /// <param name="newX">Nueva X</param>
    /// <param name="newY">Nueva Y</param>
    private void resizePrefab(float newX, float newY)
    {
        RectTransform prefabTransform = cardPrefab.GetComponent<RectTransform>();
        prefabTransform.sizeDelta = new Vector2(newX, newY);
    }

    /// <summary>
    /// Obtiene el gameobject donde pinta el canvas
    /// </summary>
    /// <param name="tag">Parametro opcional que contiene el tag a buscar</param>
    private void getCanvasGameobject(string tag = "GameCanvas")
    {
        CanvasGameObject = GameObject.FindGameObjectWithTag(tag);
    }

    /// <summary>
    /// Crea una instancia de todas las cartas
    /// </summary>
    private void instantiateCards()
    {
        RectTransform rectTransformComponent = GetComponent<RectTransform>();

        for (int i = 0; i < 10; ++i)
        {
            cardsGameObject.Add(Card.instantiateCard(cardPrefab, rectTransformComponent, CanvasGameObject.transform));
            activeCards.Add(cardsGameObject.Last());
        }
    }

    /// <summary>
    /// Selecciona el sprite de la baraja
    /// </summary>
    private void setDeckBack()
    {
        ImageComponent = GetComponent<Image>();
        ImageComponent.sprite = back;
        cardPrefab.GetComponent<Image>().sprite = back;
    }

    /// <summary>
    /// Se barajan todas las cartas de la baraja
    /// </summary>
    private void suffleDeck()
    {
        List<GameObject> randomList = new List<GameObject>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (activeCards.Count > 0)
        {
            randomIndex = r.Next(0, activeCards.Count);
            randomList.Add(activeCards[randomIndex]);
            activeCards.RemoveAt(randomIndex);
        }

        activeCards = randomList;
    }

    /// <summary>
    /// Reparte las cartas al jugador
    /// </summary>
    /// <param name="count">Cantidad de cartas a repartir</param>
    /// <returns></returns>
    public void dealCards(int count)
    {
        RectTransform canvasComponent = CanvasGameObject.GetComponent<RectTransform>();

        float width = canvasComponent.sizeDelta.x * canvasComponent.localScale.x;
        float height = canvasComponent.sizeDelta.y * canvasComponent.localScale.y;

        RectTransform transform = (RectTransform)cardPrefab.transform;

        float cardWidth = transform.sizeDelta.x * transform.localScale.x;
        float cardHeight = transform.sizeDelta.y * transform.localScale.y;

        float paddingX = cardWidth / 6;
        // p C p C p C p C p C p
        for (int i = 0; i < count; ++i)
        {
            GameObject card = activeCards.First();
            activeCards.RemoveAt(0);
            handCards.Add(card);

            Vector3 goal = new Vector3((- width / 2) + paddingX + i * cardWidth, -(height / 2) + cardHeight * 1.2f, 0);

            StartCoroutine(AuxiliarFuncions.moveObjectTo(card.GetComponent<RectTransform>(), goal));
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            dealCards(5);
    }
}
