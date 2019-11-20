using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GoogleLoginScript : MonoBehaviour {

    // INSTANCE
    public static GoogleLoginScript instance;

    // VARS
    private bool initialized = false;


    // AWAKE
    void Awake() {
        instance = this;
    }

    // LOGIC
    public void CheckAutoSetup() {
        if (SaveDataManager.getValue.playGamesAutomaticAuth)
            Login(false);
    }
    private void initPlayGamesServices() {
        try {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            initialized = true;
        } catch (Exception e) {
            Debug.LogError(e);
            SetResult("Die Initialisierung von Google Play Games Services ist fehlgeschlagen.", "An Error occured during the initilization of Google Play Games Services.");
        }
    }

    public void Login(bool withUI) {
        // Check if already Signed in
        if (isAuthenticated()) {
            if (withUI)
                SetResult("Erfolgreich angemeldet.", "Logged in successfully.");
            return;
        }
        // Prevent Double-Action
        if (withUI) {
            UiObjectReferrer.instance.notificationLoginContinueBDE.interactable = false;
            UiObjectReferrer.instance.notificationLoginContinueBEN.interactable = false;
            UiObjectReferrer.instance.notificationLoginBackBDE.interactable = false;
            UiObjectReferrer.instance.notificationLoginBackBEN.interactable = false;
            UiObjectReferrer.instance.notificationLoginContinueTDE.color = new Color32(255, 255, 255, 90);
            UiObjectReferrer.instance.notificationLoginContinueTEN.color = new Color32(255, 255, 255, 90);
            UiObjectReferrer.instance.notificationLoginBackTDE.color = new Color32(255, 255, 255, 90);
            UiObjectReferrer.instance.notificationLoginBackTEN.color = new Color32(255, 255, 255, 90);
        }
        // Save login status
        if (withUI) {
            SaveDataManager.getValue.playGamesAutomaticAuth = true;
            SaveDataManager.Save();
        }
        // Init if not done jet
        if (!initialized)
            initPlayGamesServices();
        // Authenticate
        try {
            Social.localUser.Authenticate((bool success) => {
                if (success) {
                    if (withUI)
                        SetResult("Erfolgreich angemeldet.", "Logged in successfully.");
                } else {
                    if (withUI)
                        SetResult("Fehler beim anmelden.", "Error during login.");
                }
            });
        } catch (Exception e) {
            Debug.LogError(e);
            SetResult("Die Authentifizierung ist fehlgeschlagen.", "An Error occured during Authentification.");
        }
    }
    public void Logout() {
        // Check if already Signed out
        if (!isAuthenticated()) {
            SetResult("Erfolgreich abgemeldet.", "Successfully logged out.");
            return;
        }
        // Save login status
        SaveDataManager.getValue.playGamesAutomaticAuth = false;
        SaveDataManager.Save();
        // Init iff not done jet
        if (!initialized)
            initPlayGamesServices();
        // Singout
        try {
            PlayGamesPlatform.Instance.SignOut();
            SetResult("Erfolgreich abgemeldet.", "Successfully logged out.");
        } catch (Exception e) {
            Debug.LogError(e);
            SetResult("Fehler beim abmelden", "Error during logout.");
        }
    }

    public bool isAuthenticated() {
        return Social.localUser.authenticated;
    }
    public string getUsername() {
        return Social.localUser.userName;
    }
    public string getUserID() {
        return Social.localUser.id;
    }

    private void SetResult(string messageDE, string messageEN) {
        UiObjectReferrer.instance.notificationGoogleResultTextDE.text = messageDE;
        UiObjectReferrer.instance.notificationGoogleResultTextEN.text = messageEN;
        SceneManager.instance.callSceneGoogleResult();
    }


    // ACHIEVMENTS FUNCTIONS
    public void SetNewAchievment() {

    }
    public void ShowAchievments() {
        if (isAuthenticated())
            Social.ShowAchievementsUI();
        else
            SceneManager.instance.callSceneLoginNotification();
    }


    // LEADERBOARD FUNCIONS
    public void SetNewHighscore(int score, string leaderboardID) {
        if (!isAuthenticated())
            return;

        Social.ReportScore(score, leaderboardID, (bool success) => {
            if (success)
                Debug.Log("Reported new Highscore to Leaderboard.");
            else
                Debug.LogWarning("Cannot post new Highscore to leaderboard.");
        });
    }
    public void ShowHighscore() {
        if (isAuthenticated())
            Social.ShowLeaderboardUI();
        else
            SceneManager.instance.callSceneLoginNotification();
    }
    // public string[] GetHighscoreString() {
    //     string[] _ret = new string[2] { "", "" };
    //     if (isAuthenticated()) {
    //         PlayGamesPlatform.Instance.LoadScores(GPGSIds.leaderboard_highscore, LeaderboardStart.TopScores, 10, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, (data) => {
    //             if (data.Valid) {
    //                 List<string> userIds = new List<string>();
    //                 foreach (IScore score in data.Scores) {
    //                     userIds.Add(score.userID);
    //                 }
    //                 Social.LoadUsers(userIds.ToArray(), (users) => {
    //                     foreach (IScore score in data.Scores) {
    //                         foreach (IUserProfile user in users) {
    //                             if (score.userID == user.id) {
    //                                 _ret[0] += score.rank + " - \"" + user.userName + "\"\n";
    //                                 _ret[1] += score.value + "\n";
    //                             }
    //                         }
    //                     }
    //                 });
    //             }
    //         });
    //     }
    //     return _ret;
    // }
}
