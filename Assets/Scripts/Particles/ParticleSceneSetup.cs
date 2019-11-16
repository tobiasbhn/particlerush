using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSceneSetup : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static ParticleSceneSetup instance;

    void Awake() {
        instance = this;
    }

    public void SetupIngame() {
        ParticleSpawnScript.instance.DestroyAllParticles();
        ParticleSpawnScript.instance.spawnDelay = ConstantManager.PARTICLE_SPAWN_DELAY_INGAME_MIN;
        ParticleSpawnScript.instance.spawnSpeed = ConstantManager.PARTICLE_SPAWN_SPEED_INGAME_MIN;
    }

    public void SetupMenu() {
        ParticleSpawnScript.instance.DestroyAllParticles();
        ParticleSpawnScript.instance.spawnDelay = ConstantManager.PARTICLE_SPAWN_DELAY_MENU;
        ParticleSpawnScript.instance.spawnSpeed = ConstantManager.PARTICLE_SPAWN_SPEED_MENU;
    }

    public void SetupDisabled() {
        ParticleSpawnScript.instance.DestroyAllParticles();
        ParticleSpawnScript.instance.spawnDelay = 0;
        ParticleSpawnScript.instance.spawnSpeed = 0;
    }
}
