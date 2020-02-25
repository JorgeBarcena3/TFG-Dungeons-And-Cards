﻿using System.Collections;
using System.Collections.Generic;
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
    public static IEnumerator SetSizeProgresive(RectTransform obj, Vector2 size, float time = 1f)
    {

        float t = 0;

        while (Vector3.Distance(obj.sizeDelta, size) > 0.01f)
        {
            t += Time.deltaTime / time;
            obj.sizeDelta = Vector3.Lerp(obj.sizeDelta, size, t);
            yield return null;
        }

        obj.sizeDelta = size;

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
