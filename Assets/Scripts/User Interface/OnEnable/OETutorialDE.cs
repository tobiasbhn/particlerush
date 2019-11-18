using UnityEngine;

public class OETutorialDE : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.tutorialStep1DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep2DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep3DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep4DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep5DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep6DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep7DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialHeadingDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialSkipDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialNextDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialTutorialDE.gameObject.SetActive(false);
    }
}
