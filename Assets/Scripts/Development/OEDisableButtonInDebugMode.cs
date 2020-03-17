using UnityEngine;
using UnityEngine.UI;

public class OEDisableButtonInDebugMode : MonoBehaviour {

    public Button buttonToDisable;
    public Text textToDisable;
    public Text secondTextToDisable;
    void OnEnable() {
        if (ConstantManager.demoMode) {
            buttonToDisable.interactable = false;
            textToDisable.color = new Color32(255,255,255,90);
            if (secondTextToDisable != null)
                secondTextToDisable.color = new Color32(255,255,255,90);
        }
    }
}
