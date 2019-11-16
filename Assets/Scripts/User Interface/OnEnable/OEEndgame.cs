using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OEEndgame : MonoBehaviour {

    void OnEnable() {
        ButtonsNotInteractable();
        StartCoroutine(activateButtons());
        var score = "Score: " + ((int)RuntimeDataManager.instance.preRevive.score + (int)RuntimeDataManager.instance.postRevive.score).ToString("000000");
        UiObjectReferrer.instance.endgameScoreTextDE.text = score;
        UiObjectReferrer.instance.endgameScoreTextEN.text = score;
        var highscore = "Highscore: " + ((int)RuntimeDataManager.value.highscore).ToString("000000");
        UiObjectReferrer.instance.endgameHighscoreTextDE.text = highscore;
        UiObjectReferrer.instance.endgameHighscoreTextEN.text = highscore;
        if (ScoreScript.instance.newHighscore) {
            UiObjectReferrer.instance.endgameGameOverTextEN.text = "NEW HIGHSCORE!";
            UiObjectReferrer.instance.endgameGameOverTextDE.text = "NEUER HIGHSCORE!";
        } else {
            UiObjectReferrer.instance.endgameGameOverTextEN.text = "GAME OVER";
            UiObjectReferrer.instance.endgameGameOverTextDE.text = "SPIELENDE";
        }
        ReviveScript.instance.SetupEndgame();
    }

    private IEnumerator activateButtons() {
        yield return new WaitForSecondsRealtime(1f);
        ButtonsInteractable();
    }

    void ButtonsInteractable() {
        UiObjectReferrer.instance.endgameButtonMenuDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonMenuEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonRestartDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonRestartEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonShopDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonShopEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonSettingsde.interactable = true;
        UiObjectReferrer.instance.endgameButtonSettingsEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveAdsDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveAdsEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveGoldDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveGoldEN.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveSkipDE.interactable = true;
        UiObjectReferrer.instance.endgameButtonReviveSkipEN.interactable = true;
    }
    void ButtonsNotInteractable() {
        UiObjectReferrer.instance.endgameButtonMenuDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonMenuEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonRestartDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonRestartEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonShopDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonShopEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonSettingsde.interactable = false;
        UiObjectReferrer.instance.endgameButtonSettingsEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveAdsDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveAdsEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveGoldDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveGoldEN.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveSkipDE.interactable = false;
        UiObjectReferrer.instance.endgameButtonReviveSkipEN.interactable = false;
    }
}