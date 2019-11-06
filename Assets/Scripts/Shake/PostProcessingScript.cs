using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingScript : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static PostProcessingScript instance;

    // OBJECT-LINKS
    [SerializeField] private PostProcessVolume volume;

    //SETTINGS
    private ChromaticAberration chromaticAberration;
    private float originalPPValue;

    void Awake() {
        instance = this;
    }
    void Start() {
        volume.profile.TryGetSettings(out chromaticAberration);
        originalPPValue = chromaticAberration.intensity.value;
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