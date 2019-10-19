using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageScript {
    public static void UpdateLanguage() {
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German) {
            DisableAllLanguages();
            EnableGerman();
        } else {
            DisableAllLanguages();
            EnableEnglish();
        }
    }

    private static void DisableAllLanguages() {
        foreach (GameObject obj in UiObjectReferrer.instance.objectsDE) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in UiObjectReferrer.instance.objectsEN) {
            obj.SetActive(false);
        }
    }

    private static void EnableEnglish() {
        foreach (GameObject obj in UiObjectReferrer.instance.objectsEN) {
            obj.SetActive(true);
        }
    }

    private static void EnableGerman() {
        foreach (GameObject obj in UiObjectReferrer.instance.objectsDE) {
            obj.SetActive(true);
        }
    }
}
