using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSceneScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static UiSceneScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        thisScriptLoaded = true;
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

    public void SetupSettings() {
        DisableAllMenus();
        UiObjectReferrer.instance.settingsMain.SetActive(true);
    }

    public void SetupNotificationAds() {
        DisableAllMenus();
        UiObjectReferrer.instance.notificationAdsMain.SetActive(true);
    }

    private void DisableAllMenus() {
        UiObjectReferrer.instance.ingameMain.SetActive(false);
        UiObjectReferrer.instance.menuMain.SetActive(false);
        UiObjectReferrer.instance.pauseMain.SetActive(false);
        UiObjectReferrer.instance.settingsMain.SetActive(false);
        UiObjectReferrer.instance.notificationAdsMain.SetActive(false);
    }
}
