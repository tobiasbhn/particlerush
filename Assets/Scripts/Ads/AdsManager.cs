using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static AdsManager instance;

    //ADS VARS
    public string rewardedAdId;
    public string videoAdID;
    [HideInInspector] public float lastAdShown = 0f;
    private GameStatus statusBeforAd;
    private AdResult resultToReturn;

    void Awake() {
        instance = this;
    }

    // PUBLIC CALLER FUNCTIONS
    public void ShowAd(AdType type) {
        StartCoroutine(ShowAdHelper(type, (AdResult res) => { }));
    }
    public void ShowAd(AdType type, System.Action<AdResult> callback) {
        StartCoroutine(ShowAdHelper(type, callback));
    }
    public void ShowAd(AdType type, System.Action callback) {
        StartCoroutine(ShowAdHelper(type, callback));
    }




    // PRIVATE HELPER FUNCTIONS
    private IEnumerator ShowAdHelper(AdType type, System.Action callback) {
        StartCoroutine(ShowAdHelper(type, (AdResult res) => {
            callback.Invoke();
        }));
        yield return null;
    }
    private IEnumerator ShowAdHelper(AdType type, System.Action<AdResult> callback) {
        if (!ConstantManager.debugMode) {
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
                UiSceneScript.instance.ShowOwnAd(() => {
                    AfterAdProcess();
                    _return = AdResult.Private;
                    isReady = true;
                });
            }
            while (!isReady)
                yield return null;
            Debug.Log(LogTime.Time() + ": Ads Manager - Shown Ad (Rewarded Video) Result: " + _return.ToString() + "...");
            callback.Invoke(_return);
        } else {
            callback.Invoke(AdResult.Failed);
        }
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
