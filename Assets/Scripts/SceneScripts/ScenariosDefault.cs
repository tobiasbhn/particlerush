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
    [Header("ITEMS")]
    [SerializeField] private ItemInputSceneModis itemInputModi;
    [SerializeField] private ItemSpawnSceneModi itemSpawnModi;
    [Header("UI")]
    [SerializeField] private UiSceneModis uiModi;
    [SerializeField] private ShakeSceneModis shakeModi;
    [Header("GAME LOOP")]
    [SerializeField] private ReviveSceneModis reviveModi;
    [SerializeField] private AdsSceneModis adsModi;
    [SerializeField] private TimeSceneModis timeModi;
    [Header("DATA")]
    [SerializeField] private RuntimeDataSceneModi runtimeDataModi;
    [SerializeField] private ScoreSceneModis scoreModi;

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
        SetupParticleBehaviour();
        SetupParticleSpawnModi();
        SetupItemSpawnModi();
        SetupUI();
        SetupShake();
        SetupScore();
        SetupItemInput();
        SetupPlayerInput();
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
        switch (particleSpawnModi) {
            case ParticleSpawnModi.all:
                ParticleSceneSetup.instance.SpawnModiAll();
                break;
            case ParticleSpawnModi.none:
                ParticleSceneSetup.instance.SpawnModiNone();
                break;
            case ParticleSpawnModi.onlyGold:
                ParticleSceneSetup.instance.SpawnModiGold();
                break;
            case ParticleSpawnModi.onlyMassRelative:
                ParticleSceneSetup.instance.SpawnModiMass();
                break;
            case ParticleSpawnModi.onlyNorm:
                ParticleSceneSetup.instance.SpawnModiGrow();
                break;
            case ParticleSpawnModi.onlyShrink:
                ParticleSceneSetup.instance.SpawnModiShrink();
                break;
        }
    }
    private void SetupItemSpawnModi() {
        switch (itemSpawnModi) {
            case ItemSpawnSceneModi.refuse:
                ItemSpawnScript.instance.SetupDisabled();
                break;
            case ItemSpawnSceneModi.allow:
                ItemSpawnScript.instance.SetupEnabled();
                break;
        }
    }
    private void SetupItemInput() {
        switch (itemInputModi) {
            case ItemInputSceneModis.refuse:
                ItemInputScript.instance.SetupDisable();
                break;
            case ItemInputSceneModis.allow:
                ItemInputScript.instance.SetupEnable();
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
            case UiSceneModis.adNotification:
                UiSceneScript.instance.SetupNotificationAds();
                break;
            case UiSceneModis.openSource:
                UiSceneScript.instance.SetupOpenSource();
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
