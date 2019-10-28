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
        OnSettingsEnable.instance.UpdateButtonUISound();
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
        OnSettingsEnable.instance.UpdateButtonUIVibration();
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
        OnSettingsEnable.instance.UpdateButtonUIItemPos();
    }

    public void ButtonRedirectInstagram() {
        //open Instagram on Device
        Application.OpenURL("https://www.instagram.com/tobias.bhn/");
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
        //kommt noch
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
