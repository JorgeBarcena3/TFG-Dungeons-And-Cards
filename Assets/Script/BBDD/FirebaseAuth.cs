using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;
using UnityEngine;

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
    /// Inicializamos la autorizacion
    /// </summary>
    public void init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .RequestServerAuthCode(false /* Don't force refresh */)
    .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();


    }

    /// <summary>
    /// Nos logueamos en la app
    /// </summary>
    public void LogIn()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            print("Autenticacion hecha con un resultado de: " + success);
            if (success)
            {
                authCode = PlayGamesPlatform.Instance.GetServerAuthCode();

                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);
                auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("SignInWithCredentialAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                        return;
                    }

                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})",
                        newUser.DisplayName, newUser.UserId);
                });
            }
        });

    }


}
