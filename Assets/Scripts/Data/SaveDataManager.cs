using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveDataManager {
    public static SaveData getValue;
    [HideInInspector] public static bool firstDataLoaded = false;

    //Load Data from File ore create File if no File Exists
    public static void Load() {
        if (File.Exists(Application.persistentDataPath + ConstantManager.localSaveFileName) && ConstantManager.useLocalSaveFile) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + ConstantManager.localSaveFileName, FileMode.Open);
            getValue = (SaveData)bf.Deserialize(file);
            file.Close();
            getValue.gameStatus = GameStatus.loading;
            firstDataLoaded = true;
        } else {
            Save();
        }
    }

    //Save Manipulated Data to File, or - if not manipulated - the defult Values
    public static void Save() {
        if (getValue == null)
                getValue = new SaveData();
        if (ConstantManager.useLocalSaveFile) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + ConstantManager.localSaveFileName);
            bf.Serialize(file, getValue);
            file.Close();
        }
        firstDataLoaded = true;
    }
}

//Class which contains all Data which needs to be saved and their default Values
[Serializable]
public class SaveData {
    //STATS
    public GameStatus gameStatus = GameStatus.loading;

    //STATS
    public int statsTotalGamesPlayed = 0;
    // Particles
    public int statsTotalParticles = 0;
    // Normal Particles
    public int statsTotalNormalParticlesSpawned = 0;
    public int statsTotalNormalParticlesDestroyed = 0;
    public int statsTotalNormalParticlesCollected = 0;
    public float statsTotalGainedMass = 0;
    // Shrink Particles
    public int statsTotalShrinkParticlesSpawned = 0;
    public int statsTotalShrinkParticlesDestroyed = 0;
    public int statsTotalShrinkParticlesCollected = 0;
    public float statsTotalLossMass = 0;
    // Gold Particles
    public int statsTotalGoldParticlesSpawned = 0;
    public int statsTotalGoldParticlesDestroyed = 0;
    public int statsTotalGoldParticlesCollected = 0;
    public int statsTotalGainedGold = 0;
    // Projectiles
    public int statsTotalProjectilesFired = 0;
    public int statsTotalProjectilesHit = 0;
    // Items
    public int statsTotalItemsSpawned = 0;
    public int statsTotalItemsUsed = 0;
    // Input
    public int statsTotalInputSwipe = 0;
    public int statsTotalInputTab = 0;
    // Time
    public float totalTimeIngame = 0f;
    // Score
    public float highscore = 0f;
    public float scoreTotal = 0f;


    //PROCESSES
    public bool tutorialFinished = false;
    public bool notificationAdsFinished = false;

    //SETTINGS
    public SettingsLanguages settingsLanguage = SettingsLanguages.English;
    public SettingsSounds settingsSound = SettingsSounds.All;
    public SettingsVibration settingsVibration = SettingsVibration.Medium;
    public SettingsItemPosition settingsItemPosition = SettingsItemPosition.Left;

    //SHOP
    public int shrinkItemLVL = 1; // 0 = Locked | 1 = LVL1 ...
    public int shieldItemLVL = 1;
    public int forceItemLVL = 1;
    public int clearItmLVL = 1;
    public int slowItemLVL = 1;
    public int slideItemLVL = 1;
    public int goldrushItemLVL = 1;
}