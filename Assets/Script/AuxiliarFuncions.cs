using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Biblioteca de funciones auxiliares
/// </summary>
public class AuxiliarFuncions : MonoBehaviour
{
    /// <summary>
    /// Mueve un objeto a una posicion local en un tiempo determinada
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public static IEnumerator MoveObjectToLocal(RectTransform obj, Vector3 goal, float time = 1f)
    {

        float t = 0;

        while (Vector3.Distance(obj.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.localPosition = Vector3.Lerp(obj.transform.localPosition, goal, t);
            yield return null;
        }

        obj.transform.localPosition = goal;

    }

    /// <summary>
    /// Mueve un objeto a una posicion en un tiempo determinada
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public static IEnumerator MoveObjectTo(Transform obj, Vector3 goal, float time = 1f)
    {

        float t = 0;

        while (Vector3.Distance(obj.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.position = Vector3.Lerp(obj.transform.position, goal, t);
            yield return null;
        }

        obj.transform.position = goal;

    }

    /// <summary>
    /// Transforma un rectransform a el size del world space
    /// </summary>
    /// <param name="obj">Rectrasnfom del que sacar la informacion</param>
    /// <returns></returns>
    public static Vector2 GetSizeFromRectTransform(RectTransform obj)
    {
        float x = obj.sizeDelta.x * obj.localScale.x;
        float y = obj.sizeDelta.y * obj.localScale.y;

        return new Vector2(x, y);
    }


    /// <summary>
    /// Muestra en la consola el string deseado
    /// </summary>
    /// <param name="str">String a mostrar</param>
    public static void DebugString(string str)
    {
        Debug.Log(str);
    }


    /// <summary>
    /// Efecto de FadeOut
    /// </summary>
    /// <param name="image">Imagen a la que se le aplicara el efecto</param>
    /// <returns></returns>
    public static IEnumerator FadeOut(Image image, GameObject toDescativate)
    {

        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.01f);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        toDescativate.SetActive(false);

    }


    /// <summary>
    /// Efecto de FadeOut
    /// </summary>
    /// <param name="image">Imagen a la que se le aplicara el efecto</param>
    /// <returns></returns>
    public static IEnumerator FadeIn(Image image, GameObject toDescativate)
    {
        toDescativate.SetActive(true);

        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.01f);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);


    }

}
