using UnityEngine;

/// <summary>
/// Informacion sobre el anchor
/// </summary>
public class AnchorInfo
{
    /// <summary>
    /// Posicion del anchor
    /// </summary>
    public Transform transform;

    /// <summary>
    /// Estado de la posicion
    /// </summary>
    public bool state;

    /// <summary>
    /// Posicion de la mano del jugador
    /// </summary>
    public int position;

    /// <summary>
    /// Constructor del objeto
    /// </summary>
    public AnchorInfo(bool _state, Transform _transform, int _position)
    {
        state = _state;
        transform = _transform;

        position = _position;
    }
}