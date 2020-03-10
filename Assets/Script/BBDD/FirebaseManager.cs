using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager  de la aplicacion de firebase
/// </summary>
public class FirebaseManager : Singelton<FirebaseManager>
{

    /// <summary>
    /// Se ha instanciado o no
    /// </summary>
    public static bool created = false;

    /// <summary>
    /// Referencia a la app
    /// </summary>
    public static Firebase.FirebaseApp app;

    /// <summary>
    /// Auth de firebase
    /// </summary>
    private FirebaseAuth _FirebaseAuth;

    /// <summary>
    /// Realtime database
    /// </summary>
    private FirebaseDatabaseManager _FirebaseDatabase;

    // Start is called before the first frame update
    async void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Firebase.DependencyStatus status = await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();

        // Firebase OK
        if (status == Firebase.DependencyStatus.Available)
        {
            FirebaseManager.app = Firebase.FirebaseApp.DefaultInstance;
            FirebaseManager.created = true;

            //Inicializamos los módulos
            AddModulesToGameObject();
        }
        // Otro estado de firebase
        else
        {
            Debug.LogError(status);
        }

    }

    /// <summary>
    /// Añadimos los modulos necesarios a los gameobjects
    /// </summary>
    private void AddModulesToGameObject()
    {
        try
        {
            _FirebaseAuth = this.gameObject.AddComponent<FirebaseAuth>();
            _FirebaseAuth.init();

            _FirebaseDatabase = this.gameObject.AddComponent<FirebaseDatabaseManager>();
            _FirebaseDatabase.init();

            print("Firebase modules created succesfully");
        }
        catch (Exception ex)
        {
            Debug.LogError("Firebase error: " + ex.ToString());
        }

    }

    /// <summary>
    /// Devuelve una coleccion
    /// </summary>
    public async void getCollection()
    {

        KeyExampleDto example = await _FirebaseDatabase.get<KeyExampleDto>("Test/test_01");
        Debug.Log("OLD: " + JsonConvert.SerializeObject(example));

        example.Attr1 = DateTime.Now.ToLongTimeString();
        example.Attr2 = DateTime.Now.DayOfWeek.ToString();
        Debug.Log("NEW: " + JsonConvert.SerializeObject(example));


        _FirebaseDatabase.addOrUpdate<KeyExampleDto>("Test", "test_01", example);
    }

    /// <summary>
    /// Nos logueamos el el servicio de google
    /// </summary>
    public void LogIn()
    {
        _FirebaseAuth.LogIn();
    }


}
