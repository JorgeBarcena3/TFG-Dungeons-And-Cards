using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{

    /// <summary>
    /// Mueve la camara a una posicion determinada
    /// </summary>
    /// <param name="position"></param>
    public void moveCameraTo(Vector2 position)
    {
        StartCoroutine( AuxiliarFuncions.MoveObjectTo(this.transform, position, 10f) );
    }

    
}
