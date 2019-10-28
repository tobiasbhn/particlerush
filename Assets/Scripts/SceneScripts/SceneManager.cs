using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static SceneManager instance;

    public ScenariosDefault ingame;
    public ScenariosDefault menu;
    public ScenariosDefault endgame;
    public ScenariosDefault pause;
    public ScenariosDefault resume;
    public ScenariosDefault revive;
    public ScenariosDefault shop;
    public ScenariosDefault settings;
    public ScenariosDefault adsNotification;
    public ScenariosDefault openSource;

    void Awake() {
        instance = this;
    }


    public void startGame() {
        Debug.Log(LogTime.Time() + ": Scene Manager - Going to start Game...");
        callSceneMenu();
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
    public void callSceneOpenSource() {
        openSource.callScenario();
    }
}
