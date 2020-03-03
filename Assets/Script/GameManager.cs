using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Estado del juego
/// </summary>
public enum States
{
    LOADING = 0,
    INGAME = 1,
    INMENU = 2
}

/// <summary>
/// Maneja el flujo del juego - Es un Singleton
/// </summary>
public class GameManager : Singelton<GameManager>
{
    [Header("Cartas")]

    /// <summary>
    /// Controlador de la baraja
    /// </summary>
    public Deck deck;

    [Header("HUD")]

    /// <summary>
    /// Controlador del HUD
    /// </summary>
    public HUDController hud;

    /// <summary>
    /// HUD del juego
    /// </summary>
    public GameObject HUD;

    /// <summary>
    /// Componente que se encarga de cargar la imagen
    /// </summary>
    public ImageLoader imageLoader;

    [Header("Enemigos")]

    /// <summary>
    /// Controlador de la inteligencia Artificial
    /// </summary>
    public IAController IA;

    /// <summary>
    /// Generador de enemigos
    /// </summary>
    public EnemyGenerator enemyGenerator;

    [Header("Generador de mapa")]

    /// <summary>
    /// Generador de mundo
    /// </summary>
    public WorldGenerator worldGenerator;


    [Header("Jugador")]
    /// <summary>
    /// Jugador del juego
    /// </summary>
    public Player player;


    [Header("Camara")]
    /// <summary>
    /// Se encarga de las funcions de la camara
    /// </summary>
    public CameraFunctions cameraFunctions;

    /// <summary>
    /// Turno de quien
    /// </summary>
    public TURN turn { get { return turnManager.turn.GetValueOrDefault(); } set {
            turnManager.turn = value;
        } }

    /// <summary>
    /// Estado actual del juego
    /// </summary>
    [HideInInspector]
    public States state;

    /// <summary>
    /// Manejador del turno
    /// </summary>
    public TurnManager turnManager;

    /// <summary>
    /// Agentes que serán manejados por la IA
    /// </summary>
    [HideInInspector]
    public List<IAAgent> agents;


    /// <summary>
    /// Funcion de start
    /// </summary>
    private IEnumerator Start()
    {
        state = States.LOADING;
        imageLoader = GetComponent<ImageLoader>();

        yield return null;
        StartCoroutine(starting());
    }

    /// <summary>
    /// Determinamos si es el final del juego o no
    /// </summary>
    public void checkEndGame()
    {
        if (agents.Count == 0)
        {
            Debug.Log("Has matado a los enemigos");
            resetScene();
        }
    }

    /// <summary>
    /// Reseteamos la escena
    /// </summary>
    public void resetScene()
    {
        Initiate.Fade(SceneManager.GetActiveScene().name, Color.black, 2.0f);
    }

    /// <summary>
    /// Reseteamos la escena
    /// </summary>
    public void GoToMenu()
    {
        Initiate.Fade("Menu", Color.black, 2.0f);
    }

    /// <summary>
    /// Inicializamos los objetos del juego
    /// </summary>
    /// <returns></returns>
    private IEnumerator starting()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            state = States.INMENU;
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            worldGenerator.init();
            yield return null;
            deck.init();
            yield return null;
            player.init();
            yield return null;
            imageLoader.FadeOut();
            yield return null;
            setCameraToPlayer();
            yield return null;
            enemyGenerator.init(player);
            yield return null;
            agents = new List<IAAgent>();
            enemyGenerator.enemies.ForEach(m => agents.Add(m.GetComponent<IAAgent>()));
            yield return null;
            IA.init();

            while (Vector2.Distance(Camera.main.transform.position, player.transform.position) > 0.01f)
            {
                yield return null;
            }

            StartCoroutine(deck.DealCards());
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(deck.DealCards());
            yield return new WaitForSeconds(0.5f);

            state = States.INGAME;
            turn = TURN.PLAYER;

            hud.turnlbl.showTurn("JUGADOR");
            yield return new WaitForSeconds(hud.turnlbl.getTimeAnimation());

            StartCoroutine(turnManager.TurnUpdate());
        }
    }

        /// <summary>
        /// Coloca la camara en el player
        /// </summary>
        public void setCameraToPlayer(float time = 10)
        {
            if (turn != TURN.IA)
                cameraFunctions.moveCameraTo(player.transform.position, time);
        }

    
}
