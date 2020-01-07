using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cargador de imagenes
/// </summary>
public class ImageLoader : MonoBehaviour
{
    /// <summary>
    /// Imagen de loading del juego
    /// </summary>
    public Image loadingImage;

    /// <summary>
    /// Canvas del HUD
    /// </summary>
    public GameObject HUDCanvas;

    /// <summary>
    /// Fadeout de la imagen
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(AuxiliarFuncions.FadeOut(loadingImage, HUDCanvas, 5));

    }
}