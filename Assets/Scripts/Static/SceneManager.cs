using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager
{
    public static void startGame() {
        Debug.Log(LogTime.Time() + ": Scene Manager - Going to start Game...");
        callSceneMenu();
    }

    //SCENE FUNCTIONS
    public static void callSceneTutorial() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.tutorial)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Tutorial...");

        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.tutorial;
        SaveDataManager.Save();
        
    }

    public static void callSceneMenu() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.menu)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Menu...");

        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.menu;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupMenu();
        ParticleSceneSetup.instance.SetupMenu();
        UiSceneScript.instance.SetupMenu();
        ShakeScript.instance.SetupMenu();
    }

    public static void callSceneIngame() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.ingame)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Ingame...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.ingame;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupIngame();
        ParticleSceneSetup.instance.SetupIngame();
        UiSceneScript.instance.SetupIngame();
        ShakeScript.instance.SetupIngame();
    }

    public static void callSceneEndgame() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.endgame)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Endgame...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.endgame;
        SaveDataManager.Save();
        Time.timeScale = 0f;
    }

    public static void callScenePause() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.paused)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Pause...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.paused;
        SaveDataManager.Save();
        Time.timeScale = 0f;
        UiSceneScript.instance.SetupPause();
        PlayerSceneSetup.instance.SetupPause();
    }

    public static void callSceneResume() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.ingame)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Resume...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.ingame;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        UiSceneScript.instance.SetupIngame();
        PlayerSceneSetup.instance.SetupResume();
    }

    public static void callSceneSettings() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.settings)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Settings...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.settings;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupDisabled();
        ParticleSceneSetup.instance.SetupDisabled();
        UiSceneScript.instance.SetupSettings();
        ShakeScript.instance.SetupDisabled();
    }

    public static void callSceneAdsNotification() {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == GameStatus.notification)
            return;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading Ads Notification...");
        
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.notification;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupDisabled();
        ParticleSceneSetup.instance.SetupDisabled();
        UiSceneScript.instance.SetupNotificationAds();
        ShakeScript.instance.SetupDisabled();
    }
}
