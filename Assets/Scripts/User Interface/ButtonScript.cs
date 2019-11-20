using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    //INSTANCE
    public static ButtonScript instance;

    void Awake() {
        instance = this;
    }

    // GERNERAL
    public void ButtonLoadPreviousScenario() {
        if (SceneManager.instance.previousScenario != null)
            SceneManager.instance.previousScenario.callScenario();
    }
    
    //SETTINGS
    public void ButtonSettingLanguage() {
        var lang = SaveDataManager.getValue.settingsLanguage;
        SaveDataManager.getValue.settingsLanguage = lang == SettingsLanguages.English ? SettingsLanguages.German : SettingsLanguages.English;
        SaveDataManager.getValue.languageManualySet = true;
        SaveDataManager.Save();
        LanguageScript.UpdateLanguage();
    }
    public void ButtonSettingsSound() {
        switch(SaveDataManager.getValue.settingsSound) {
            case SettingsSounds.All:
                SaveDataManager.getValue.settingsSound = SettingsSounds.Sound;
                SoundScript.instance.SoundOnlySound();
                break;
            case SettingsSounds.Sound:
                SaveDataManager.getValue.settingsSound = SettingsSounds.Off;
                SoundScript.instance.SoundOff();
                break;
            case SettingsSounds.Off:
                SaveDataManager.getValue.settingsSound = SettingsSounds.All;
                SoundScript.instance.SoundOn();
                break;
            default:
                SaveDataManager.getValue.settingsSound = SettingsSounds.All;
                SoundScript.instance.SoundOn();
                break;
        }
        SaveDataManager.Save();
        OESettings.instance.UpdateButtonUISound();
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
        VibrationManager.Setup();
        OESettings.instance.UpdateButtonUIVibration();
        VibrationManager.Vibrate();
    }

    public void ButtonSettingsDebug() {
        switch(SaveDataManager.getValue.settingsDebug) {
            case SettingsDebug.off:
                SaveDataManager.getValue.settingsDebug = SettingsDebug.ingame;
                break;
            case SettingsDebug.ingame:
                SaveDataManager.getValue.settingsDebug = SettingsDebug.everywhere;
                break;
            case SettingsDebug.everywhere:
                SaveDataManager.getValue.settingsDebug = SettingsDebug.off;
                break;
        }
        SaveDataManager.Save();
        FPS.instance.UpdateShow();
        OESettings.instance.UpdateButtonUIDebug();
    }

    public void ButtonRedirectInstagram() {
        //open Instagram on Device
        Application.OpenURL("https://www.instagram.com/tobias.bhn/");
    }
    public void ButtonRedirectTwitter() {
        // open Twitter on Device
        Application.OpenURL("https://twitter.com/tobias_bhn");
    }
    public void ButtonSpecialAds() {
        //Show Rewarded Ad
        if (SaveDataManager.getValue.notificationAdsFinished)
            AdsManager.instance.ShowAd(AdType.Rewarded, (AdResult result) => {
                SceneManager.instance.callSceneSettings();
            });
        else
            SceneManager.instance.callSceneAdsNotification();
    }
    public void ButtonSpecialAdsContinueNotification() {
        SaveDataManager.getValue.notificationAdsFinished = true;
        SaveDataManager.Save();
        // SceneManager.callSceneSettings();
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
        if (GoogleLoginScript.instance.isAuthenticated()) {
            SceneManager.instance.callSceneLogoutNotification();
        } else {
            SceneManager.instance.callSceneLoginNotification();
        }
    }
    public void ButtonRedirectImprint() {
        //open Leagal Webpage
        Application.OpenURL("https://legal.tobiasbohn.com/");
    }


    // REVIVE
    public void ButtonReviveCancel() {
        ReviveScript.instance.SetupLevel();
    }
    public void ButtonReviveGold() {
        ReviveScript.instance.reviveGold();
    }
    public void ButtonReviveAd() {
        ReviveScript.instance.reviveAd();
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Cheat() {
        PlayerScript.instance.SetTargetMass(PlayerScript.instance.targetMass + 5);
    }
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------
}
