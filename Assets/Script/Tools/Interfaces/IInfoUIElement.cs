using UnityEngine;

/// <summary>
/// Obliga a que los obejetos que hereden de esta clase implementen un metodo para rellenar la informacion
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class IInfoUIElement<T> : MonoBehaviour
{
    /// <summary>
    ///  Rellena la informacion
    /// </summary>
    /// <param name="info"></param>
    public abstract void fillInfo(T info);


}

