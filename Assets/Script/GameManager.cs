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
    INGAME = 1
}

/// <summary>
/// De quien es el turno en cada momento
/// </summary>
public enum TURN
{
    PLAYER = 0,
    IA = 1
}

/// <summary>
/// Maneja el flujo del juego - Es un Singleton
/// </summary>
public class GameManager : MonoBehaviour
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
    /// Instancia del GameManager
    /// </summary>
    private static GameManager instance;

    /// <summary>
    /// Estado actual del juego
    /// </summary>
    [HideInInspector]
    public States state;

    /// <summary>
    /// Quien tiene el turno actualmente
    /// </summary>
    public TURN? turn = null;

    /// <summary>
    /// Agentes que serán manejados por la IA
    /// </summary>
    [HideInInspector]
    public List<IAAgent> agents;

    /// <summary>
    /// Obtenemos la instancia actual del GameManager
    /// </summary>
    /// <returns>Instancia del GameManager</returns>
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            //GameManager.instance =  new GameManager();
        }

        return instance;
    }

    /// <summary>
    /// Funcion de start
    /// </summary>
    private IEnumerator Start()
    {
        state = States.LOADING;
        imageLoader = GetComponent<ImageLoader>();
        GameManager.instance = this;

        yield return null;
        StartCoroutine(starting());
    }

    /// <summary>
    /// Update del GameManager
    /// </summary>
    private IEnumerator TurnUpdate()
    {

        while (GameManager.GetInstance().state == States.INGAME)
        {
            if (turn == TURN.IA)
            {
                checkEndGame();

                if (deck.deckCanvasInfo.anchorToCards.Where(m => !m.state).Count() > 0)
                {
                    deck.DealCards();
                    yield return new WaitForSeconds(0.5f);
                }

                //Mostramos la animacion del player
                hud.turnlbl.showTurn();
                yield return new WaitForSeconds(hud.turnlbl.getTimeAnimation());

                StartCoroutine(IA.doAction(agents));
                yield return new WaitUntil(() => IA.actionDone);               

                turn = TURN.PLAYER;

                setCameraToPlayer();
                yield return new WaitForSeconds(1f);

                //Mostramos la animacion del player
                hud.turnlbl.showTurn();
                yield return new WaitForSeconds(hud.turnlbl.getTimeAnimation());
            }

            yield return null;
        }

    }

    /// <summary>
    /// Pasa de turno
    /// </summary>
    public void changeTurnToIA()
    {
        if (turn == TURN.PLAYER && !deck.inCardAction)
            turn = TURN.IA;
    }

    /// <summary>
    /// Determinamos si es el final del juego o no
    /// </summary>
    private void checkEndGame()
    {
        if (agents.Count == 0)
        {
            Debug.Log("Has matado a los enemigos");
            resetScene();
        }
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Inicializamos los objetos del juego
    /// </summary>
    /// <returns></returns>
    private IEnumerator starting()
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

        HUD.SetActive(true);

        deck.DealCards();
        yield return new WaitForSeconds(0.5f);

        hud.turnlbl.showTurn();
        yield return new WaitForSeconds(hud.turnlbl.getTimeAnimation());

        state = States.INGAME;
        turn = TURN.PLAYER;

        StartCoroutine(TurnUpdate());
    }

    /// <summary>
    /// Coloca la camara en el player
    /// </summary>
    public void setCameraToPlayer()
    {
        if (turn != TURN.IA)
            cameraFunctions.moveCameraTo(player.transform.position);
    }

}
