using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Clase que contiene los servicios php
/// </summary>
public class PHPServices : MonoBehaviour
{
    /// <summary>
    /// URL de conexion con el servidor
    /// </summary>
    public static string url = "http://localhost/phpmyadmin/dungeon/";

    /// <summary>
    /// Obtenemos todos los scores
    /// </summary>
    /// <returns></returns>
    public static IEnumerator GetAllScores()
    {
        string API_URL = url + "getAllScores.php";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("serverName=" + url + "&username=root$password=''"));

        UnityWebRequest www = UnityWebRequest.Post(API_URL, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            yield return results;
        }

    }

    /// <summary>
    /// Almacenamos los scores en la base de datos
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public IEnumerator SetScore(string name, string score)
    {
        string API_URL = url + "setScore.php";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("serverName=" + url + "&username=root$password=&score="+ score + "&name=" + name));

        UnityWebRequest www = UnityWebRequest.Post(API_URL, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            yield return www.error;
        }
        else
        {
            string results = www.downloadHandler.text;
            yield return results;
        }

    }


}
