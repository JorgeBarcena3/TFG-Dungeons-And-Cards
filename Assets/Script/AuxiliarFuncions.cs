using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Biblioteca de funciones auxiliares
/// </summary>
public class AuxiliarFuncions : MonoBehaviour
{
    /// <summary>
    /// Mueve un objeto a una posicion en un tiempo determinada
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
   public static IEnumerator moveObjectTo(RectTransform obj, Vector3 goal, float time = 1f)
    {

        float t = 0;

        while (Vector3.Distance(obj.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.localPosition = Vector3.Lerp(obj.transform.localPosition, goal, t);
            yield return null;
        }

        obj.transform.position = goal;        

    }

    //TODO MOVIMIENTO LOCAL
}
