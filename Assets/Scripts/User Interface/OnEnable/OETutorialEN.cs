﻿using UnityEngine;

public class OETutorialEN : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.tutorialStep1EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep2EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep3EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep4EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep5EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep6EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep7EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialHeadingEN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialSkipEN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialNextEN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialTutorialEN.gameObject.SetActive(false);
    }
}
