using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour {
    
    //INSTANCE
    [HideInInspector] public static ShakeScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //SHAKE SETTINGS
    private bool allowShake = false;
    private Vector3 originalCamPos;


    void Awake() {
        instance = this;
        originalCamPos = transform.localPosition;
        thisScriptLoaded = true;
    }

    public void Shake() {
        StartCoroutine(ShakeHelper());
    }

    private IEnumerator ShakeHelper() {
        var durationLeft = ConstantManager.CAMERA_SHAKE_DURATION;
        if (allowShake)
            PostProcessingScript.instance.Eskalate();
        while (allowShake && durationLeft >= 0) {
            var shake = Random.insideUnitSphere * ConstantManager.CAMERA_SHAKE_AMOUNT * durationLeft;
            transform.localPosition = originalCamPos + shake;
            durationLeft -= Time.deltaTime * ConstantManager.CAMERA_SHAKE_DECREASE;
            yield return null;
        }
        transform.localPosition = originalCamPos;
    }

    public void SetupIngame() {
        allowShake = ConstantManager.CAMERA_SHAKE_ALLOW_INGAME;
    }
    public void SetupMenu() {
        allowShake = ConstantManager.CAMERA_SHAKE_ALLOW_MENU;
    }
    public void SetupDisabled() {
        allowShake = false;
    }
}