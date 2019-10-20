﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static EndgameScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //BEHAVIOUR
    private bool alreadyRevived = false;

    void Awake() {
        instance = this;
        thisScriptLoaded = true;
    }

    public void SetupEndgame() {
        StartCoroutine(animateEndgame());
    }

    public void SetupIngame() {
        alreadyRevived = false;
    }

    private IEnumerator animateEndgame() {
        //Reduce timeScale (animated)
        while (Time.timeScale > 0) {
            Time.timeScale -= Time.deltaTime * 7.5f;
            if (Time.timeScale < 0.5f) {
                // Disable Input
                PlayerSceneSetup.instance.SetupPause();
                // Spawn no more PArticles, Destroy all current Particles
                ParticleSceneSetup.instance.SetupDisabled();
            }
            if (Time.timeScale < 0.001f)
                break; // Exit Loop if amount is too small
            yield return null;
        }
        // Disable last bit of timeScle (Should be al least at 0.001f or less)
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.3f);

        // Show UI
        UiSceneScript.instance.SetupEndgame(!alreadyRevived);
    }
}
