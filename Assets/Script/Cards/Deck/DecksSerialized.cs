using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lista de cartas serializadas
/// </summary>
public class DecksSerialized : DtoFirebase
{
    /// <summary>
    /// Almacena todos los mazos creados
    /// </summary>
    public List<List<int>> deckCollection;
    /// <summary>
    /// nombres de barajas
    /// </summary>
    public List<string> names;
    /// <summary>
    /// indica el mazo seleccionado para la partida
    /// </summary>
    public int deck_selected;
    /// <summary>
    /// fecha de salvado
    /// </summary>
    public string date;

}
