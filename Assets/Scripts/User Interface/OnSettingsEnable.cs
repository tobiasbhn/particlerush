using System.Collections;
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
        buttonPrefixesDE = new string[3] {
            UiObjectReferrer.instance.settingsSoundTextDE.GetComponent<Text>().text,
            UiObjectReferrer.instance.settingsVibrationTextDE.GetComponent<Text>().text,
            UiObjectReferrer.instance.settingsItemPosTextDE.GetComponent<Text>().text
        };
        buttonPrefixesEN = new string[3] {
            UiObjectReferrer.instance.settingsSoundTextEN.GetComponent<Text>().text,
            UiObjectReferrer.instance.settingsVibrationTextEN.GetComponent<Text>().text,
            UiObjectReferrer.instance.settingsItemPosTextEN.GetComponent<Text>().text
        };
        settingsSoundDataDE = new string[3] { "Aus", "An", "Nur Töne" };
        settingsSoundDataEN = new string[3] { "Off", "On", "Only Sounds" };
        settingsVibrationDataDE = new string[4] { "Aus", "Kurz", "Mittel", "Lang" };
        settingsVibrationDataEN = new string[4] { "Off", "Short", "Medium", "Long" };
        settingsItemPosDataDE = new string[2] { "Rechts", "Links" };
        settingsItemPosDataEN = new string[2] { "Right", "Left" };
    }


    public void UpdateButtonUI() {
        UpdateButtonUISound();
        UpdateButtonUIVibration();
        UpdateButtonUIItemPos();
    }
    public void UpdateButtonUISound() {
        var currendSoundSetting = (int)SaveDataManager.getValue.settingsSound;
        var soundTextDE = buttonPrefixesDE[0] + settingsSoundDataDE[currendSoundSetting];
        var soundTextEN = buttonPrefixesEN[0] + settingsSoundDataEN[currendSoundSetting];
        UiObjectReferrer.instance.settingsSoundTextDE.GetComponent<Text>().text = soundTextDE;
        UiObjectReferrer.instance.settingsSoundTextEN.GetComponent<Text>().text = soundTextEN;
    }
    public void UpdateButtonUIVibration() {
        var currendVibrationSetting = (int)SaveDataManager.getValue.settingsVibration;
        var vibrationTextDE = buttonPrefixesDE[1] + settingsVibrationDataDE[currendVibrationSetting];
        var vibrationTextEN = buttonPrefixesEN[1] + settingsVibrationDataEN[currendVibrationSetting];
        UiObjectReferrer.instance.settingsVibrationTextDE.GetComponent<Text>().text = vibrationTextDE;
        UiObjectReferrer.instance.settingsVibrationTextEN.GetComponent<Text>().text = vibrationTextEN;
    }
    public void UpdateButtonUIItemPos() {
        var currendItemPosSetting = (int)SaveDataManager.getValue.settingsItemPosition;
        var itemPosTextDE = buttonPrefixesDE[2] + settingsItemPosDataDE[currendItemPosSetting];
        var itemPosTextEN = buttonPrefixesEN[2] + settingsItemPosDataEN[currendItemPosSetting];
        UiObjectReferrer.instance.settingsItemPosTextDE.GetComponent<Text>().text = itemPosTextDE;
        UiObjectReferrer.instance.settingsItemPosTextEN.GetComponent<Text>().text = itemPosTextEN;
    }
}
