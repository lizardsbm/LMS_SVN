using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;

public class FirebaseLogin : BaseBehaviour {
    Firebase.Auth.FirebaseAuth auth;

    string loginState;

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 100, 200, 200), loginState);
    }

    protected override void Start() {
        base.Start();
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void loginWithGooglePlay() {

        string googleTokenId = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Firebase.Auth.Credential credential =
            Firebase.Auth.GoogleAuthProvider.GetCredential(googleTokenId, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled) {
                loginState = "Cancel";
                return;
            }
            if (task.IsFaulted) {
                loginState = "Fail";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            loginState = string.Format("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
