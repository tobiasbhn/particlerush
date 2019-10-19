using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingScript : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static PostProcessingScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //SETTINGS
    private ChromaticAberration chromaticAberration;
    private float originalPPValue;

    void Awake() {
        instance = this;

        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromaticAberration);
        originalPPValue = chromaticAberration.intensity.value;
        thisScriptLoaded = true;
    }
    public void Eskalate() {
        StartCoroutine(EskalateHelper());
    }

    private IEnumerator EskalateHelper() {
        var durationLeft = ConstantManager.CAMERA_SHAKE_DURATION;
        while (durationLeft >= 0) {
            var amount = ConstantManager.PP_CHROMATIC_ABBERATION_ESKALATION * durationLeft;
            chromaticAberration.intensity.value = originalPPValue + amount;
            durationLeft -= Time.deltaTime * ConstantManager.CAMERA_SHAKE_DECREASE;
            yield return null;
        }
        chromaticAberration.intensity.value = originalPPValue;
    }
}