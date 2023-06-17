using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleLogin : BaseBehaviour
{
    public GameObject bg;

    protected override void Start() {

        base.Start();

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestIdToken()
                .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public void signIn() {
        bg.SetActive(true);

        Social.localUser.Authenticate(success => {

            if (success) {

            } else {

            }
        });
    }
}
