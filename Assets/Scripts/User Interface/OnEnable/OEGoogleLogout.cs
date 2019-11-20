using UnityEngine;

public class OEGoogleLogout : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.notificationLogoutContinueBDE.interactable = true;
        UiObjectReferrer.instance.notificationLogoutContinueBEN.interactable = true;
        UiObjectReferrer.instance.notificationLogoutBackBDE.interactable = true;
        UiObjectReferrer.instance.notificationLogoutBackBEN.interactable = true;
        UiObjectReferrer.instance.notificationLogoutContinueTDE.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLogoutContinueTEN.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLogoutBackTDE.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLogoutBackTEN.color = new Color32(255,255,255,255);
    }
}
