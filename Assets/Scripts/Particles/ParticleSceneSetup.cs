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
        ParticleSpawnScript.instance.spawnBaseDelay = ConstantManager.PARTICLE_SPAWN_BASE_DELAY_INGAME;
        ParticleSpawnScript.instance.spawnBaseSpeed = ConstantManager.PARTICLE_SPAWN_BASE_SPEED_INGAME;
        ParticleSpawnScript.instance.allowSpeedIncrease = ConstantManager.PARTICLE_ALLOW_SPEED_INCREASE_INGAME;
        ParticleSpawnScript.instance.spawnModi = ConstantManager.PARTICLE_SPAWNMODI_INGAME;
    }

    public void SetupMenu() {
        ParticleSpawnScript.instance.DestroyAllParticles();
        ParticleSpawnScript.instance.spawnBaseDelay = ConstantManager.PARTICLE_SPAWN_BASE_DELAY_MENU;
        ParticleSpawnScript.instance.spawnBaseSpeed = ConstantManager.PARTICLE_SPAWN_BASE_SPEED_MENU;
        ParticleSpawnScript.instance.allowSpeedIncrease = ConstantManager.PARTICLE_ALLOW_SPEED_INCREASE_MENU;
        ParticleSpawnScript.instance.spawnModi = ConstantManager.PARTICLE_SPAWNMODI_MENU;
    }

    public void SetupDisabled() {
        ParticleSpawnScript.instance.DestroyAllParticles();
        ParticleSpawnScript.instance.spawnBaseDelay = 0;
        ParticleSpawnScript.instance.spawnBaseSpeed = 0;
        ParticleSpawnScript.instance.allowSpeedIncrease = false;
        ParticleSpawnScript.instance.spawnModi = SpawnModi.none;
    }
}
