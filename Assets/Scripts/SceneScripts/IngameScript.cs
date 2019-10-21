using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static IngameScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    void Awake() {
        instance = this;
        thisScriptLoaded = true;
    }

    public void setupIngame() {
        if (Time.realtimeSinceStartup - AdsManager.instance.lastAdShown > ConstantManager.AD_TIME_TO_PASS_TO_SHOW_AD) {
            StartCoroutine(AdsManager.instance.ShowAd(AdType.Normal, (AdResult result) => {
                //Dont care about Result, but render Ingame Scene on Ad-Ended
                setupIngameHelper();
            }));
        } else {
            setupIngameHelper();
        }
    }

    private void setupIngameHelper() {
        //Setup specific Game Settings and Values
        SaveDataManager.getValue.gameStatus = GameStatus.ingame;
        SaveDataManager.Save();
        Time.timeScale = 1f;
        EndgameScript.instance.SetupIngame();
        RuntimeDataManager.instance.SetupIngame(); // RuntimeDataManager need to be setup after Endgame Setup (so alreadyRevived is Reset)
        PlayerSceneSetup.instance.SetupIngame();
        ParticleSceneSetup.instance.SetupIngame();
        UiSceneScript.instance.SetupIngame();
        ShakeScript.instance.SetupIngame();
    }
}
