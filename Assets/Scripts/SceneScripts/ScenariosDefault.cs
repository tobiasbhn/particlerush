using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenariosDefault : ScriptableObject {
    [Header("GENERAL")]
    [SerializeField] private GameStatus gameStatus;
    [Header("PLAYER")]
    [SerializeField] private PlayerSceneModis playerModi;
    [SerializeField] private PlayerInputSceneModis playerInputModi;
    [Header("PARTICLES")]
    [SerializeField] private ParticleBehaviourSceneModis particleBehaviourModi;
    [SerializeField] private ParticleSpawnModi particleSpawnModi;
    [Header("UI")]
    [SerializeField] private UiSceneModis uiModi;
    [SerializeField] private ShakeSceneModis shakeModi;
    [SerializeField] private UiShowAccountInfo accountInfo;
    [SerializeField] private UiShowLevelInfo levelInfo;
    [Header("GAME LOOP")]
    [SerializeField] private ReviveSceneModis reviveModi;
    [SerializeField] private AdsSceneModis adsModi;
    [SerializeField] private TimeSceneModis timeModi;
    [Header("DATA")]
    [SerializeField] private RuntimeDataSceneModi runtimeDataModi;
    [SerializeField] private ScoreSceneModis scoreModi;
    [Header("AUDIO")]
    [SerializeField] private AudioLowPassModi lowPassModi;
    [Header("TUTORIAL")]
    [SerializeField] private TutorialActionModie tutorialMode;

    public virtual void callScenario() {
        // Set previous Scenario
        if (SceneManager.instance.currentScenario != null)
            SceneManager.instance.previousScenario = SceneManager.instance.currentScenario;

        //Set above Modis and call scripts
        if (SaveDataManager.getValue.gameStatus != gameStatus || gameStatus == GameStatus.notification) {
            //Show Add?
            if (Time.realtimeSinceStartup - AdsManager.instance.lastAdShown > ConstantManager.AD_TIME_TO_PASS_TO_SHOW_AD && adsModi == AdsSceneModis.allow) {
                AdsManager.instance.ShowAd(AdType.Normal, callScenarioHelper);
            } else {
                callScenarioHelper();
            }
        }
        // Set current Scenario
        if (gameStatus != GameStatus.notification)
            SceneManager.instance.currentScenario = this;
    }

    private void callScenarioHelper() {
        SetupTime();
        SetupRevive();
        SetupRuntimeData();
        SetupPlayer();
        SetupParticleBehaviour();
        SetupParticleSpawnModi();
        SetupUI();
        SetupAccountInfo();
        SetupLevelInfo();
        SetupShake();
        SetupScore();
        SetupPlayerInput();
        SetupLowPassModi();
        SetupSound();
        SetupTutorial();
        SaveDataManager.getValue.gameStatus = gameStatus;
        SaveDataManager.Save();
        FPS.instance.UpdateShow();
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
    private void SetupPlayerInput() {

    }
    private void SetupParticleBehaviour() {
        switch (particleBehaviourModi) {
            case ParticleBehaviourSceneModis.keepCurrent:
                break;
            case ParticleBehaviourSceneModis.disabled:
                ParticleSceneSetup.instance.SetupDisabled();
                break;
            case ParticleBehaviourSceneModis.ingame:
                ParticleSceneSetup.instance.SetupIngame();
                break;
            case ParticleBehaviourSceneModis.menu:
                ParticleSceneSetup.instance.SetupMenu();
                break;
        }
    }
    private void SetupParticleSpawnModi() {
        ParticleSpawnScript.instance.spawnModi = particleSpawnModi;
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
            case UiSceneModis.adNotification:
                UiSceneScript.instance.SetupNotificationAds();
                break;
            case UiSceneModis.openSource:
                UiSceneScript.instance.SetupOpenSource();
                break;
            case UiSceneModis.statistics:
                UiSceneScript.instance.SetupStats();
                break;
            case UiSceneModis.tutorial:
                UiSceneScript.instance.SetupTutorial();
                break;
            case UiSceneModis.login:
                UiSceneScript.instance.SetupNotificationLogin();
                break;
            case UiSceneModis.logout:
                UiSceneScript.instance.SetupNotificationLogout();
                break;
            case UiSceneModis.googleResult:
                UiSceneScript.instance.SetupNotificationGoogleResult();
                break;
        }
    }
    private void SetupAccountInfo() {
        switch (accountInfo) {
            case UiShowAccountInfo.disabled:
                UiObjectReferrer.instance.accountInfoMain.SetActive(false);
                break;
            case UiShowAccountInfo.show:
                UiObjectReferrer.instance.accountInfoMain.SetActive(true);
                break;
        }
    }
    public void SetupLevelInfo() {
        switch (levelInfo) {
            case UiShowLevelInfo.disabled:
                UiObjectReferrer.instance.levelInfoMain.SetActive(false);
                break;
            case UiShowLevelInfo.show:
                UiObjectReferrer.instance.levelInfoMain.SetActive(true);
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
    private void SetupLowPassModi() {
        switch (lowPassModi) {
            case AudioLowPassModi.lowPass:
                SoundScript.instance.EnableLowPass();
                break;
            case AudioLowPassModi.normal:
                SoundScript.instance.DisableLowPass();
                break;
        }
    }
    private void SetupSound() {
        switch (SaveDataManager.getValue.settingsSound) {
            case SettingsSounds.Off:
                SoundScript.instance.SoundOff();
                break;
            case SettingsSounds.All:
                SoundScript.instance.SoundOn();
                break;
            case SettingsSounds.Sound:
                SoundScript.instance.SoundOnlySound();
                break;
        }
    }
    private void SetupTutorial() {
        if (tutorialMode == TutorialActionModie.start)
            TutorialScript.instance.StartTutorial();
    }
}
