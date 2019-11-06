﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnIngameEnable : MonoBehaviour {
    void OnEnable() {
        SetupItemSlots();
    }

    private void SetupItemSlots() {
        if (SaveDataManager.getValue.settingsItemPosition == SettingsItemPosition.Right) {
            UiObjectReferrer.instance.ingameItemSlotsLeft.SetActive(false);
            UiObjectReferrer.instance.ingameItemSlotsRight.SetActive(true);
        } else if (SaveDataManager.getValue.settingsItemPosition == SettingsItemPosition.Left) {
            UiObjectReferrer.instance.ingameItemSlotsLeft.SetActive(true);
            UiObjectReferrer.instance.ingameItemSlotsRight.SetActive(false);
        }
    }
}
