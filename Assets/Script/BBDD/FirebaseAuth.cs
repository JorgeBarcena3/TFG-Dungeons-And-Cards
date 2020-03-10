using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

/// <summary>
/// Autenticacion de firebase
/// </summary>
public class FirebaseAuth : Singelton<FirebaseAuth>
{
    /// <summary>
    /// Variable que maneja la autorizacion
    /// </summary>
    public static string authCode;

    /// <summary>
    /// Token que maneja la autorizacion al servicio
    /// </summary>
    public static Firebase.Auth.FirebaseAuth auth;

    /// <summary>
    /// Current User
    /// </summary>
    Firebase.Auth.FirebaseUser User;

    /// <summary>
    /// Inicializamos la autorizacion
    /// </summary>
    public void init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false /* Don't force refresh */)
            .RequestIdToken()
        .Build();

        PlayGamesPlatform.DebugLogEnabled = false;

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

    }

    /// <summary>
    /// Nos logueamos en la app
    /// </summary>
    public void LogIn()
    {

        Social.localUser.Authenticate((bool success, string msg) =>
        {

            if (success)
            {
                authCode = PlayGamesPlatform.Instance.GetServerAuthCode();

                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);
                auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.LogError("SignInWithCredentialAsync was canceled. " + msg);
                        return;
                    }

                    User = task.Result;

                });
            }
        });

    }

    /// <summary>
    /// Salimos de los servicios de google play
    /// </summary>
    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        User = null;
    }


}
