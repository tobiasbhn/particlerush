using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OEAccountInfo : MonoBehaviour {

    // INSTANCE
    public static OEAccountInfo instance;
    void Awake() {
        instance = this;
    }

    void OnEnable() {
        UpdateAccountInfo();
    }
    public void UpdateAccountInfo() {
        if (SaveDataManager.getValue.currentGold > 0 || SaveDataManager.getValue.currentLevel > 0) {
            UiObjectReferrer.instance.accountInfoGold.text = ((int)SaveDataManager.getValue.currentGold).ToString();
            UiObjectReferrer.instance.accountInfoHighscore.text = ((int)SaveDataManager.getValue.highscore).ToString();
        } else {
            UiObjectReferrer.instance.accountInfoMain.SetActive(false);
        }
    }
}
