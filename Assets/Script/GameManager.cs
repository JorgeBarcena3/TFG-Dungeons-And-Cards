using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    /// <summary>
    /// Controlador de la baraja
    /// </summary>
    public Deck deck;

    /// <summary>
    /// Componente que se encarga de cargar la imagen
    /// </summary>
    public ImageLoader imageLoader;

    /// <summary>
    /// Generador de mundo
    /// </summary>
    public WorldGenerator worldGenerator;

    /// <summary>
    /// Jugador del juego
    /// </summary>
    public Player player;

    /// <summary>
    /// Se encarga de las funcions de la camara
    /// </summary>
    public CameraFunctions cameraFunctions;

    /// <summary>
    /// Controlador de la inteligencia Artificial
    /// </summary>
    public IAController IA;

    /// <summary>
    /// Generador de enemigos
    /// </summary>
    public EnemyGenerator enemyGenerator;

    /// <summary>
    /// HUD del juego
    /// </summary>
    public GameObject HUD;

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
    public TURN ? turn = null;

    /// <summary>
    /// Agentes que serán manejados por la IA
    /// </summary>
    private List<IAAgent> agents;

    /// <summary>
    /// Obtenemos la instancia actual del GameManager
    /// </summary>
    /// <returns>Instancia del GameManager</returns>
    public static GameManager GetInstance()
    {
        if (!instance)
        {
          //  GameManager.instance =  new GameManager();
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

        while(GameManager.GetInstance().state == States.INGAME)
        {
            if (turn == TURN.IA)
            {
                deck.DealCards();
                yield return new WaitForSeconds(1f);

                StartCoroutine( IA.doAction(agents) );

                yield return new WaitUntil(() => IA.actionDone );

                setCameraToPlayer();
                yield return new WaitForSeconds(1f);

                turn = TURN.PLAYER;

            }

            yield return null;
        }
       
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

        while (Vector2.Distance(Camera.main.transform.position, player.transform.position) > 0.01f)
        {
            yield return null;
        }

        HUD.SetActive(true);
        state = States.INGAME;
        turn = TURN.PLAYER;
        deck.DealCards();

        yield return new WaitForSeconds(1f);

        StartCoroutine(TurnUpdate());
    }

    /// <summary>
    /// Coloca la camara en el player
    /// </summary>
    public void setCameraToPlayer()
    {
        cameraFunctions.moveCameraTo(player.transform.position);
    }

}
