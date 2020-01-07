//https://pressstart.vip/tutorials/2018/07/12/44/pan--zoom.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zoom sobre un objeto
/// </summary>
public class Zoom : MonoBehaviour
{

    /// <summary>
    /// Posicion donde se hace el primer touch
    /// </summary>
    private Vector3 touchStart;

    /// <summary>
    /// Minimo zoom
    /// </summary>
    public float zoomOutMin = 4;

    /// <summary>
    /// Zoom maximo
    /// </summary>
    public float zoomOutMax = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    /// <summary>
    /// Solamente si es en el ordenador
    /// </summary>
    /// <param name="increment"></param>
    private void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}