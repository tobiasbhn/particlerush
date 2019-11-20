using UnityEngine;

public class OEGoogleLogin : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.notificationLoginContinueBDE.interactable = true;
        UiObjectReferrer.instance.notificationLoginContinueBEN.interactable = true;
        UiObjectReferrer.instance.notificationLoginBackBDE.interactable = true;
        UiObjectReferrer.instance.notificationLoginBackBEN.interactable = true;
        UiObjectReferrer.instance.notificationLoginContinueTDE.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLoginContinueTEN.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLoginBackTDE.color = new Color32(255,255,255,255);
        UiObjectReferrer.instance.notificationLoginBackTEN.color = new Color32(255,255,255,255);
    }
}
