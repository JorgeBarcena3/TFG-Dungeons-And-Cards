using System;


/// <summary>
/// Clase tablero del juego
/// </summary>
public class Tablero
{

    /// <summary>
    /// Representacion del mundo generado
    /// </summary>
    public Cell[,] worldCells;

    /// <summary>
    /// Ancho del tableor
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Altura del tablero
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Radio por el que se calcularan los vecinos
    /// </summary>
    public int NeighboursRadius { get; set; }

    /// <summary>
    /// Probabilidades de empezar vivo
    /// </summary>
    private float ChanceToLive { get; set; }

    /// <summary>
    /// Manager que controla la aplicacion de las reglas
    /// </summary>
    private RuleManager RuleManager { get; set; }

    /// <summary>
    /// Encargado de una vez generado el mapa, determinar las salas que lo contienen
    /// </summary>
    public RoomsManager RoomManager { get; private set; }

    /// <summary>
    /// Sobrecarga operador de []
    /// </summary>
    /// <param name="x">Valor de X</param>
    /// <param name="y">Valor de Y</param>
    /// <returns></returns>
    public Cell this[int x, int y]
    {
        get { return this.worldCells[x, y]; }
        private set { }
    }

    /// <summary>
    /// Constructor para crear el tablero
    /// </summary>
    /// <param name="tamanioX"></param>
    /// <param name="tamanioY"></param>
    public Tablero(int tamanioX, int tamanioY, int _radioVecino, string _reglaDeGeneracion, float probabilidades_de_ser_suelo_inicial, bool pasillosEstrechos)
    {
        this.RuleManager = new RuleManager(_reglaDeGeneracion, 'S', 'B');
        this.Width = tamanioX;
        this.Height = tamanioY;
        this.worldCells = new Cell[Width, Height];
        this.NeighboursRadius = _radioVecino;
        this.ChanceToLive = probabilidades_de_ser_suelo_inicial;
        this.RoomManager = new RoomsManager(this, pasillosEstrechos);
    }

    /// <summary>
    /// Llena un array con cells aleatorias
    /// </summary>
    public void CreateRandomWorld()
    {
        for (int x = 0; x < this.worldCells.GetLength(0); ++x)
        {
            for (int y = 0; y < this.worldCells.GetLength(1); ++y)
            {
                this.worldCells[x, y] = new Cell(x, y, ChanceToLive);
            }
        }

        SearchNeighbors();

    }

    /// <summary>
    /// Una vez completado el tablero buscamos y almacenamos los vecinos
    /// </summary>
    private void SearchNeighbors()
    {

        for (int x = 0; x < this.worldCells.GetLength(0); ++x)
        {
            for (int y = 0; y < this.worldCells.GetLength(1); ++y)
            {
                SetNeighbors(ref this.worldCells[x, y]);
            }
        }
    }

    /// <summary>
    /// Computa los vecinos de una celda
    /// </summary>
    /// <param name="myCell">La celda base</param>
    private void SetNeighbors(ref Cell myCell)
    {
        int radioVecinos = NeighboursRadius;

        myCell.CountNeighborsAlive = 0;

        //Cogemos todos los vecinos
        for (int y = radioVecinos; y >= -radioVecinos; --y)
        {
            for (int x = radioVecinos; x >= -radioVecinos; --x)
            {
                int NeighborX = myCell.CellInfo.x + x;
                int NeighborY = myCell.CellInfo.y + y;

                if (
                    (NeighborX >= 0 && NeighborX < worldCells.GetLength(0))
                 && (NeighborY >= 0 && NeighborY < worldCells.GetLength(1))
                 && (Math.Abs(x) + Math.Abs(y) != 0)
                 )
                {
                    if (this.worldCells[NeighborX, NeighborY].Value == CELLSTYPE.ALIVE)
                        ++myCell.CountNeighborsAlive;

                }
            }
        }

    }

    /// <summary>
    /// Actualizamos el estado de las celulas
    /// </summary>
    public void ComputeNeighbors()
    {

        Cell[,] next = new Cell[Width, Height];

        for (int x = 0; x < this.worldCells.GetLength(0); ++x)
        {
            for (int y = 0; y < this.worldCells.GetLength(1); ++y)
            {
                Cell cell = new Cell(this.worldCells[x, y]);
                next[x, y] = this.RuleManager.ApplyRules(cell);

            }
        }

        CopyNewBoard(next);
        SearchNeighbors();
        this.RoomManager.CheckRooms(this);

    }

    /// <summary>
    /// Copia un nuevo array a la variable existente
    /// </summary>
    /// <param name="next">New Array</param>
    private void CopyNewBoard(Cell[,] next)
    {

        for (int x = 0; x < this.worldCells.GetLength(0); ++x)
        {
            for (int y = 0; y < this.worldCells.GetLength(1); ++y)
            {
                this.worldCells[x, y] = new Cell(next[x, y]);

            }
        }
    }

    /// <summary>
    /// Suaviza el mapa algunos puntos en el centro
    /// </summary>
    public void SmoothOutTheMap()
    {
        SearchNeighbors();

        Cell[,] next = new Cell[Width, Height];

        for (int y = 0; y < this.worldCells.GetLength(0); ++y)
        {
            for (int x = 0; x < this.worldCells.GetLength(1); ++x)
            {
                Cell cell = new Cell(this.worldCells[x, y]);
                if (this.worldCells[x, y].CountNeighborsAlive == 8 && this.worldCells[x, y].Value == CELLSTYPE.DEAD)
                    cell = new Cell(cell.CellInfo.x, cell.CellInfo.y, CELLSTYPE.ALIVE);
                else if (this.worldCells[x, y].CountNeighborsAlive == 0 && this.worldCells[x, y].Value == CELLSTYPE.ALIVE)
                    cell = new Cell(cell.CellInfo.x, cell.CellInfo.y, CELLSTYPE.DEAD);


                next[x, y] = cell;

            }
        }

        CopyNewBoard(next);
        SearchNeighbors();

    }
}