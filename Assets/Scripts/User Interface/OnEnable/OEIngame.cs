using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OEIngame : MonoBehaviour {
    void OnEnable() {
        SetupItemSlots();
    }

    private void SetupItemSlots() {
        UiObjectReferrer.instance.ingameGoldText.gameObject.SetActive(true);
        UiObjectReferrer.instance.ingameScoreText.gameObject.SetActive(true);
        UiObjectReferrer.instance.ingamePauseButton.SetActive(true);
    }
}
