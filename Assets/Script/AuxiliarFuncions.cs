using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Biblioteca de funciones auxiliares
/// </summary>
public class AuxiliarFuncions : MonoBehaviour
{

    /// <summary>
    /// Cambia el tamaño al tamaño deseado
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="size"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static IEnumerator SetLocalScaleProgresive(Transform obj, Vector2 size, float time = 1f)
    {

        float t = 0;


        while (Vector3.Distance(obj.localScale, size) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.localScale = Vector3.Lerp(obj.localScale, size, t);
            yield return null;
        }

        obj.localScale = size;

    }

    /// <summary>
    /// Mueve un objeto a una posicion local en un tiempo determinada
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public static IEnumerator MoveObjectToLocal(RectTransform obj, Vector3 goal, float time = 1f)
    {

        float t = 0;

        while (Vector2.Distance(obj.localPosition, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.localPosition = Vector3.Lerp(obj.localPosition, goal, t);
            yield return null;
        }

        obj.localPosition = goal;

    }

    /// <summary>
    /// Nos movemos siguiendo unos waypoints
    /// </summary>
    public static IEnumerator moveWithWaypoints(Transform obj, List<Tile> waypoints, float time = 0.1f)
    {
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, waypoints.Last().transform.position.z);
        foreach (Tile tile in waypoints)
        {

            Vector3 newPosition = tile.transform.position;
            obj.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, -(GameManager.Instance.worldGenerator.tamanioY + 1));

            GameManager.Instance.StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(obj.transform, newPosition, 1f));

            yield return new WaitUntil(() =>
                obj.transform.position.x == newPosition.x &&
                obj.transform.position.y == newPosition.y
                );


            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, tile.transform.localPosition.z) + (GameManager.Instance.player != null ? GameManager.Instance.player.zOffset : Vector3.zero ) ;
            

        }

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
            obj.position = Vector3.Lerp(obj.position, goal, t);
            yield return null;
        }

        obj.position = goal;

    }

    /// <summary>
    /// Mueve un objeto a una posicion en un tiempo determinada
    /// </summary>
    /// <param name="obj">Objeto a desplazar</param>
    /// <param name="goal">Posicion meta</param>
    /// <param name="time">Tiempo de desplazamiento</param>
    public static IEnumerator MoveObjectTo2D(Transform obj, Vector2 goal, float time = 1f)
    {
        float t = 0;

        while (Vector2.Distance(obj.transform.position, goal) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.position = new Vector3(Mathf.Lerp(obj.position.x, goal.x, t), Mathf.Lerp(obj.position.y, goal.y, t), obj.position.z);
            yield return null;
        }

        obj.position = new Vector3(goal.x, goal.y, obj.position.z);

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
    public static IEnumerator FadeOut(Image image, GameObject toDescativate, float time = 1, bool setTransition = false)
    {
        float t = 0;
        if (setTransition)
            InfoBackground.IS_TRANSITION = true;

        while (image.color.a > 0)
        {
            t += Time.deltaTime / time;
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), t);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        toDescativate.SetActive(false);
        if (setTransition)
            InfoBackground.IS_TRANSITION = false;

    }


    /// <summary>
    /// Efecto de FadeOut
    /// </summary>
    /// <param name="image">Imagen a la que se le aplicara el efecto</param>
    /// <returns></returns>
    public static IEnumerator FadeIn(Image image, GameObject toDescativate, float percentage = 100, float time = 1, bool setTransition = false)
    {
        toDescativate.SetActive(true);

        if (setTransition)
            InfoBackground.IS_TRANSITION = true;

        float t = 0;
        float percent = percentage / 100;

        while (image.color.a < percent)
        {
            t += Time.deltaTime / time;
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, percent), t);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, percent);

        if (setTransition)
            InfoBackground.IS_TRANSITION = false;


    }
    
}
