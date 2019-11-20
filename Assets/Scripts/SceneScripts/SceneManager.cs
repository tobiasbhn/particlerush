using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static SceneManager instance;

    // VARS
    public ScenariosDefault currentScenario;
    public ScenariosDefault previousScenario;

    // OBJECT LINKS
    public ScenariosDefault ingame;
    public ScenariosDefault menu;
    public ScenariosDefault endgame;
    public ScenariosDefault tutorial;
    public ScenariosDefault pause;
    public ScenariosDefault resume;
    public ScenariosDefault revive;
    public ScenariosDefault shop;
    public ScenariosDefault settings;
    public ScenariosDefault adsNotification;
    public ScenariosDefault loginNotification;
    public ScenariosDefault logoutNotification;
    public ScenariosDefault googleResult;
    public ScenariosDefault openSource;

    void Awake() {
        instance = this;
    }

    public void startGame() {
        Debug.Log(LogTime.Time() + ": Scene Manager - Going to start Game...");
        if (SaveDataManager.getValue.tutorialFinished)
            callSceneMenu();
        else
            callSceneTutorial();
    }


    public void callSceneIngame() {
        ingame.callScenario();
    }
    public void callSceneMenu() {
        menu.callScenario();
    }
    public void callSceneEndgame() {
        endgame.callScenario();
    }
    public void callSceneTutorial() {
        tutorial.callScenario();
    }
    public void callSceneRevive() {
        revive.callScenario();
    }
    public void callScenePause() {
        pause.callScenario();
    }
    public void callSceneResume() {
        resume.callScenario();
    }
    public void callSceneShop() {
        shop.callScenario();
    }
    public void callSceneSettings() {
        settings.callScenario();
    }
    public void callSceneAdsNotification() {
        adsNotification.callScenario();
    }
    public void callSceneLoginNotification() {
        loginNotification.callScenario();
    }
    public void callSceneLogoutNotification() {
        logoutNotification.callScenario();
    }
    public void callSceneGoogleResult() {
        googleResult.callScenario();
    }
    public void callSceneOpenSource() {
        openSource.callScenario();
    }
}
