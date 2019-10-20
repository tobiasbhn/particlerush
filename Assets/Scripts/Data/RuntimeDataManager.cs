using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDataManager : MonoBehaviour {

    //INSTANCE
    public static RuntimeDataManager instance;
    public bool thisScriptLoaded = false;

    // STATS
    // Particles
    public int statsParticlesSpawnedTotal;

    void Awake() {
        instance = this;
    }
    void Start() {
        thisScriptLoaded = true;
    }

}
