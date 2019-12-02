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
   public static IEnumerator moveObjectTo(GameObject obj, Vector3 goal, float time = 1f)
    {

        float distance = Vector3.Distance(obj.transform.position, goal);

        while (Vector3.Distance(obj.transform.position, goal) > 0.05f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, goal, (distance/time) * Time.deltaTime);
            yield return null;
        }
      
    }
}
