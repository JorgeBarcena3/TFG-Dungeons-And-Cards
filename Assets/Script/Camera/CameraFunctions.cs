using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Funciones de la camara
/// </summary>
public class CameraFunctions : MonoBehaviour
{

    /// <summary>
    /// Curva que va a seguir la animacion
    /// </summary>
    public AnimationCurve animation_curve;

    /// <summary>
    /// Determina la posicion de la camara
    /// </summary>
    /// <param name="position"></param>
    public void setPositionCamera(Vector2 position) 
    {
        GetComponent<Transform>().position = position;
    }

    /// <summary>
    /// Mueve la camara a una posicion determinada
    /// </summary>
    /// <param name="position"></param>
    public void moveCameraTo(Vector2 position, float time = 10f)
    {
        StartCoroutine( AuxiliarFuncions.MoveObjectTo(this.transform, position, time) );
    }
   /// <summary>
   /// Realiza un zoomin o un zoomaout
   /// </summary>
   /// <param name="zoom">en que size va a acabar la camara</param>
   /// <param name="time">tiempo en alcanzar el size</param>
   /// <returns></returns>
    public IEnumerator zoomInOut(float zoom, float time) 
    {
        Camera camera = GetComponent<Camera>();
        float size = camera.orthographicSize;
        if (size > zoom)
        {
            while (size > zoom)
            {
                size -= Time.deltaTime/time;
                camera.orthographicSize = size;
                yield return null;
            }

        }
        else if (size < zoom) 
        {
            while (size < zoom) 
            {
                size += Time.deltaTime / time;
                camera.orthographicSize = size;
                yield return null;
            }
           
        }
           
        camera.orthographicSize = zoom;
        yield return null;
    }
    /// <summary>
    /// Realiza un zoomout y despues un zoomin, esto se hace al inicio de la partida
    /// </summary>
    /// <param name="zoomOut">que size de zoom out alcanza</param>
    /// <param name="zoomIn">que size de zoom in alcanza</param>
    /// <param name="timeOut">tiempo que dedica en el zoom out</param>
    /// <param name="timeIn">tiempo que dedica en el zoom in></param>
    /// <returns></returns>
    public IEnumerator zoomOutAndIn(float zoomOut, float zoomIn, float timeOut, float timeIn)
    {
        Camera camera = GetComponent<Camera>();
        float t = 0;

        while (camera.orthographicSize < zoomOut)
        {
            t +=  Time.deltaTime;
            float s = t / timeOut;
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomOut, animation_curve.Evaluate(s));
            yield return null;
        }

        camera.orthographicSize = zoomOut;

        t = 0;

        while (camera.orthographicSize > zoomIn)
        {
            t += Time.deltaTime / timeIn;
            float s = t / timeIn;
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomIn, animation_curve.Evaluate(s));
            yield return null;
        }

       
        camera.orthographicSize = zoomIn;
        yield return null;
    }


}
