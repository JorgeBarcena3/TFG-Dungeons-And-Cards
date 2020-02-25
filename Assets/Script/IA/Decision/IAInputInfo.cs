using System.Collections.Generic;
/// <summary>
/// Informacion que toma la IA para tomar una decision
/// </summary>
public class IAInputInfo
{
    /// <summary>
    /// Distnacia hasta el jugador
    /// </summary>
    public List<Tile> waypointsToPlayer { get; set; }

    /// <summary>
    /// Porcentaje de vida que le queda
    /// </summary>
    public float porcentajeVida { get; set; }

    /// <summary>
    /// Distancia del player al objetivo
    /// </summary>
    public int distanciaPlayerObjetivo { get; set; }

    /// <summary>
    /// Constructor obligatorio
    /// </summary>
    public IAInputInfo(List<Tile> _dp, float _pv, int _dpo) 
    {
        waypointsToPlayer = _dp;
        porcentajeVida = _pv;
        distanciaPlayerObjetivo = _dpo;
    }

}