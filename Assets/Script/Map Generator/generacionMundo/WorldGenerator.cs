using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WorldGenerator : MonoBehaviour
{
    [Header("Datos del mapa")]
    /// <summary>
    /// Tamaño Y de la cueva
    /// </summary>
    public int tamanioX = 50;

    /// <summary>
    /// Tamaño X de la cueva
    /// </summary>
    public int tamanioY = 50;

    /// <summary>
    /// Tamaño del mapa
    /// </summary>
    public Vector2 size { get { return new Vector2(tamanioY, tamanioX);  } }

    /// <summary>
    /// Radio por el que se calcularan las casillas vecinas
    /// </summary>
    public int radioVecino = 1;

    /// <summary>
    /// Los pasillos seran estrechos o amplios
    /// </summary>
    public bool pasillosEstrechos = false;

    /// <summary>
    /// Determina si despues de cada iteracion se generan pasillos o no
    /// </summary>
    public bool generarPasillosPorIteracion = true;

    /// <summary>
    /// Determina si suavizamos o no el mundo
    /// </summary>
    public bool suavizarMundo = true;



    [Range(0, 1)]
    /// <summary>
    /// Probabilidades de que sea suelo inicial
    /// </summary>
    public float probabilidadesDeSerSueloRnd = 0.45f;

    [Header("Opciones de debug")]

    /// <summary>
    ///  Determina si se mostraran los colores del modo debug
    /// </summary>
    public bool mostrarColoresDebug = false;

    /// <summary>
    ///  Determina si se mostraran los colores de cada sala
    /// </summary>
    public bool colorDeSalasYPasillos = false;

    /// <summary>
    /// Determina si se activan o desactivan los controles
    /// </summary>
    public bool desactivarControles = true;


    [Header("Semilla del mundo")]

    /// <summary>
    /// Semilla de generacion aleatoria
    /// </summary>
    public int seed;

    /// <summary>
    /// Determinamos si cogemos una seed al azar
    /// </summary>
    public bool useRandomSeed;

    /// <summary>
    /// Ver progreso de creacion de mapa
    /// </summary>
    public bool verProcesoCreacionMapa = false;

    /// <summary>
    /// Numero de iteraciones que ha hecho en la primera generacion del mapa
    /// </summary>
    private int countIteracionesIniciales = 0;

    [Header("Regla de generacion de mundo (Cells Atomata - B/S)")]

    /// <summary>
    /// Regla para la generacion del mundo
    /// </summary>
    public string reglaDeGeneracion = "5678/35678";


    [Header("Iteraciones para la creacion del mundo")]

    /// <summary>
    /// Numero de iteraciones del mapa
    /// </summary>
    public int interaciones = 15;

    /// <summary>
    /// Se daran iteracciones iniciales o no
    /// </summary>
    public bool iteracionesIniciales = true;

    /// <summary>
    /// Representacion del mundo generado
    /// </summary>
    public Tablero board;

    /// <summary>
    /// Slider del mapa
    /// </summary>
    private Slider slider;

    /// <summary>
    /// Genera un mapa aleatorio
    /// </summary>
    void generateWorld()
    {
        //Creamos la matriz de las celdas
        this.board = new Tablero(tamanioY, tamanioX, radioVecino, reglaDeGeneracion, probabilidadesDeSerSueloRnd, pasillosEstrechos);

        //Creamos un tablero random
        this.board.CreateRandomWorld();

        if (iteracionesIniciales && !verProcesoCreacionMapa)
            RefreshBoard();

        DrawBoard();

    }

    /// <summary>
    /// Sprite con la que se representaran todo
    /// </summary>
    public GameObject[] prefabTiles;

    /// <summary>
    /// Sprites generados
    /// </summary>
    public List<GameObject> SpriteBoard { get; private set; }

    /// <summary>
    /// Función que pinta el tablero
    /// </summary>
    void DrawBoard()
    {

        if (SpriteBoard == null)
            SpriteBoard = new List<GameObject>();
        else
        {
            for (int i = 0; i < SpriteBoard.Count; i++)
            {
                Destroy(SpriteBoard[i]);
            }
            SpriteBoard = new List<GameObject>();

        }

        GameArtTheme.Instance.ChooseTheme();

        Vector3 size = prefabTiles[0].GetComponent<SpriteRenderer>().bounds.size;

        for (int x = 0; x < this.board.worldCells.GetLength(0); x++)
        {
            for (int y = 0; y < this.board.worldCells.GetLength(1); y++)
            {
               
                Vector3 position = x % 2 == 0 ? this.transform.position + new Vector3(y * size.x - 1, -(x * size.y * 23 / 40), -(x)) :
                                                this.transform.position + new Vector3(y * size.x - 1 + size.x / 2, -(x * size.y * 23 / 40), -(x));

                if (this.board.worldCells[x,y].Value == CELLSTYPE.DEAD)
                {
                    SpriteBoard.Add(Instantiate(prefabTiles[0], position, Quaternion.identity, this.transform));

                }
                else
                {
                    SpriteBoard.Add(Instantiate(prefabTiles[1], position, Quaternion.identity, this.transform));

                }

                SpriteBoard.Last().GetComponent<Tile>().CellInfo = this.board.worldCells[x, y].CellInfo;

            }
        }

        GameArtTheme.Instance.AddDecoration(SpriteBoard);
    }

    /// <summary>
    /// Metodo de inicializacion del mapa
    /// </summary>
    public void init()
    {
        if (!useRandomSeed)
            UnityEngine.Random.InitState(seed);

        if (verProcesoCreacionMapa)
            InvokeRepeating("doAStep", 0.0f, 0.5f);


        generateWorld();


        slider = GetComponent<Slider>();

        if (slider)
        {
            slider.activate(prefabTiles[0].GetComponent<SpriteRenderer>(), new Vector2(tamanioX, tamanioY));
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!desactivarControles)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RefreshBoard();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                board.SmoothOutTheMap();
                DrawBoard();

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                board.RoomManager.CheckRooms(board);
                DrawBoard();

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                board.RoomManager.CheckRooms(board, true);
                DrawBoard();
            }
        }
    }

    /// <summary>
    /// Hace una genracion del algoritmo
    /// </summary>
    public void DoAStep()
    {
        this.board.ComputeNeighbors();
        board.RoomManager.CheckRooms(board);
        if (++countIteracionesIniciales >= interaciones)
        {
            board.RoomManager.CheckRooms(board, this.generarPasillosPorIteracion);
            if (suavizarMundo)
                board.SmoothOutTheMap();

            CancelInvoke("doAStep");
        }
        DrawBoard();

    }

    /// <summary>
    /// Computamos los vecinos otra vez
    /// </summary>
    public void RefreshBoard()
    {
        for (int i = 0; i < interaciones; ++i)
        {
            this.board.ComputeNeighbors();
        }

        if (suavizarMundo)
            this.board.SmoothOutTheMap();

        if (generarPasillosPorIteracion)
            board.RoomManager.CheckRooms(board, true);
        else
            board.RoomManager.CheckRooms(board);

               
        DrawBoard();

    }

}
