using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Encargado de determinar las salas que contiene un tablero
/// </summary>
public class RoomsManager
{
    /// <summary>
    /// Lista de habitaciones
    /// </summary>
    private List<Room> rooms;

    /// <summary>
    /// Referencia al tablero donde esta la habitacion
    /// </summary>
    private Tablero board;

    /// <summary>
    /// Determina si los pasillos serán estrechos o no
    /// </summary>
    private bool narrowHallWays;

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public RoomsManager(Tablero _tablero, bool _pasillosEstrechos)
    {
        this.rooms = new List<Room>();
        this.board = _tablero;
        this.narrowHallWays = _pasillosEstrechos;
    }

    /// <summary>
    /// Determina las rooms que hay en un tablero
    /// </summary>
    /// <param name="_tablero">Tablero de donde se buscaran las habitaciones</param>
    public void CheckRooms(Tablero _tablero, bool unirHabitaciones = false)
    {

        SetCellsOutOfRoom(_tablero);

        for (int y = 0; y < _tablero.world_cell.GetLength(0); ++y)
        {
            for (int x = 0; x < _tablero.world_cell.GetLength(1); ++x)
            {
                if (_tablero[x, y].Value == CELLSTYPE.ALIVE && !_tablero[x, y].CellInfo.isInRoom)
                {
                    Room newRoom = new Room(GetRegionTiles(_tablero, _tablero[x, y]), _tablero);
                    Color RegionColor = new Color((float)Random.Range(0f, 1f), (float)Random.Range(0f, 1f), (float)Random.Range(0f, 1f));
                    foreach (Cell cell in newRoom.Cells)
                    {
                        cell.Color = RegionColor;
                        cell.CellInfo.isInRoom = true;

                    }
                    rooms.Add(newRoom);

                }
            }
        }

        if (unirHabitaciones)
            ConnectClosestRooms();
    }

    /// <summary>
    /// Elimina de las celdas de un tablero
    /// </summary>
    /// <param name="tablero">Tablero del cual eliminar las cosas</param>
    private void SetCellsOutOfRoom(Tablero _tablero)
    {

        this.rooms = new List<Room>();

        for (int y = 0; y < _tablero.world_cell.GetLength(0); ++y)
        {
            for (int x = 0; x < _tablero.world_cell.GetLength(1); ++x)
            {
                _tablero[x, y].CellInfo.isInRoom = false;
                _tablero[x, y].Color = Color.white;
            }
        }
    }

    /// <summary>
    /// A partir de un celda se obtiene una region
    /// </summary>
    /// <param name="tablero">Mapa donde se buscará la región</param>
    /// <param name="cell">Celda raiz</param>
    /// <returns></returns>
    private List<Cell> GetRegionTiles(Tablero _tablero, Cell _cell)
    {

        List<Cell> room = new List<Cell>();

        Queue<Cell> queue = new Queue<Cell>();
        _cell.CellInfo.isInRoom = true;
        queue.Enqueue(_cell);

        while (queue.Count > 0)
        {
            Cell cell = queue.Dequeue();
            room.Add(cell);
            CELLSTYPE cellType = cell.Value;

            int radioVecinos = _tablero.NeighboursRadius;

            for (int x = cell.CellInfo.x - radioVecinos; x <= cell.CellInfo.x + radioVecinos; x++)
            {
                for (int y = cell.CellInfo.y - radioVecinos; y <= cell.CellInfo.y + radioVecinos; y++)
                {

                    int NeighborX = x;
                    int NeighborY = y;

                    if (
                        (NeighborX >= 0 && NeighborX < _tablero.world_cell.GetLength(0))
                     && (NeighborY >= 0 && NeighborY < _tablero.world_cell.GetLength(1))
                     && (NeighborX == cell.CellInfo.x || NeighborY == cell.CellInfo.y)
                     )
                    {
                        if (_tablero[NeighborX, NeighborY].Value == cellType && !_tablero[NeighborX, NeighborY].CellInfo.isInRoom)
                        {
                            _tablero[NeighborX, NeighborY].CellInfo.isInRoom = true;
                            queue.Enqueue(_tablero[NeighborX, NeighborY]);

                        }

                    }

                }
            }
        }

        return room;
    }

    /// <summary>
    /// Conectamos las habitaciones mas cercanas
    /// </summary>
    /// <param name="allRooms">Lista de habitaciones</param>
    private void ConnectClosestRooms()
    {
        int mejorDistancia = 0;

        Cell mejorCeldaA = new Cell();
        Cell mejorCeldaB = new Cell();
        Room mejorHabitacionA = new Room();
        Room mejorHabitacionB = new Room();

        bool conexionEncontrada = false;

        Room habitacionPrincipal = rooms.OrderByDescending(i => i.RoomSize).Take(1).FirstOrDefault();

        if (habitacionPrincipal != null)
        {
            foreach (Room roomA in rooms)
            {
                conexionEncontrada = false;

                if (roomA != habitacionPrincipal)
                {

                    for (int celdaAIndex = 0; celdaAIndex < roomA.RoomsLimit.Count; celdaAIndex++)
                    {
                        for (int celdaBIndex = 0; celdaBIndex < habitacionPrincipal.RoomsLimit.Count; celdaBIndex++)
                        {
                            Cell celdaA = roomA.RoomsLimit[celdaAIndex];
                            Cell celdaB = habitacionPrincipal.RoomsLimit[celdaBIndex];
                            int distanciaManhattanEntreHabitaciones = (int)(Mathf.Pow(celdaA.CellInfo.x - celdaB.CellInfo.x, 2) + Mathf.Pow(celdaA.CellInfo.y - celdaB.CellInfo.y, 2));

                            if (distanciaManhattanEntreHabitaciones < mejorDistancia || !conexionEncontrada)
                            {
                                mejorDistancia = distanciaManhattanEntreHabitaciones;
                                conexionEncontrada = true;
                                mejorCeldaA = celdaA;
                                mejorCeldaB = celdaB;
                                mejorHabitacionA = roomA;
                                mejorHabitacionB = habitacionPrincipal;
                            }
                        }
                    }
                }


                if (conexionEncontrada)
                {
                    CreateHall(mejorHabitacionA, mejorHabitacionB, mejorCeldaA, mejorCeldaB);
                }
            }
        }
    }

    /// <summary>
    /// Crea un pasillo uniendo esas habitaciones
    /// </summary>
    /// <param name="roomA"></param>
    /// <param name="roomB"></param>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    void CreateHall(Room roomA, Room roomB, Cell startPos, Cell endPos)
    {
        Room.ConnectRooms(roomA, roomB);

        List<Cell> lineaEntreHabitaciones = GetLineBetweenRooms(startPos, endPos, roomA, roomB);
        lineaEntreHabitaciones.Insert(0, startPos);

        foreach (Cell c in lineaEntreHabitaciones)
        {
            CreateHall(c);
        }

    }

    /// <summary>
    /// Crea el pasillo en el tablero
    /// </summary>
    /// <param name="c"></param>
    /// <param name="conVecinos"></param>
    void CreateHall(Cell c)
    {

        int drawX = c.CellInfo.x;
        int drawY = c.CellInfo.y;

        board[drawX, drawY].Value = CELLSTYPE.ALIVE;
        board[drawX, drawY].Color = Color.red;

        if (!this.narrowHallWays)
        {
            List<Cell> vecinos = c.getNeighbours(board);

            foreach (Cell cell in vecinos)
            {
                cell.Value = CELLSTYPE.ALIVE;
                cell.Color = Color.red;
            }
        }

    }

    /// <summary>
    /// Mediante Pathfinding encuentra un camino de habitacion a habitacion
    /// </summary>
    /// <param name="desde"></param>
    /// <param name="hasta"></param>
    /// <param name="roomDesde"></param>
    /// <param name="roomHasta"></param>
    /// <returns></returns>
    List<Cell> GetLineBetweenRooms(Cell desde, Cell hasta, Room roomDesde, Room roomHasta)
    {

        PathFinding pf = new PathFinding();
        return pf.calcularRuta(board, desde, hasta);

    }

}