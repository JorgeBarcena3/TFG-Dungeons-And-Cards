using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using System.Reflection;

/// <summary>
/// Maneja las analiticas que se van a enviar a Firebase
/// </summary>
public class FirebaseAnalyticsManager : Singelton<FirebaseAnalyticsManager>
{
    /// <summary>
    /// Se inicializa el componente de analiticas
    /// </summary>
    public void init()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        Application.logMessageReceived += sendErrors;
    }

    /// <summary>
    /// Determinamos el user
    /// </summary>
    public void setUser(string name = "UnRegister")
    {
        FirebaseAnalytics.SetUserId(name);
    }

    /// <summary>
    /// Set the login Method
    /// </summary>
    /// <param name="Method"></param>
    public void setUserLoginMethod(string Method)
    {
        FirebaseAnalytics.SetUserProperty(
             FirebaseAnalytics.UserPropertySignUpMethod,
             Method);
    }

    /// <summary>
    /// Nos desuscribimos del evento
    /// </summary>
    private void OnDisable()
    {
        Application.logMessageReceived -= sendErrors;

    }

    /// <summary>
    /// Envia la informacion de la partida a firebase
    /// </summary>
    public void sendStatics(GameInfoDto _info)
    {
        List<Parameter> parametros = new List<Parameter>();

        PropertyInfo[] properties = typeof(GameInfoDto).GetProperties();
        foreach (PropertyInfo p in properties)
        {

            parametros.Add(new Parameter(p.Name, p.GetValue(_info).ToString().Substring(0, p.GetValue(_info).ToString().Length > 100 ? 99 : p.GetValue(_info).ToString().Length)));

        }

        FirebaseAnalytics.LogEvent(
            FirebaseAnalytics.EventLevelUp,
            parametros.ToArray()
         );
    }



    /// <summary>
    /// Enviamos el error en caso de que exista
    /// </summary>
    /// <param name="logString"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    void sendErrors(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            FirebaseAnalytics.LogEvent(
                    "ERROR",
                    "Name",
                    logString.Substring(0, 99)
            );
        }
    }

    /// <summary>
    /// Envia un informacion determinada afirebase
    /// </summary>
    public void sendSelectedinfo(string _event, string name, string value)
    {
        FirebaseAnalytics.LogEvent(
            _event,
           name,
           value);
    }


}
