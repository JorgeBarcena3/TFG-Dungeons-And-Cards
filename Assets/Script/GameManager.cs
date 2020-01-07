using System.Collections;
using System.Collections.Generic;
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
    public CameraFunctions camera;

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
    private States state;

    /// <summary>
    /// Obtenemos la instancia actual del GameManager
    /// </summary>
    /// <returns>Instancia del GameManager</returns>
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            GameManager.instance = new GameManager();
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

        yield return null;
        StartCoroutine(starting());
    }

    /// <summary>
    /// Update del GameManager
    /// </summary>
    private void Update()
    {

        if (GameManager.GetInstance().state == States.INGAME && Input.GetKeyDown(KeyCode.D) || (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2))
        {
            deck.DealCards();
        }
    }

    /// <summary>
    /// Inicializamos los objetos del juego
    /// </summary>
    /// <returns></returns>
    private IEnumerator starting()
    {
        GameManager.instance = this;
        yield return null;
        worldGenerator.init();
        yield return null;
        deck.init();
        yield return null;
        player.init();
        yield return null;
        imageLoader.FadeOut();
        yield return null;

        setCameraToPlayer();

        while (Vector2.Distance(Camera.main.transform.position, player.transform.position) > 0.01f)
        {
            yield return null;
        }

        HUD.SetActive(true);
        state = States.INGAME;
    }

    /// <summary>
    /// Coloca la camara en el player
    /// </summary>
    public void setCameraToPlayer()
    {
        camera.moveCameraTo(player.transform.position);
    }
}
