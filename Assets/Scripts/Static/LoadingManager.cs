using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingManager {
    public static bool isEverythingLoaded() {
            // PARTICLES
        if (ParticleSpawnScript.instance != null &&
            ParticleSceneSetup.instance != null &&

            // PLAYER
            PlayerScript.instance != null &&
            PlayerMovementScript.instance != null &&
            PlayerSceneSetup.instance != null &&
            ShootingController.instance != null &&

            // SHAKE
            PostProcessingScript.instance != null &&
            ShakeScript.instance != null &&
            
            // UI
            ScreenSetupScript.instance != null &&
            UiSceneScript.instance != null &&
            UiObjectReferrer.instance != null &&
            ButtonScript.instance != null) {

            if (// PARTICLES
                ParticleSpawnScript.instance.thisScriptLoaded &&
                ParticleSceneSetup.instance.thisScriptLoaded &&

                // PLAYER
                PlayerScript.instance.thisScriptLoaded &&
                PlayerMovementScript.instance.thisScriptLoaded &&
                PlayerSceneSetup.instance.thisScriptLoaded &&
                ShootingController.instance.thisScriptLoaded &&

                // SHAKE
                PostProcessingScript.instance.thisScriptLoaded &&
                ShakeScript.instance.thisScriptLoaded &&
                
                // UI
                ScreenSetupScript.instance.thisScriptLoaded &&
                UiSceneScript.instance.thisScriptLoaded &&
                UiObjectReferrer.instance.thisScriptLoaded &&
                ButtonScript.instance.thisScriptLoaded) {
                    return true;
            }
        }
        return false;
    }
}
