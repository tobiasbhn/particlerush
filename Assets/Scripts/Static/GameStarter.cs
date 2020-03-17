using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    //Start Game-Start-Routine on load
    void Start() {
        Debug.Log(LogTime.Time() + ": Game Starter Script - Starting Game...");
        StartCoroutine(CheckPrestartConditions());
    }

    //Save Data on Application-Quit
    private void OnApplicationQuit() {
        Debug.Log(LogTime.Time() + ": Game Starter Script - Game is going to Quit. Saving Data...");
        SaveDataManager.Save();
        Debug.Log(LogTime.Time() + ": Game Starter Script - Data save. Have a nice Day...");
    }

    //Defined Start-Routine
    IEnumerator CheckPrestartConditions() {
        //Check if all Saved Data is collected
        Debug.Log(LogTime.Time() + ": Game Starter Script - Loading saved Data...");
        SaveDataManager.Load();
        while (!SaveDataManager.firstDataLoaded) {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(LogTime.Time() + ": Game Starter Script - All saved Data loaded...");

        //Set Language
        Debug.Log(LogTime.Time() + ": Game Starter Script - Going to apply current Language...");
        if (SaveDataManager.getValue.languageManualySet == false) {
            var autoLanguage = Application.systemLanguage == SystemLanguage.German ? SettingsLanguages.German : SettingsLanguages.English;
            SaveDataManager.getValue.settingsLanguage = autoLanguage;
            SaveDataManager.Save();
        }
        LanguageScript.UpdateLanguage();
        Debug.Log(LogTime.Time() + ": Game Starter Script - Language Set...");

        //Set Vibration
        VibrationManager.Setup();

        //Setup Google Play
        GoogleLoginScript.instance.CheckAutoSetup();

        //Debug && FreeShop
        if (ConstantManager.freeShop) {
            SaveDataManager.getValue.coinItemLVL = 0;
            SaveDataManager.getValue.secondChanceItemLVL = 0;
            SaveDataManager.getValue.shieldItemLVL = 0;
            SaveDataManager.getValue.shootItemLVL = 0;
            SaveDataManager.getValue.shrinkItemLVL = 0;
            SaveDataManager.getValue.slideItemLVL = 0;
            SaveDataManager.Save();
        }

        //Call SceneManager to load whatever needs to be loaded
        Debug.Log(LogTime.Time() + ": Game Starter Script - Calling Scene Manager to load first Scene...");
        SceneManager.instance.startGame();
    }
}
