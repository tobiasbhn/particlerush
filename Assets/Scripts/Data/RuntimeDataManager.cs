using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDataManager : MonoBehaviour {

    //INSTANCE
    public static RuntimeDataManager instance;

    // RUNTIME DATA
    [HideInInspector] public static RuntimeData value;
    public RuntimeData preRevive;
    public RuntimeData postRevive;

    void Awake() {
        instance = this;
    }

    void Start() {
        preRevive = new RuntimeData();
        postRevive = new RuntimeData();
        value = preRevive;
    }

    public void SetupIngame() {
        if (ReviveScript.instance.alreadyRevived) {
            postRevive = new RuntimeData();
            postRevive.totalCountRevive++;
            value = postRevive;
            value.startTime = Time.realtimeSinceStartup;
        } else {
            postRevive = new RuntimeData();
            preRevive = new RuntimeData();
            preRevive.totalGamesPlayed++;
            value = preRevive;
            value.startTime = Time.realtimeSinceStartup;
        }
    }

    public void SetupEndgame() {
        SaveDataManager.getValue.statsTotalGamesPlayed += preRevive.totalGamesPlayed + postRevive.totalGamesPlayed;
        SaveDataManager.getValue.statsTotleCountRevive += preRevive.totalCountRevive + postRevive.totalCountRevive;
        // STATS
        SaveDataManager.getValue.statsTotalParticles += value.particlesSpawned;
        // Normal Particles
        SaveDataManager.getValue.statsTotalNormalParticlesSpawned += value.normalParticlesSpawned;
        SaveDataManager.getValue.statsTotalNormalParticlesDestroyed += value.normalParticlesDestroyed;
        SaveDataManager.getValue.statsTotalNormalParticlesCollected += value.normalParticlesCollected;
        SaveDataManager.getValue.statsTotalGainedMass += value.gainedMass;
        // Shrink Particles
        SaveDataManager.getValue.statsTotalShrinkParticlesSpawned += value.shrinkParticlesSpawned;
        SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed += value.shrinkParticlesDestroyed;
        SaveDataManager.getValue.statsTotalShrinkParticlesCollected += value.shrinkParticlesCollected;
        SaveDataManager.getValue.statsTotalLossMass += value.lossMass;
        // Gold Particles
        SaveDataManager.getValue.statsTotalGoldParticlesSpawned += value.goldParticlesSpawned;
        SaveDataManager.getValue.statsTotalGoldParticlesDestroyed += value.goldParticlesDestroyed;
        SaveDataManager.getValue.statsTotalGoldParticlesCollected += value.goldParticlesCollected;
        SaveDataManager.getValue.statsTotalGainedGold += value.goldMassCollected;
        // Projectiles
        SaveDataManager.getValue.statsTotalProjectilesFired += value.projectilesFiredTotal;
        SaveDataManager.getValue.statsTotalProjectilesHit += value.projectilesHitTotal;
        // Items
        SaveDataManager.getValue.statsTotalItemsSpawned += value.itemsSpawned;
        SaveDataManager.getValue.statsTotalItemsUsed += value.itemsUsed;
        // Input
        SaveDataManager.getValue.statsTotalInputSwipe += value.inputSwipeCount;
        SaveDataManager.getValue.statsTotalInputTab += value.inputTabCount;
        // Time
        SaveDataManager.getValue.totalTimeIngame += Time.realtimeSinceStartup - value.startTime;
        // Score
        SaveDataManager.getValue.highscore = value.highscore;
        SaveDataManager.getValue.scoreTotal += value.score;

        SaveDataManager.Save();
    }
}

[System.Serializable]
public class RuntimeData {
    // STATS
    public int totalGamesPlayed = 0;
    public int totalCountRevive = 0;
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
    // Items
    public int itemsSpawned = 0;
    public int itemsUsed = 0;
    // Input
    public int inputSwipeCount = 0;
    public int inputTabCount = 0;
    // Time
    public float startTime = 0f;
    // Score
    public float score = 0f;
    public float highscore = 0f;
    public float difficultyFactor = 0f;
}
