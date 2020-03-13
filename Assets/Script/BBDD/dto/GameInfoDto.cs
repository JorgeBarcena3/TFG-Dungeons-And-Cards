using System;
using System.Collections.Generic;
/// <summary>
/// Determina los posibles estados al finalizar la partida
/// </summary>
public enum CURRENTSTATE
{
    WINNER,
    LOSER,
    UNFINISHED
}

/// <summary>
/// Guarda la informacion de la partida
/// </summary>
public class GameInfoDto : DtoFirebase
{
    /// <summary>
    /// Duracion de la partida
    /// </summary>
    public double duration { get; set; }

    /// <summary>
    /// Estado al final de la partida
    /// </summary>
    public CURRENTSTATE estado { get; set; }

    /// <summary>
    /// Semilla de la partida utilizada
    /// </summary>
    public int seed { get; set; }

    /// <summary>
    /// Numero de enemigos en el mapa
    /// </summary>
    public int numeroDeEnemigos { get; set; }

    /// <summary>
    /// Numero de turnos
    /// </summary>
    public int numeroTurnos { get; set; }

    /// <summary>
    /// Tiempo para calcular la duracion
    /// </summary>
    public DateTime time;

    /// <summary>
    /// Cartas utilizadas en la partida
    /// </summary>
    public List<InfoCard> cartasUtilizadas;

    /// <summary>
    /// Constructor de la informacion
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="enemiesNumber"></param>
    public GameInfoDto(int seed, int enemiesNumber)
    {
        this.seed = seed;
        this.numeroDeEnemigos = enemiesNumber;
        this.cartasUtilizadas = new List<InfoCard>();
        this.numeroTurnos = 0;
        this.duration = 0;
        this.time = DateTime.Now;
    }

    /// <summary>
    /// Calcuamos la duracion de la partida
    /// </summary>
    public void calculateDuration()
    {
        duration = DateTime.Now.Subtract(time).TotalSeconds;
    }
}