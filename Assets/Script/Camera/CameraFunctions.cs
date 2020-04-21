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
    /// Realiza un zoom 
    /// </summary>
    /// <param name="zoom">contra mas pequeño, mas cerca</param>
    /// <returns></returns>
    public IEnumerator zoomIntOut(float zoom, float time) 
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
    public IEnumerator zoomOutAndInt(float zoomOut, float zoomInt, float timeOut, float timeInt)
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
