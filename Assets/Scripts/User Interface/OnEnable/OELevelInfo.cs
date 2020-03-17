using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OELevelInfo : MonoBehaviour {

    // INSTANCE
    public static OELevelInfo instance;
    void Awake() {
        instance = this;
    }

    void OnEnable() {
        UpdateInfos();
    }
    public void UpdateInfos() {
        if (SaveDataManager.getValue.currentLevelPoints > 0 && !ConstantManager.demoMode) {
            var currentLevel = SaveDataManager.getValue.currentLevel;
            var percent = 100;
            if (currentLevel != ConstantManager.LEVEL_MAX_LEVEL) {
                var goal = ConstantManager.LEVEL_POINTS[currentLevel];
                var current = SaveDataManager.getValue.currentRemainingLevelPoints;
                percent = 100 * current / goal;
                percent = Mathf.Min(percent, 100);
                percent = Mathf.Max(percent, 0);

                var nextLvlText = currentLevel == ConstantManager.LEVEL_MAX_LEVEL - 1 ? "MAX" : (currentLevel + 1).ToString();
                UiObjectReferrer.instance.levelCurrentText.text = currentLevel.ToString();
                UiObjectReferrer.instance.levelNextText.text = nextLvlText;
                UiObjectReferrer.instance.levelInfoText.text = "LEVEL";
            } else {
                UiObjectReferrer.instance.levelCurrentText.text = "";
                UiObjectReferrer.instance.levelNextText.text = "";
                UiObjectReferrer.instance.levelInfoText.text = "MAX";
            }
            var fullWidth = UiObjectReferrer.instance.levelInfoSliderContainer.rect.width;
            var contentRectTransform = UiObjectReferrer.instance.levelInfoSliderContent;
            contentRectTransform.sizeDelta = new Vector2((fullWidth / 100 * percent), contentRectTransform.sizeDelta.y);


        } else {
            UiObjectReferrer.instance.levelInfoMain.SetActive(false);
        }
    }
}
