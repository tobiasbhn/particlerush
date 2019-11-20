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
        if (OEAccountInfo.instance != null)
            OEAccountInfo.instance.UpdateAccountInfo();
        if (OELevelInfo.instance != null)
            OELevelInfo.instance.UpdateInfos();
    }
}

//Class which contains all Data which needs to be saved and their default Values
[Serializable]
public class SaveData {
    //STATS
    public GameStatus gameStatus = GameStatus.loading;

    //STATS
    public int statsTotalGamesPlayed = 0;
    public int statsTotleCountRevive = 0;
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
    // Input
    public int statsTotalInputSwipe = 0;
    public int statsTotalInputTab = 0;
    // Time
    public float totalTimeIngame = 0f;
    // Score
    public float scoreTotal = 0f;
    public int currentGold = 0;
    public int currentLevel = 0;
    public int currentLevelPoints = 0;
    public int currentRemainingLevelPoints = 0;

    //HIGHSCORE
    public float highscore = 0f;
    public int highscoreRoundDataGrow = 0;
    public int highscoreRoundDataShrink = 0;
    public int highscoreRoundDataGold = 0;
    public float highscoreRoundDataTime = 0f;



    //PROCESSES
    public bool tutorialFinished = false;
    public bool notificationAdsFinished = false;
    public bool languageManualySet = false;
    public bool playGamesAutomaticAuth = false;

    //SETTINGS
    public SettingsLanguages settingsLanguage = SettingsLanguages.English;
    public SettingsSounds settingsSound = SettingsSounds.All;
    public SettingsVibration settingsVibration = SettingsVibration.Short;
    public SettingsDebug settingsDebug = SettingsDebug.off;

    //SHOP
    public int statsTotalGoldSpend = 0;
    public int shrinkItemLVL = 0; // 0 = Locked | 1 = LVL1 ...
    public int shieldItemLVL = 0;
    public int forceItemLVL = 0;
    public int clearItmLVL = 0;
    public int slowItemLVL = 0;
    public int slideItemLVL = 0;
    public int goldrushItemLVL = 0;
}