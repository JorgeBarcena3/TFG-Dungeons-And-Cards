using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
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
    /// <param name="zoomInt">que size de zoom in alcanza</param>
    /// <param name="timeOut">tiempo que dedica en el zoom out</param>
    /// <param name="timeInt">tiempo que dedica en el zoom in></param>
    /// <returns></returns>
    public IEnumerator zoomOutAndIn(float zoomOut, float zoomInt, float timeOut, float timeInt)
    {
        Camera camera = GetComponent<Camera>();
        float size = camera.orthographicSize;
       
 
        while (size < zoomOut)
        {
            size += Time.deltaTime / timeOut;
            camera.orthographicSize = size;
            yield return null;
        }

        while (size > zoomInt)
        {
            size -= Time.deltaTime / timeInt;
            camera.orthographicSize = size;
            yield return null;
        }

       
        camera.orthographicSize = zoomInt;
        yield return null;
    }


}
