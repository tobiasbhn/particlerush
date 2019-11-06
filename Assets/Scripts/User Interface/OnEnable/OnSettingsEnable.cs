﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSettingsEnable : MonoBehaviour {

    //INSTANCE
    public static OnSettingsEnable instance;

    //VARS
    private string[] buttonPrefixesDE;
    private string[] buttonPrefixesEN;
    private string[] settingsSoundDataDE;
    private string[] settingsSoundDataEN;
    private string[] settingsVibrationDataEN;
    private string[] settingsVibrationDataDE;
    private string[] settingsItemPosDataDE;
    private string[] settingsItemPosDataEN;
    private string[] settingsDebugDataDE;
    private string[] settingsDebugDataEN;

    [HideInInspector] public bool showDebugScreen = false;

    void Awake() {
        instance = this;
    }
    void Start() {
        DefineVars();
    }
    void OnEnable() {
        UpdateButtonUI();
    }

    private void DefineVars() {
        buttonPrefixesDE = new string[4] { "Geräusche umschalten - ", "Vibrationen umschalten - ", "Item Position umschalten - ", "Debug Informationen - " };
        buttonPrefixesEN = new string[4] { "Toggle Sounds - ", "Toggle Vibrations - ", "Toggle Item Position - ", "Debug Informations - " };
        settingsSoundDataDE = new string[3] { "Aus", "An", "Nur Töne" };
        settingsSoundDataEN = new string[3] { "Off", "On", "Only Sounds" };
        settingsVibrationDataDE = new string[4] { "Aus", "Kurz", "Mittel", "Lang" };
        settingsVibrationDataEN = new string[4] { "Off", "Short", "Medium", "Long" };
        settingsItemPosDataDE = new string[2] { "Links", "Rechts" };
        settingsItemPosDataEN = new string[2] { "Left", "Right" };
        settingsDebugDataDE = new string[3] {"Aus", "Nur im Spiel", "Überall"};
        settingsDebugDataEN = new string[3] {"Off", "Only Ingame", "Everywhere"};
    }


    public void UpdateButtonUI() {
        UiObjectReferrer.instance.settingsScrollcontainer.verticalNormalizedPosition = 1f;
        UpdateButtonUISound();
        UpdateButtonUIVibration();
        UpdateButtonUIItemPos();
        UpdateButtonUIDebug();
    }
    public void UpdateButtonUISound() {
        if (settingsSoundDataDE == null || settingsSoundDataEN == null)
            DefineVars();
        var currendSoundSetting = (int)SaveDataManager.getValue.settingsSound;
        var soundTextDE = buttonPrefixesDE[0] + settingsSoundDataDE[currendSoundSetting];
        var soundTextEN = buttonPrefixesEN[0] + settingsSoundDataEN[currendSoundSetting];
        UiObjectReferrer.instance.settingsSoundTextDE.text = soundTextDE;
        UiObjectReferrer.instance.settingsSoundTextEN.text = soundTextEN;
    }
    public void UpdateButtonUIVibration() {
        if (settingsVibrationDataDE == null || settingsVibrationDataEN == null)
            DefineVars();
        var currendVibrationSetting = (int)SaveDataManager.getValue.settingsVibration;
        var vibrationTextDE = buttonPrefixesDE[1] + settingsVibrationDataDE[currendVibrationSetting];
        var vibrationTextEN = buttonPrefixesEN[1] + settingsVibrationDataEN[currendVibrationSetting];
        UiObjectReferrer.instance.settingsVibrationTextDE.text = vibrationTextDE;
        UiObjectReferrer.instance.settingsVibrationTextEN.text = vibrationTextEN;
    }
    public void UpdateButtonUIItemPos() {
        if (settingsItemPosDataDE == null || settingsItemPosDataEN == null)
            DefineVars();
        var currendItemPosSetting = (int)SaveDataManager.getValue.settingsItemPosition;
        var itemPosTextDE = buttonPrefixesDE[2] + settingsItemPosDataDE[currendItemPosSetting];
        var itemPosTextEN = buttonPrefixesEN[2] + settingsItemPosDataEN[currendItemPosSetting];
        UiObjectReferrer.instance.settingsItemPosTextDE.text = itemPosTextDE;
        UiObjectReferrer.instance.settingsItemPosTextEN.text = itemPosTextEN;
    }
    public void UpdateButtonUIDebug() {
        if (settingsItemPosDataDE == null || settingsItemPosDataEN == null)
            DefineVars();
        var currendItemPosSetting = (int)SaveDataManager.getValue.settingsDebug;
        var debugTextDE = buttonPrefixesDE[3] + settingsDebugDataDE[currendItemPosSetting];
        var debugTextEN = buttonPrefixesEN[3] + settingsDebugDataEN[currendItemPosSetting];
        UiObjectReferrer.instance.settingsDebugTextDE.text = debugTextDE;
        UiObjectReferrer.instance.settingsDebugTextEN.text = debugTextEN;
    }
}