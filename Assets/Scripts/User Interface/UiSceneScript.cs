using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSceneScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static UiSceneScript instance;

    //VARS
    [HideInInspector] public bool ownAdActive = false;

    void Awake() {
        instance = this;
    }


    public void SetupIngame() {
        DisableAllMenus();
        UiObjectReferrer.instance.ingameMain.SetActive(true);
    }

    public void SetupMenu() {
        DisableAllMenus();
        UiObjectReferrer.instance.menuMain.SetActive(true);
    }

    public void SetupPause() {
        DisableAllMenus();
        UiObjectReferrer.instance.pauseMain.SetActive(true);
    }

    public void SetupShop() {
        DisableAllMenus();
        UiObjectReferrer.instance.shopLowerMain.SetActive(true);
        UiObjectReferrer.instance.shopUpperMain.SetActive(true);
    }

    public void SetupSettings() {
        DisableAllMenus();
        UiObjectReferrer.instance.settingsMain.SetActive(true);
    }

    public void SetupNotificationAds() {
        DisableAllMenus();
        UiObjectReferrer.instance.notificationAdsMain.SetActive(true);
    }

    public void SetupEndgame() {
        DisableAllMenus();
        UiObjectReferrer.instance.endgameMain.SetActive(true);
    }

    public void SetupOpenSource() {
        DisableAllMenus();
        UiObjectReferrer.instance.openSourceMain.SetActive(true);
    }

    public void SetupStats() {
        DisableAllMenus();
        UiObjectReferrer.instance.statisticsMain.SetActive(true);
    }

    public void SetupTutorial() {
        DisableAllMenus();
        UiObjectReferrer.instance.tutorialMainTop.SetActive(true);
        UiObjectReferrer.instance.tutorialMainBottom.SetActive(true);
    }
    public void SetupNotificationLogin() {
        DisableAllMenus();
        UiObjectReferrer.instance.notificationLoginMain.SetActive(true);
    }
    public void SetupNotificationLogout() {
        DisableAllMenus();
        UiObjectReferrer.instance.notificationLogoutMain.SetActive(true);
    }
    public void SetupNotificationGoogleResult() {
        DisableAllMenus();
        UiObjectReferrer.instance.notificationGoogleResultMain.SetActive(true);
    }
    public void SetupLeaderboard() {
        DisableAllMenus();
        UiObjectReferrer.instance.highscoreMain.SetActive(true);
    }

    public void DisableAllMenus() {
        UiObjectReferrer.instance.ingameMain.SetActive(false);
        UiObjectReferrer.instance.menuMain.SetActive(false);
        UiObjectReferrer.instance.pauseMain.SetActive(false);
        UiObjectReferrer.instance.settingsMain.SetActive(false);
        UiObjectReferrer.instance.notificationAdsMain.SetActive(false);
        UiObjectReferrer.instance.endgameMain.SetActive(false);
        UiObjectReferrer.instance.shopLowerMain.SetActive(false);
        UiObjectReferrer.instance.shopUpperMain.SetActive(false);
        UiObjectReferrer.instance.openSourceMain.SetActive(false);
        UiObjectReferrer.instance.statisticsMain.SetActive(false);
        UiObjectReferrer.instance.tutorialMainTop.SetActive(false);
        UiObjectReferrer.instance.tutorialMainBottom.SetActive(false);
        UiObjectReferrer.instance.notificationLoginMain.SetActive(false);
        UiObjectReferrer.instance.notificationLogoutMain.SetActive(false);
        UiObjectReferrer.instance.notificationGoogleResultMain.SetActive(false);
        UiObjectReferrer.instance.highscoreMain.SetActive(false);
    }

    public void ShowOwnAd(System.Action callback) {
        ownAdActive = true;
        StartCoroutine(ShowOwnAdHelper(callback));
    }
    private IEnumerator ShowOwnAdHelper(System.Action callback) {
        Debug.Log("OWN ADD REAL FUNCTION HELPER");
        UiObjectReferrer.instance.ownAdMain.SetActive(true);
        while (ownAdActive)
            yield return null;
        Debug.Log("Finished");
        UiObjectReferrer.instance.ownAdMain.SetActive(false);
        callback.Invoke();
    }

    public void EnableOnlySwipeBar() {
        UiObjectReferrer.instance.ingameMain.SetActive(true);
        UiObjectReferrer.instance.ingameGoldText.gameObject.SetActive(false);
        UiObjectReferrer.instance.ingameScoreText.gameObject.SetActive(false);
        UiObjectReferrer.instance.ingamePauseButton.SetActive(false);
    }
}
