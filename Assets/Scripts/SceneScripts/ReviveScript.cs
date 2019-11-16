using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static ReviveScript instance;

    //BEHAVIOUR
    [HideInInspector] public bool alreadyRevived = false;

    void Awake() {
        instance = this;
    }

    //SCENE SETUPS
    public void SetupEndgame() {
        if (alreadyRevived) {
            SetupLevel();
        } else {
            SetupRevive();
        }
    }
    public void SetupIngame() {
        alreadyRevived = false;
    }

    //FUNCTIONS TO CALL WHEN CLICKEDON ANY REVIVE ACCEPT OR DECLINE BUTTON
    public void reviveAd() {
        alreadyRevived = true;
        AdsManager.instance.ShowAd(AdType.Rewarded, (AdResult result) => {
            if (result == AdResult.Finished || result == AdResult.Private) {
                SceneManager.instance.callSceneRevive();
            } else {
                SetupLevel();
            }
        });
    }
    public void reviveGold() {
        alreadyRevived = true;
        //SUBSTRACT GOLD
        SceneManager.instance.callSceneRevive();
    }


    //FUNCTIONS TO UPDATE OR SETUP THE UI DEPENDING ON WHAT TO SHOW
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
        var time = Time.realtimeSinceStartup;
        var maxBarWidth = UiObjectReferrer.instance.endgameReviveDE.GetComponent<RectTransform>().rect.width;
        while (time + 5f > Time.realtimeSinceStartup) {
            var progress = 100 * (Time.realtimeSinceStartup - time) / ConstantManager.REVIVE_TIME;
            UiObjectReferrer.instance.endgameReviveBar1.sizeDelta = new Vector2((maxBarWidth / 100 * progress), UiObjectReferrer.instance.endgameReviveBar1.sizeDelta.y);
            UiObjectReferrer.instance.endgameReviveBar2.sizeDelta = new Vector2((maxBarWidth / 100 * progress), UiObjectReferrer.instance.endgameReviveBar2.sizeDelta.y);
            UiObjectReferrer.instance.endgameReviveBar3.sizeDelta = new Vector2((maxBarWidth / 100 * progress), UiObjectReferrer.instance.endgameReviveBar3.sizeDelta.y);
            UiObjectReferrer.instance.endgameReviveBar4.sizeDelta = new Vector2((maxBarWidth / 100 * progress), UiObjectReferrer.instance.endgameReviveBar4.sizeDelta.y);
            yield return null;
        }
        SetupLevel();
    }
}
