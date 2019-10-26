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
    private AdResult resultToReturn;

    void Awake() {
        instance = this;
    }
    void Start() {
        lastAdShown = Time.realtimeSinceStartup;
        thisScriptLoaded = true;
    }

    public IEnumerator ShowAd(AdType type) {
        StartCoroutine(ShowAd(type, (AdResult res) => { }));
        yield return null;
    }

    // Call: StartCoroutine(ShowRewardedAd( (AdResult result) => { Debug.Log(result); }));
    public IEnumerator ShowAd(AdType type, System.Action<AdResult> callback) {
        Debug.Log(LogTime.Time() + ": Ads Manager - Ad (Rewarded Video) requestet...");

        BeforeAdProcess();
        bool isReady = false;
        AdResult _return = AdResult.Failed;
        string idToUse = type == AdType.Normal ? videoAdID : rewardedAdId;

        if (Advertisement.isInitialized && Advertisement.IsReady(idToUse)) {
            //Show Unity Ad
            ShowOptions showOptions = new ShowOptions();
            showOptions.resultCallback = (ShowResult result) => {
                AfterAdProcess();
                if (result == ShowResult.Finished) {
                    _return = AdResult.Finished;
                    isReady = true;
                } else if (result == ShowResult.Skipped) {
                    _return = AdResult.Skipped;
                    isReady = true;
                } else if (result == ShowResult.Failed) {
                    _return = AdResult.Failed;
                    isReady = true;
                }
            };
            Advertisement.Show(idToUse, showOptions);
        } else {
            //Show own Ad
            Debug.Log("Would show own Ad.");
            AfterAdProcess();
            _return = AdResult.Private;
            isReady = true;
        }
        while (!isReady)
            yield return null;
        Debug.Log(LogTime.Time() + ": Ads Manager - Shown Ad (Rewarded Video) Result: " + _return.ToString() + "...");
        callback.Invoke(_return);
    }

    private void BeforeAdProcess() {
        statusBeforAd = SaveDataManager.getValue.gameStatus;
        SaveDataManager.getValue.gameStatus = GameStatus.advert;
        SaveDataManager.Save();
        Time.timeScale = 0f;
    }
    private void AfterAdProcess() {
        lastAdShown = Time.realtimeSinceStartup;
        SaveDataManager.getValue.gameStatus = statusBeforAd;
        SaveDataManager.Save();
        Time.timeScale = 1f;
    }

}
