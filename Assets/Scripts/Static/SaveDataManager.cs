﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveDataManager {
    [HideInInspector] public static SaveData getValue;
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

    //SETTINGS
    public SettingsLanguages settingsLanguage = SettingsLanguages.English;
    public SettingsSounds settingsSound = SettingsSounds.All;
    public SettingsVibration settingsVibration = SettingsVibration.Medium;
    public SettingsItemPosition settingsItemPosition = SettingsItemPosition.Left;
}