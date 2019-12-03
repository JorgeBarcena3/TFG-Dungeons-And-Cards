using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja el flujo del juego - Es un Singleton
/// </summary>
public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Controlador de la baraja
    /// </summary>
    public Deck Deck;

    /// <summary>
    /// Generador de mundo
    /// </summary>
    public WorldGenerator worldGenerator;

    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    private static GameManager instance;

    /// <summary>
    /// Obtenemos la instancia actual del GameManager
    /// </summary>
    /// <returns>Instancia del GameManager</returns>
    public static GameManager getInstance()
    {
        if(!instance)
        {
            GameManager.instance = new GameManager();
        }

        return instance;
    }

    /// <summary>
    /// Funcion de start
    /// </summary>
    private void Start()
    {
        worldGenerator.init();
        Deck.init();
    }



}
