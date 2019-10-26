using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static EndgameScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //BEHAVIOUR
    [HideInInspector] public bool alreadyRevived = false;

    void Awake() {
        instance = this;
        thisScriptLoaded = true;
    }

    public void SetupEndgame() {
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.endgame;
        RuntimeDataManager.instance.SaveRuntimeData();
        ShakeScript.instance.SetupIngame();
        ScoreScript.instance.SetupDisabled();
        StartCoroutine(animateEndgame());
    }

    public void SetupIngame() {
        alreadyRevived = false;
    }

    private IEnumerator animateEndgame() {
        //Reduce timeScale (animated)
        while (Time.timeScale > 0) {
            Time.timeScale -= Time.deltaTime * 7.5f;
            if (Time.timeScale < 0.5f) {
                // Disable Input
                PlayerSceneSetup.instance.SetupPause();
                // Spawn no more PArticles, Destroy all current Particles
                ParticleSceneSetup.instance.SetupDisabled();
            }
            if (Time.timeScale < 0.001f)
                break; // Exit Loop if amount is too small
            yield return null;
        }
        // Disable last bit of timeScle (Should be al least at 0.001f or less)
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.3f);

        // Show UI
        UiSceneScript.instance.SetupEndgame();
    }

    public void reviveAd() {
        alreadyRevived = true;
        StartCoroutine(AdsManager.instance.ShowAd(AdType.Rewarded, (AdResult result) => {
            if (result == AdResult.Finished) {
                SceneManager.callSceneIngame(false);
            } else {
                SetupLevel();
            }
        }));
    }

    public void reviveGold() {
        alreadyRevived = true;
        //SUBSTRACT GOLD
        SceneManager.callSceneIngame(false);
    }

    
    public void SetupRevive() {
        StartCoroutine(ReviveCountdown());
        UiObjectReferrer.instance.endgameReviveDE.SetActive(true);
        UiObjectReferrer.instance.endgameReviveEN.SetActive(true);
        UiObjectReferrer.instance.endgameLevelDE.SetActive(false);
        UiObjectReferrer.instance.endgameLevelEN.SetActive(false);
    }
    public void SetupLevel() {
        UiObjectReferrer.instance.endgameReviveDE.SetActive(false);
        UiObjectReferrer.instance.endgameReviveEN.SetActive(false);
        UiObjectReferrer.instance.endgameLevelDE.SetActive(true);
        UiObjectReferrer.instance.endgameLevelEN.SetActive(true);
    }

    private IEnumerator ReviveCountdown() {
        yield return new WaitForSecondsRealtime(5f);
        SetupLevel();
    }
}
