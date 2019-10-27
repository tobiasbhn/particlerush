using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenariosDefault : ScriptableObject {
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private PlayerSceneModis playerModi;
    [SerializeField] private ParticleSceneModis particleModi;
    [SerializeField] private TimeSceneModis timeModi;
    [SerializeField] private UiSceneModis uiModi;
    [SerializeField] private ReviveSceneModis reviveModi;
    [SerializeField] private ShakeSceneModis shakeModi;
    [SerializeField] private ScoreSceneModis scoreModi;
    [SerializeField] private RuntimeDataSceneModi runtimeDataModi;
    [SerializeField] private AdsSceneModis adsModi;

    public virtual void callScenario() {
        //Set above Modis and call scripts
        if (SaveDataManager.getValue.gameStatus != gameStatus) {
            //Show Add?
            if (Time.realtimeSinceStartup - AdsManager.instance.lastAdShown > ConstantManager.AD_TIME_TO_PASS_TO_SHOW_AD && adsModi == AdsSceneModis.allow) {
                AdsManager.instance.ShowAd(AdType.Normal, callScenarioHelper);
            } else {
                callScenarioHelper();
            }
        }
    }

    private void callScenarioHelper() {
        SetupTime();
        SetupRevive();
        SetupRuntimeData();
        SetupPlayer();
        SetupParticles();
        SetupUI();
        SetupShake();
        SetupScore();
        SaveDataManager.getValue.gameStatus = gameStatus;
        SaveDataManager.Save();
    }

    private void SetupTime() {
        switch (timeModi) {
            case TimeSceneModis.keepCurrent:
                break;
            case TimeSceneModis.normal:
                Time.timeScale = 1f;
                break;
            case TimeSceneModis.stop:
                Time.timeScale = 0f;
                break;
        }
    }
    private void SetupRevive() {
        switch (reviveModi) {
            case ReviveSceneModis.keepCurrent:
                break;
            case ReviveSceneModis.reset:
                ReviveScript.instance.alreadyRevived = false;
                break;
        }
    }
    private void SetupRuntimeData() {
        switch (runtimeDataModi) {
            case RuntimeDataSceneModi.keepCurrent:
                break;
            case RuntimeDataSceneModi.ingame:
                RuntimeDataManager.instance.SetupIngame();
                break;
            case RuntimeDataSceneModi.endgame:
                RuntimeDataManager.instance.SetupEndgame();
                break;
        }
    }
    private void SetupPlayer() {
        switch (playerModi) {
            case PlayerSceneModis.keepCurrent:
                break;
            case PlayerSceneModis.disabled:
                PlayerSceneSetup.instance.SetupDisabled();
                break;
            case PlayerSceneModis.ingame:
                PlayerSceneSetup.instance.SetupIngame();
                break;
            case PlayerSceneModis.menu:
                PlayerSceneSetup.instance.SetupMenu();
                break;
            case PlayerSceneModis.pause:
                PlayerSceneSetup.instance.SetupPause();
                break;
            case PlayerSceneModis.resume:
                PlayerSceneSetup.instance.SetupResume();
                break;
        }
    }
    private void SetupParticles() {
        switch (particleModi) {
            case ParticleSceneModis.keepCurrent:
                break;
            case ParticleSceneModis.disabled:
                ParticleSceneSetup.instance.SetupDisabled();
                break;
            case ParticleSceneModis.ingame:
                ParticleSceneSetup.instance.SetupIngame();
                break;
            case ParticleSceneModis.menu:
                ParticleSceneSetup.instance.SetupMenu();
                break;
        }
    }
    private void SetupUI() {
        switch (uiModi) {
            case UiSceneModis.keepCurrent:
                break;
            case UiSceneModis.disabled:
                UiSceneScript.instance.DisableAllMenus();
                break;
            case UiSceneModis.ingame:
                UiSceneScript.instance.SetupIngame();
                break;
            case UiSceneModis.menu:
                UiSceneScript.instance.SetupMenu();
                break;
            case UiSceneModis.endgame:
                UiSceneScript.instance.SetupEndgame();
                break;
            case UiSceneModis.pause:
                UiSceneScript.instance.SetupPause();
                break;
            case UiSceneModis.settings:
                UiSceneScript.instance.SetupSettings();
                break;
            case UiSceneModis.shop:
                UiSceneScript.instance.SetupShop();
                break;
            case UiSceneModis.ad_notification:
                UiSceneScript.instance.SetupNotificationAds();
                break;
        }
    }
    private void SetupShake() {
        switch (shakeModi) {
            case ShakeSceneModis.keepCurrent:
                break;
            case ShakeSceneModis.disabled:
                ShakeScript.instance.SetupDisabled();
                break;
            case ShakeSceneModis.ingame:
                ShakeScript.instance.SetupIngame();
                break;
            case ShakeSceneModis.menu:
                ShakeScript.instance.SetupMenu();
                break;
        }
    }
    private void SetupScore() {
        switch (scoreModi) {
            case ScoreSceneModis.keepCurrent:
                break;
            case ScoreSceneModis.disabled:
                ScoreScript.instance.SetupDisabled();
                break;
            case ScoreSceneModis.active:
                ScoreScript.instance.SetupActive();
                break;
            case ScoreSceneModis.activeReset:
                ScoreScript.instance.SetupIngame();
                break;
        }
    }
}
