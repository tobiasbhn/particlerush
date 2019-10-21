using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDataManager : MonoBehaviour {

    //INSTANCE
    public static RuntimeDataManager instance;
    public bool thisScriptLoaded = false;

    // RUNTIME DATE
    public static RuntimeData setValue;
    public RuntimeData preRevive;
    public RuntimeData postRevive;

    void Awake() {
        instance = this;
    }
    void Start() {
        preRevive = new RuntimeData();
        postRevive = new RuntimeData();
        setValue = preRevive;
        thisScriptLoaded = true;
    }

    public void SetupIngame() {
        if (EndgameScript.instance.alreadyRevived) {
            postRevive = new RuntimeData();
            setValue = postRevive;
            setValue.startTime = Time.realtimeSinceStartup;
        } else {
            postRevive = new RuntimeData();
            preRevive = new RuntimeData();
            setValue = preRevive;
            setValue.startTime = Time.realtimeSinceStartup;
        }
    }

    public void SaveRuntimeData() {
        // STATS
        SaveDataManager.getValue.statsTotalParticles += setValue.particlesSpawned;
        // Normal Particles
        SaveDataManager.getValue.statsTotalNormalParticlesSpawned += setValue.normalParticlesSpawned;
        SaveDataManager.getValue.statsTotalNormalParticlesDestroyed += setValue.normalParticlesDestroyed;
        SaveDataManager.getValue.statsTotalNormalParticlesCollected += setValue.normalParticlesCollected;
        SaveDataManager.getValue.statsTotalGainedMass += setValue.gainedMass;
        // Shrink Particles
        SaveDataManager.getValue.statsTotalShrinkParticlesSpawned += setValue.shrinkParticlesSpawned;
        SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed += setValue.shrinkParticlesDestroyed;
        SaveDataManager.getValue.statsTotalShrinkParticlesCollected += setValue.shrinkParticlesCollected;
        SaveDataManager.getValue.statsTotalLossMass += setValue.lossMass;
        // Gold Particles
        SaveDataManager.getValue.statsTotalGoldParticlesSpawned += setValue.goldParticlesSpawned;
        SaveDataManager.getValue.statsTotalGoldParticlesDestroyed += setValue.goldParticlesDestroyed;
        SaveDataManager.getValue.statsTotalGoldParticlesCollected += setValue.goldParticlesCollected;
        SaveDataManager.getValue.statsTotalGainedGold += setValue.goldMassCollected;
        // Projectiles
        SaveDataManager.getValue.statsTotalProjectilesFired += setValue.projectilesFiredTotal;
        SaveDataManager.getValue.statsTotalProjectilesHit += setValue.projectilesHitTotal;
        // Time
        SaveDataManager.getValue.totalTimeIngame += Time.realtimeSinceStartup - setValue.startTime;

        SaveDataManager.Save();
    }
}

[System.Serializable]
public class RuntimeData {
    // STATS
    // Particles
    public int particlesSpawned = 0;
    public int normalParticlesSpawned = 0;
    public int normalParticlesDestroyed = 0;
    public int normalParticlesCollected = 0;
    public float gainedMass = 0;
    public int shrinkParticlesSpawned = 0;
    public int shrinkParticlesDestroyed = 0;
    public int shrinkParticlesCollected = 0;
    public float lossMass = 0;
    public int goldParticlesSpawned = 0;
    public int goldParticlesDestroyed = 0;
    public int goldParticlesCollected = 0;
    public int goldMassCollected = 0;
    // Projectiles
    public int projectilesFiredTotal = 0;
    public int projectilesHitTotal = 0;
    // Time
    public float startTime = 0;
}
