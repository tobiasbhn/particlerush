﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager {
    public static void startGame() {
        Debug.Log(LogTime.Time() + ": Scene Manager - Going to start Game...");
        callSceneMenu();
    }

    //SCENE FUNCTIONS
    public static void callSceneTutorial() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.tutorial))
            return;

        //Setup specific Game Settings and Values

    }

    public static void callSceneMenu() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.menu))
            return;

        //Setup specific Game Settings and Values
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupMenu();
        ParticleSceneSetup.instance.SetupMenu();
        UiSceneScript.instance.SetupMenu();
        ShakeScript.instance.SetupMenu();
        ScoreScript.instance.SetupDisabled();
    }

    public static void callSceneIngame(bool reset) {
        //Setup Ingame if not already happend
        if (SaveDataManager.getValue.gameStatus != GameStatus.ingame)
            IngameScript.instance.setupIngame(reset);
    }

    public static void callSceneEndgame() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.endgame))
            return;
        EndgameScript.instance.SetupEndgame();
    }

    public static void callScenePause() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.paused))
            return;

        //Setup specific Game Settings and Values
        Time.timeScale = 0f;
        UiSceneScript.instance.SetupPause();
        PlayerSceneSetup.instance.SetupPause();
        ShakeScript.instance.SetupIngame();
        ScoreScript.instance.SetupDisabled();
    }

    public static void callSceneResume() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.ingame))
            return;

        //Setup specific Game Settings and Values
        Time.timeScale = 1f;
        UiSceneScript.instance.SetupIngame();
        PlayerSceneSetup.instance.SetupResume();
        ShakeScript.instance.SetupIngame();
        ScoreScript.instance.SetupActive();
    }

    public static void callSceneSettings() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.settings))
            return;

        //Setup specific Game Settings and Values
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupDisabled();
        ParticleSceneSetup.instance.SetupDisabled();
        UiSceneScript.instance.SetupSettings();
        ShakeScript.instance.SetupDisabled();
        ScoreScript.instance.SetupDisabled();
    }

    public static void callSceneAdsNotification() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.notification))
            return;

        //Setup specific Game Settings and Values
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupDisabled();
        ParticleSceneSetup.instance.SetupDisabled();
        UiSceneScript.instance.SetupNotificationAds();
        ShakeScript.instance.SetupDisabled();
        ScoreScript.instance.SetupDisabled();
    }

    public static void callSceneShop() {
        //Return if Scene already called
        if (!allowSceneSwitch(GameStatus.shop))
            return;
        //Setup specific Game Settings and Values
        Time.timeScale = 1f;
        PlayerSceneSetup.instance.SetupMenu();
        ParticleSceneSetup.instance.SetupDisabled();
        UiSceneScript.instance.SetupShop();
        ShakeScript.instance.SetupDisabled();
        ScoreScript.instance.SetupDisabled();
    }






    private static bool allowSceneSwitch(GameStatus status) {
        //Return if Scene already called
        if (SaveDataManager.getValue.gameStatus == status)
            return false;
        Debug.Log(LogTime.Time() + ": Scene Manager - Loading " + status.ToString() + "...");

        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = status;
        SaveDataManager.Save();
        return true;
    }
}
