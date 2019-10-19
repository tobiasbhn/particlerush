using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static AdsManager instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //ADS VARS
    public string rewardedAdId;
    public string videoAdID;
    [HideInInspector] public float lastAdShown;
    private GameStatus statusBeforAd;

    void Awake() {
        instance = this;
    }
    void Start() {
        lastAdShown = Time.time;
        thisScriptLoaded = true;
    }

    public void ShowRewardedAd() {
        BeforeAdProcess();
        if (Advertisement.isInitialized && Advertisement.IsReady(rewardedAdId)) {
            //Show Unity Ad
            ShowOptions showOptions = new ShowOptions();
            showOptions.resultCallback = RewaredAdResult;
            Advertisement.Show(rewardedAdId, showOptions);
        } else {
            //Show own Ad
            Debug.Log("Would show own Ad.");
        }
    }
    private void RewaredAdResult(ShowResult result) {
        if (result == ShowResult.Finished) {
            AfterAdProcess();
            Debug.Log("Reward");
        } else if (result == ShowResult.Skipped) {
            AfterAdProcess();
        } else if (result == ShowResult.Failed) {
            AfterAdProcess();
        }
    }

    private void BeforeAdProcess() {
        statusBeforAd = SaveDataManager.getValue.gameStatus;
        SaveDataManager.getValue.gameStatus = GameStatus.advert;
        SaveDataManager.Save();
        Time.timeScale = 0;
    }
    private void AfterAdProcess() {
        lastAdShown = Time.time;
        SaveDataManager.getValue.gameStatus = statusBeforAd;
        SaveDataManager.Save();
        Time.timeScale = 1f;
    }

}
