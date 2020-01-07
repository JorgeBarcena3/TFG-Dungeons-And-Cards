using System.Collections.Generic;
/// <summary>
/// Room que compone un mapa
/// </summary>
public class Room
{
    /// <summary>
    /// Lista de celdas que componen la habitacion
    /// </summary>
    public List<Cell> Cells { get; private set; }

    /// <summary>
    /// Vertices de cada habitacion
    /// </summary>
    public List<Cell> RoomsLimit { get; private set; }

    /// <summary>
    /// Habitaciones a las que esta conectado
    /// </summary>
    public List<Room> ConnectedRooms { get; private set; }

    /// <summary>
    /// Tamaño de la room
    /// </summary>
    public int RoomSize { get; private set; }

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public Room()
    {
    }

    /// <summary>
    /// Constructor con parametros
    /// </summary>
    /// <param name="_room">Lista de celdas de la habitacion</param>
    public Room(List<Cell> _room, Tablero _tablero)
    {
        this.Cells = _room;
        this.RoomSize = Cells.Count;
        this.ConnectedRooms = new List<Room>();

        this.RoomsLimit = new List<Cell>();

        foreach (Cell cell in this.Cells)
        {
            for (int x = cell.CellInfo.x - 1; x <= cell.CellInfo.x + 1; x++)
            {
                for (int y = cell.CellInfo.y - 1; y <= cell.CellInfo.y + 1; y++)
                {
                    if (
                            (x >= 0 && x < _tablero.worldCells.GetLength(0))
                         && (y >= 0 && y < _tablero.worldCells.GetLength(1))
                         && (x == cell.CellInfo.x || y == cell.CellInfo.y)
                    )
                    {
                        if (_tablero[x, y].Value == CELLSTYPE.ALIVE)
                        {
                            RoomsLimit.Add(cell);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Conecta dos habitaciones
    /// </summary>
    /// <param name="roomA">Habitacion a conectar A</param>
    /// <param name="roomB">Habitacion a conectar B</param>
    public static void ConnectRooms(Room roomA, Room roomB)
    {
        roomA.ConnectedRooms.Add(roomB);
        roomB.ConnectedRooms.Add(roomA);
    }

    /// <summary>
    /// Determinamos si la habitacion esta conectada con la anterior
    /// </summary>
    /// <param name="otherRoom">Habitacion con la que deberá estar conectado</param>
    /// <returns></returns>
    public bool IsConnected(Room otherRoom)
    {
        return ConnectedRooms.Contains(otherRoom);
    }


}