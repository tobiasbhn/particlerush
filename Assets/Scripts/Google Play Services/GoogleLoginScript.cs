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
        if (isAuthenticated() || ConstantManager.debugMode) {
            if (withUI && !ConstantManager.debugMode)
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

        // Save login status
        if (withUI) {
            SaveDataManager.getValue.playGamesAutomaticAuth = true;
            SaveDataManager.Save();
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
        return (Social.localUser.authenticated && !ConstantManager.debugMode);
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
    public void CheckAchievements() {
        if (!isAuthenticated())
            return;

        PlayGamesPlatform.Instance.IncrementAchievement(
                    GPGSIds.achievement_level_up,
                    SaveDataManager.getValue.currentLevel >= 10 ? 10: 0,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_willkommen_in_partikel_rush,
                    100f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_hallo_welt,
                    SaveDataManager.getValue.statsTotalGamesPlayed > 0 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_nicht_jetzt,
                    SaveDataManager.getValue.statsTotleCountRevive > 0 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_shoppingqueen,
                    GetShopingqueenProgress(),
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_stressig,
                    SaveDataManager.getValue.maxReachedDifficulty == 1f ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_guter_score,
                    SaveDataManager.getValue.highscore >= 200000 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_ist_das_berhaupt_mglich,
                    SaveDataManager.getValue.highscore >= 500000 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_nochmal,
                    SaveDataManager.getValue.statsTotalGamesPlayed >= 10 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_9000,
                    SaveDataManager.getValue.statsTotalProjectilesFired >= 9000 ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_streber,
                    SaveDataManager.getValue.achieved_streber ? 100f : 0f,
                    (success) => { });
        Social.ReportProgress(
                    GPGSIds.achievement_ehre,
                    SaveDataManager.getValue.achieved_ehre ? 100f : 0f,
                    (success) => { });
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

    // PRIVATE
    private float GetShopingqueenProgress() {
        if (SaveDataManager.getValue.shrinkItemLVL > 0) {
            return 100f;
        }
        return 0f;
    }
}
