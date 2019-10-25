using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    //INSTANCE
    public static ButtonScript instance;
    public bool thisScriptLoaded = false;

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
        thisScriptLoaded = true;
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

    //SCENES
    public void ButtonSceneCallMenu() {
        SceneManager.callSceneMenu();
    }
    public void ButtonSceneCallIngame() {
        SceneManager.callSceneIngame(true);
    }
    public void ButtonSceneCallShop() {
        SceneManager.callSceneShop();
    }
    public void ButtonSceneCallTutorial() {
        //
    }
    public void ButtonSceneCallSettings() {
        SceneManager.callSceneSettings();
    }
    public void ButtonSceneCallPause() {
        SceneManager.callScenePause();
    }
    public void ButtonSceneCallResume() {
        SceneManager.callSceneResume();
    }

    //SETTINGS
    public void ButtonSettingLanguage() {
        var lang = SaveDataManager.getValue.settingsLanguage;
        SaveDataManager.getValue.settingsLanguage = lang == SettingsLanguages.English ? SettingsLanguages.German : SettingsLanguages.English;
        SaveDataManager.Save();
        LanguageScript.UpdateLanguage();
    }
    public void ButtonSettingsSound() {
        switch(SaveDataManager.getValue.settingsSound) {
            case SettingsSounds.All:
                SaveDataManager.getValue.settingsSound = SettingsSounds.Sound;
                break;
            case SettingsSounds.Sound:
                SaveDataManager.getValue.settingsSound = SettingsSounds.Off;
                break;
            case SettingsSounds.Off:
                SaveDataManager.getValue.settingsSound = SettingsSounds.All;
                break;
            default:
                SaveDataManager.getValue.settingsSound = SettingsSounds.All;
                break;
        }
        SaveDataManager.Save();
        UpdateButtonUISound();
    }
    public void ButtonSettingsVibration() {
        switch(SaveDataManager.getValue.settingsVibration) {
            case SettingsVibration.Off:
                SaveDataManager.getValue.settingsVibration = SettingsVibration.Short;
                break;
            case SettingsVibration.Short:
                SaveDataManager.getValue.settingsVibration = SettingsVibration.Medium;
                break;
            case SettingsVibration.Medium:
                SaveDataManager.getValue.settingsVibration = SettingsVibration.Long;
                break;
            case SettingsVibration.Long:
                SaveDataManager.getValue.settingsVibration = SettingsVibration.Off;
                break;
            default:
                SaveDataManager.getValue.settingsVibration = SettingsVibration.Medium;
                break;
        }
        SaveDataManager.Save();
        UpdateButtonUIVibration();
    }
    public void ButtonSettingsItemPos() {
        switch(SaveDataManager.getValue.settingsItemPosition) {
            case SettingsItemPosition.Left:
                SaveDataManager.getValue.settingsItemPosition = SettingsItemPosition.Right;
                break;
            case SettingsItemPosition.Right:
                SaveDataManager.getValue.settingsItemPosition = SettingsItemPosition.Left;
                break;
            default:
                SaveDataManager.getValue.settingsItemPosition = SettingsItemPosition.Left;
                break;
        }
        SaveDataManager.Save();
        UpdateButtonUIItemPos();
    }

    public void ButtonRedirectInstagram() {
        //open Instagram on Device
        Application.OpenURL("https://www.instagram.com/tobias.bhn/");
    }
    public void ButtonSpecialAds() {
        //Show Rewarded Ad
        if (SaveDataManager.getValue.notificationAdsFinished)
            StartCoroutine(AdsManager.instance.ShowAd(AdType.Rewarded));
        else
            SceneManager.callSceneAdsNotification();
    }
    public void ButtonSpecialAdsContinueNotification() {
        SaveDataManager.getValue.notificationAdsFinished = true;
        SaveDataManager.Save();
        SceneManager.callSceneSettings();
        ButtonSpecialAds();
    }
    public void ButtonRedirectPayPal() {
        //Link to Donation-Page
        Application.OpenURL("https://paypal.me/tobiasbhn");
    }
    public void ButtonRedirectPlayerSupport() {
        var mailto = "mailto:info@tobiasbohn.com";
        var subject = "Particle%20Rush%20Player%20Support";
        var bodyEN = "Hey%20Tobi%2C%0AI%20have%20a%20question%20or%20other%20concern%20about%20your%20game%20Particle%20Rush%3A";
        var bodyDE = "Hey%20Tobi%2C%0Aich%20habe%20eine%20Frage%20oder%20ein%20anderes%20Anliegen%20zu%20deinem%20Spiel%20Particle%20Rush%3A";
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German)
            Application.OpenURL(mailto + "?subject=" + subject + "&body=" + bodyDE);
        else
            Application.OpenURL(mailto + "?subject=" + subject + "&body=" + bodyEN);
    }
    public void ButtonRedirectBugs() {
        var mailto = "mailto:info@tobiasbohn.com";
        var subject = "Particle%20Rush%20Bug%20Report";
        var bodyEN = "Hey%20Tobi%2C%0AI%20found%20a%20bug%20in%20Particle%20Rush%3A";
        var bodyDE = "Hey%20Tobi%2C%0Aich%20habe%20einen%20Bug%20in%20Particle%20Rush%20gefunden%3A";
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German)
            Application.OpenURL(mailto + "?subject=" + subject + "&body=" + bodyDE);
        else
            Application.OpenURL(mailto + "?subject=" + subject + "&body=" + bodyEN);
    }
    public void ButtonSpecialGooglePlaySignOff() {
        //kommt noch
    }
    public void ButtonSpecialLibarys() {
        //kommt noch
    }
    public void ButtonRedirectImprint() {
        //open Leagal Webpage
        Application.OpenURL("https://legal.tobiasbohn.com/");
    }


    // REVIVE
    public void ButtonReviveCancel() {
        UiSceneScript.instance.DisableReviveScreen();
    }
    public void ButtonReviveGold() {
        EndgameScript.instance.reviveGold();
    }
    public void ButtonReviveAd() {
        EndgameScript.instance.reviveAd();
    }



    //HELPER
    public void UpdateButtonUI() {
        UpdateButtonUISound();
        UpdateButtonUIVibration();
        UpdateButtonUIItemPos();
    }
    private void UpdateButtonUISound() {
        var currendSoundSetting = (int)SaveDataManager.getValue.settingsSound;
        var soundTextDE = buttonPrefixesDE[0] + settingsSoundDataDE[currendSoundSetting];
        var soundTextEN = buttonPrefixesEN[0] + settingsSoundDataEN[currendSoundSetting];
        UiObjectReferrer.instance.settingsSoundTextDE.GetComponent<Text>().text = soundTextDE;
        UiObjectReferrer.instance.settingsSoundTextEN.GetComponent<Text>().text = soundTextEN;
    }
    private void UpdateButtonUIVibration() {
        var currendVibrationSetting = (int)SaveDataManager.getValue.settingsVibration;
        var vibrationTextDE = buttonPrefixesDE[1] + settingsVibrationDataDE[currendVibrationSetting];
        var vibrationTextEN = buttonPrefixesEN[1] + settingsVibrationDataEN[currendVibrationSetting];
        UiObjectReferrer.instance.settingsVibrationTextDE.GetComponent<Text>().text = vibrationTextDE;
        UiObjectReferrer.instance.settingsVibrationTextEN.GetComponent<Text>().text = vibrationTextEN;
    }
    private void UpdateButtonUIItemPos() {
        var currendItemPosSetting = (int)SaveDataManager.getValue.settingsItemPosition;
        var itemPosTextDE = buttonPrefixesDE[2] + settingsItemPosDataDE[currendItemPosSetting];
        var itemPosTextEN = buttonPrefixesEN[2] + settingsItemPosDataEN[currendItemPosSetting];
        UiObjectReferrer.instance.settingsItemPosTextDE.GetComponent<Text>().text = itemPosTextDE;
        UiObjectReferrer.instance.settingsItemPosTextEN.GetComponent<Text>().text = itemPosTextEN;
    }

}
