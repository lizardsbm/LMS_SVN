using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public partial class FirebaseManager : BaseSingleton<FirebaseManager> {

    public static string USER_TOKEN {
        get {

            Debug.Log("TokenLoad");

#if UNITY_EDITOR
            Debug.Log(SystemInfo.deviceUniqueIdentifier);
            return SystemInfo.deviceUniqueIdentifier;
#else
            Debug.Log(((PlayGamesLocalUser)Social.localUser).userName);
            return ((PlayGamesLocalUser)Social.localUser).userName;
#endif
        }
    }

    // 최초 실행시 가지고 있을 FirebaseApp 값
    private FirebaseApp firebaseApp;

    public override void init() {
        base.init();

        obj.name = "FirebaseManager";
        StartCoroutine(Initailze());
    }

    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    private bool firebaseInitialized;

    private IEnumerator Initailze() {

        InitializeFirebaseAndStart();

        while (!firebaseInitialized) {
            yield return null;
        }

        StartGame();
        initDatabase();
    }

    /// <summary>
    /// 파이어베이스 시작할때 필요한 세팅
    /// </summary>
    async void InitializeFirebaseAndStart() {

        Firebase.DependencyStatus dependencyStatus = Firebase.FirebaseApp.CheckDependencies();

        if (dependencyStatus != Firebase.DependencyStatus.Available) {
            await Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    firebaseInitialized = true;
                } else {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                    Application.Quit();
                }
            });
        } else {
            firebaseInitialized = true;
        }
    }

    // Actually start the game, once we've verified that everything
    // is working and we have the firebase prerequisites ready to go.
    void StartGame() {
        // FirebaseApp is responsible for starting up Crashlytics, when the core app is started.
        // To ensure that the core of FirebaseApp has started, grab the default instance which
        // is lazily initialized.

        Firebase.AppOptions ops = new Firebase.AppOptions();

        firebaseApp = FirebaseApp.Create(ops);

        // Setup database url when running in the editor
#if UNITY_EDITOR

        if (firebaseApp.Options.DatabaseUrl == null) {
            firebaseApp.SetEditorDatabaseUrl("https://api-project-53251866.firebaseio.com/");
        }
#endif
    }
}
