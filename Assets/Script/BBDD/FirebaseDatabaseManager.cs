using Firebase.Unity.Editor;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Threading.Tasks;
using System;

/// <summary>
/// Maneja la realtime database
/// </summary>
public class FirebaseDatabaseManager : Singelton<FirebaseDatabaseManager>
{

    /// <summary>
    /// Referencia a la RealTimeDatabase
    /// </summary>
    static Firebase.Database.DatabaseReference reference;

    /// <summary>
    /// Inicializamos la base de datos
    /// </summary>
    public void init()
    {
        FirebaseManager.app.SetEditorDatabaseUrl("https://dungeonsandcards-8330c.firebaseio.com");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    /// <summary>
    /// Devuelve una coleccion determinada
    /// </summary>
    /// <returns></returns>
    public async Task<T> get<T> (string value) where T : DtoFirebase, new()
    {
        DataSnapshot data = await FirebaseDatabase.DefaultInstance
         .GetReference(value)
         .GetValueAsync();

        T result = dictionaryToType<T>(DataSnapshotToDictionary(data));

        return result;

    }

    /// <summary>
    /// Hace un Update de un elemento ya existente
    /// </summary>
    /// <returns></returns>
    public async void addOrUpdate<T>(string collection, string id, T obj) where T : DtoFirebase, new()
    {
        string json = JsonConvert.SerializeObject(obj);

        await reference.Child(collection).Child(id).SetRawJsonValueAsync(json);

    }
    
    /// <summary>
    /// Dictionary to type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dict"></param>
    /// <returns></returns>
    private T dictionaryToType<T>(Dictionary<string, string> dict) where T : DtoFirebase, new()
    {
        Type type = typeof(T);
        T result = (T)Activator.CreateInstance(type);

        foreach (var item in dict)
        {
            type.GetProperty(item.Key).SetValue(result, item.Value, null);
        }

        return result;
    }

    /// <summary>
    /// Snapshot to dictionary
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private Dictionary<string, string> DataSnapshotToDictionary(DataSnapshot data)
    {
        Dictionary<string, string> attrb = new Dictionary<string, string>();

        foreach (var item in data.Children)
        {
            attrb.Add(item.Key, item.GetValue(true).ToString());
        }

        return attrb;
    }
}
